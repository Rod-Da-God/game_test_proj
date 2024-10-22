using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Игра_проект
{
    internal class Game
    {
        private string number;
        private int count;

        public Game()
        {
            number = GetRand();
            count = 0;
        }

        public string GetRand()
        {
           var random = new Random();
            var digits = new List<int>();
            while (digits.Count < 4)
            {
                int digit = random.Next(0, 10);
                if (!digits.Contains(digit))
                {
                    digits.Add(digit);
                }
            }
            return string.Join("", digits);
        }

        public string Check(string answer)
        {
            count++;
            int correctPosition = 0;
            int correctNumber = 0;


            for (int i = 0; i < number.Length; i++)
            {
                if (answer[i] == number[i])
                {
                    correctPosition++;
                }
            }

            for (int i = 0; i < number.Length; i++)
            {
                if (answer.Contains(number[i]))
                {
                    correctNumber++;
                }
            }

            if (correctPosition == 4)
            {
                return $"Поздравляю, вы победили за {count} попыток";
            }
            else
            {
                return $"Угаданных позиций: {correctPosition}, Угаданных чисел: {correctNumber}";
            }
        }
    }
}
