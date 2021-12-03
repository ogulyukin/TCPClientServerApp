using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChatServer
{
    class Chatter
    {
        private string[] _stringArr;
        private Random _rand;

        public Chatter()
        {
            _rand = new();
            LoadLorem();
        }

        public string GetNewText()
        {
            var count = _rand.Next(1, 5);
            StringBuilder result = new();
            for (int i = 0; i < count; i++)
                result.Append($" {GetNextWord()}");
            return result.ToString();
        }

        private string GetNextWord()
        {
            return _stringArr[_rand.Next(0, _stringArr.Length)]; 
        }

        private void LoadLorem()
        {
            if (!System.IO.File.Exists("lorem.txt"))
            {
                Console.WriteLine("Не найден файл lorem.txt запуск чатбота невозможен!");
                return;
            }

            try
            {
                StreamReader sr = new("lorem.txt");
                string resultString = sr.ReadToEnd();
                _stringArr = resultString.Split(' ');
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}
