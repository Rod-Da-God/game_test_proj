using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Игра_проект
{
    internal class Player
    {
        public string Name { get; set; }
        public string Score { get; set; }

        public int Password { get; set; }

        public Player(string name, string score)
        {
            Score = score;
            Name = name;
        }

    }
}
