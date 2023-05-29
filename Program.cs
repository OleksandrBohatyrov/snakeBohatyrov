using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NAudio;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace snakeBohatyrov
{
    
    class SnakeGame
    {
        private int score;
        private int speed;
        private DateTime startTime;


        public SnakeGame()
        {
            score = 0;
            speed = 100; // Установка значения по умолчанию
            startTime = DateTime.Now;
        }

        public TimeSpan GetElapsedTime()
        {
            return DateTime.Now - startTime;
        }

        public void EatFood()
        {
            score++; // Прибавляем 1 к результату
        }

        public int GetScore()
        {
            return score;
        }


        public int GetSpeed() // Получение значения скорости
        {
            return speed;
        }

        public void SetSpeed(int newSpeed) // Установка значения скорости
        {
            speed = newSpeed;
        }

        public void PrintScore()
        {
           
            int originalCursorLeft = Console.CursorLeft;
            int originalCursorTop = Console.CursorTop;

            Console.SetCursorPosition(Console.WindowWidth - 10, 0); // Установка позиции курсора в правый верхний угол

            Console.ForegroundColor = ConsoleColor.Red; // Установка красного цвета вывода
            Console.Write("Score: " + score);
            Console.ResetColor(); // Сброс цвета вывода

            Console.SetCursorPosition(Console.WindowWidth - 30, 0); // Позиция для вывода времени жизни

            TimeSpan elapsedTime = GetElapsedTime();
            TimeSpan roundedTime = TimeSpan.FromSeconds(Math.Round(elapsedTime.TotalSeconds));
            Console.Write("Time: " + roundedTime.ToString(@"hh\:mm\:ss"));


            Console.SetCursorPosition(originalCursorLeft, originalCursorTop); // Возвращение курсора на исходную позицию
            
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            SnakeGame snakeGame = new SnakeGame();
            Console.WriteLine("Tere tulemast Serpentine'i mängu!");

            Console.Write("Write speed: ");
            int speed = int.Parse(Console.ReadLine());
            snakeGame.SetSpeed(speed);
            

            Console.Write("Write your name: ");
            string playerName = Console.ReadLine();

            Console.WriteLine($"Tere, {playerName}! Ole valmis mängima.");
            Console.Clear();
            


            Console.SetWindowSize(80, 25);

            Walls walls = new Walls(80, 25);
            walls.Draw();

            // Отрисовка точек			
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Point p = new Point(4, 5, '●', ConsoleColor.Green);
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();

            FoodCreator foodCreator = new FoodCreator(80, 25, '$', ConsoleColor.Green);
                
            Point food = foodCreator.CreateFood();
            food.Draw();

            sound mäng = new sound();
            ConsoleKeyInfo nupp = new ConsoleKeyInfo();
            _ = mäng.Tagaplaanis_Mangida("../../../back.wav");
            DateTime startTime = DateTime.Now;

            while (true)
            {
                snakeGame.PrintScore();

                if (walls.IsHit(snake) || snake.IsHitTail())
                {

                    break;

                }
                if (snake.Eat(food))
                {

                    _ = mäng.Natuke_mangida("../../../eat.wav");
                    snakeGame.EatFood(); // Увеличить счет при поедании еды

                    // Создание новой еды
                    food = foodCreator.CreateFood();
                    food.Draw();
                }
                else
                {
                    
                    snake.Move();
                    Thread.Sleep(snakeGame.GetSpeed()); // Скорость
                }

                Thread.Sleep(100);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }
            WriteGameOver();

            Console.ReadLine();

            PlayerResultsManager manager = new PlayerResultsManager();

            int gameScore = snakeGame.GetScore();
            TimeSpan elapsedTime = DateTime.Now - startTime;


            List<PlayerResult> playerResults = new List<PlayerResult>();

            if (File.Exists("../../../results.txt"))
            {
                playerResults = manager.LoadResultsFromFile("../../../results.txt");
            }

            playerResults.Add(new PlayerResult { Name = playerName, Score = gameScore, Time = elapsedTime });

            manager.SaveResultsToFile(playerResults, "../../../results.txt");

            Console.WriteLine("Results :");
            foreach (PlayerResult result in playerResults)
            {
                Console.WriteLine($"{result.Name}: {result.Score} {result.Time}");
            }
        }

        static void WriteGameOver()
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            WriteText("Game Over", xOffset + 1, yOffset++);
            yOffset++;
            WriteText("Autor: Oleksandr Bohatyrov", xOffset + 2, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            sound over = new sound();
            _ = over.Natuke_mangida("../../../over.wav");
            
        }


        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
        


    }
}