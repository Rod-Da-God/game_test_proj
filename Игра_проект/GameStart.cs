using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Игра_проект
{
    internal class GameStart
    {
        public List<Player> leaderboard = new List<Player>();
        public List<Player> user = new List<Player>();
        private const string usersFile = "users.json";
        private const string LeaderboardFile = "leaderboard.json";

        public GameStart()
        {
            LoadLeaderboard();
            LoadUsers();
        }

        public void StartGame()
        {
            Console.WriteLine("Добро пожаловать в игру 'Угадай число'!");
            Console.WriteLine("Введите логин");
            string login = Console.ReadLine();
            Console.WriteLine("Введите пароль");
            string password = Console.ReadLine();

            if (AuthenticateUser(login, password))
            {
               
                Console.WriteLine("Введите ваш ник!");
                string name = Console.ReadLine();
                Game game = new Game();
                string result;
                bool play = true;
                while (play)
                {
                AGAIN:
                    Console.WriteLine("ведите 4 значное число");
                    string answer = Console.ReadLine();
                    if (answer.Length > 4 || answer.Length < 4)
                    {
                        goto AGAIN;
                    }
                    result = game.Check(answer);
                    Console.WriteLine(result);
                    if (result.StartsWith("Поздравляю"))
                    {
                        SavePlayerResult(name, result);
                        ShowMenu();
                        play = false;
                    }
                }
            }
            else
            {
                Console.WriteLine("неправильный логин пароль");
                Console.Clear();
                StartGame();
            }
        }

        private void SavePlayerResult(string name, string result)
        {
            leaderboard.Add(new Player(name, result));
            leaderboard = leaderboard.OrderBy(p => int.Parse(p.Score.Split(' ')[4])).ToList();
            SaveLeaderboard();
        }


        public void ShowMenu()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Вывести список лидеров");
            Console.WriteLine("2. Начать новую игру");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                ShowLeaderboard();
            }
            else if (choice == "2")
            {
                StartGame();
            }
        }

        private void ShowLeaderboard()
        {
            Console.WriteLine("Список лидеров:");
            foreach (var player in leaderboard)
            {
                Console.WriteLine($"{player.Name}: {player.Score}");
            }
            ShowMenu();
        }

        private void SaveLeaderboard()
        {
            string jsonString = JsonSerializer.Serialize(leaderboard);
            File.WriteAllText(LeaderboardFile, jsonString);
        }
        private void LoadUsers()
        {
            string json = File.ReadAllText(usersFile);
            user = JsonSerializer.Deserialize<List<Player>>(json);
        }

        private bool AuthenticateUser(string login, string password)
        {
            return user.Any(u => u.Name == login && u.Password.ToString() == password);
        }

        private void LoadLeaderboard()
        {
            if (File.Exists(LeaderboardFile))
            {
                string jsonString = File.ReadAllText(LeaderboardFile);
                leaderboard = JsonSerializer.Deserialize<List<Player>>(jsonString);
            }
        }
    }

}
