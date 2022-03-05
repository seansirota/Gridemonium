using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Gridemonium
{
    //Stream operations class that is used to display high scores in the high scores tab. Has methods
    //for reading, writing, creating a file, and updating the scores.
    public class StreamOps
    {
        //File stream objects for accessing the text file with all the scores, reading, writing, and the list containing the scores.
        private static FileInfo _highscores;
        private static FileStream _stream;
        private static StreamWriter _fileWriter;
        private static StreamReader _fileReader;
        private static List<string> scores;

        //Method that is used at the launch of the program. Creates a text file if one doesn't already exist
        //and populates it with default values.
        public static void CreateFile()
        {
            _highscores = new FileInfo(@"C:\Users\Sean\Documents\Highscores.txt");
            if (!_highscores.Exists)
            {
                using(_stream = _highscores.Open(FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    _fileWriter = new StreamWriter(_stream);
                    _fileWriter.WriteLine("High Scores");

                    for (int i = 0; i < 10; i++)
                        _fileWriter.WriteLine("---");

                    _fileWriter.Close();                                       
                }
            }

            scores = ReadFile();
        }
       
        //Method used to add the score achieved at the end of a game to the list.
        //Clears the text file every time before adding the updated scores.
        public static void AddScore(int score)
        {
            File.WriteAllText(_highscores.FullName, string.Empty);

            using (_stream = _highscores.Open(FileMode.Open, FileAccess.Write, FileShare.Read))
            {                
                for (int i = 0; i < 11; i++)
                {
                    if (scores[i] == "High Scores")
                        continue;

                    if (scores[i] == "---")
                    {
                        UpdateFile(i, score);
                        i = 11;
                    }                        
                    else
                    {
                        if (Int32.Parse(scores[i]) < score)
                        {
                            UpdateFile(i, score);
                            i = 11;
                        }
                    }                    
                }
            }
        }

        //Method used on the HighScores form to display all high scores. Only top ten scores are shown.
        public static void DisplayScores(HighScores scoresForm)
        {
            string scoresList = "";

            foreach (string s in scores)
                scoresList += s + "\n";
            
            scoresForm.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "HighScoresLabel").Text = scoresList;
        }

        //Method used to read the text file to extract contents and use it when updating the scores.
        private static List<string> ReadFile()
        {
            using (_stream = _highscores.OpenRead())
            {
                _fileReader = new StreamReader(_stream);
                string content = _fileReader.ReadToEnd();

                _fileReader.Close();
                return content.Replace("\n", "").Split('\r').ToList();
            }
        }

        //Method used for writing to the text file from the score list data.
        //Places the score achieved from the game in the table if there is a blank space,
        //or if it is higher than a previously entered scored. Only the top ten scores are ever saved.
        private static void UpdateFile(int position, int score)
        {
            _fileWriter = new StreamWriter(_stream);
            _fileWriter.WriteLine("High Scores");            

            for (int i = 1; i < scores.Count; i++)
            {
                if (i == 11)
                {
                    i = scores.Count;
                    continue;
                }

                if (i == position)
                {
                    _fileWriter.WriteLine(score);
                    scores.Insert(position, score.ToString());
                    continue;
                }
                else 
                    _fileWriter.WriteLine(scores[i]);
            }

            scores.RemoveAt(11);
            _fileWriter.Close();
        }
    }
}
