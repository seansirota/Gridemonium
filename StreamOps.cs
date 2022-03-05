using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Gridemonium
{
    public class StreamOps
    {
        private static FileInfo _highscores;
        private static FileStream _stream;
        private static StreamWriter _fileWriter;
        private static StreamReader _fileReader;
        private static List<string> scores;

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
       
        public static void AddScore(int score)
        {
            File.WriteAllText(_highscores.FullName, string.Empty);

            using (_stream = _highscores.Open(FileMode.Open, FileAccess.Write, FileShare.Read))
            {                
                for (int i = 0; i < scores.Count; i++)
                {
                    if (scores[i] == "High Scores")
                        continue;

                    if (scores[i] == "---")
                    {
                        UpdateFile(scores, i, score);
                        i = scores.Count;
                    }                        

                    if (Int32.Parse(scores[i]) < score)
                    {
                        UpdateFile(scores, i, score);
                        i = scores.Count;
                    }
                    else
                    {
                        UpdateFile(scores, i + 1, score);
                        i = scores.Count;
                    }
                }
            }
        }

        public static void DisplayScores(HighScores scoresForm)
        {
            string scoresList = "";

            foreach (string s in scores)
                scoresList += s + "\n";
            
            scoresForm.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "HighScoresLabel").Text = scoresList;
        }

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

        private static void UpdateFile(List<string> scores, int position, int score)
        {
            _fileWriter = new StreamWriter(_stream);
            _fileWriter.WriteLine("High Scores");            

            for (int i = 0; i < scores.Count; i++)
            {
                if (i == 10)
                    break;

                if (i == position - 1)
                {
                    _fileWriter.WriteLine(score);
                    scores.Insert(position, score.ToString());
                    continue;
                }

                _fileWriter.WriteLine(scores[i]);
            }

            scores.RemoveRange(11, 1);
            _fileWriter.Close();
        }
    }
}
