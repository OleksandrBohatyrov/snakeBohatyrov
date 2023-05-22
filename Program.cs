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

        public SnakeGame()
        {
            score = 0;
            speed = 100; // Установка значения по умолчанию
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
    }

    class Program
    {
        static void Main(string[] args)
        {
            SnakeGame snakeGame = new SnakeGame();
            Console.WriteLine("Добро пожаловать в игру Змейка!");

            Console.Write("Write speed: ");
            int speed = int.Parse(Console.ReadLine());
            snakeGame.SetSpeed(speed);
            

            Console.Write("Write your name: ");
            string playerName = Console.ReadLine();

            Console.WriteLine($"Привет, {playerName}! Приготовьтесь к игре.");
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
            _ = mäng.Tagaplaanis_Mangida("C:\\Users\\opilane\\source\\repos\\snakeBohatyrov\\bin\\Debug\\net6.0\\Back.wav");

            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }
                if (snake.Eat(food))
                {
                    food = foodCreator.CreateFood();
                    food.Draw();
                    snakeGame.EatFood(); // Увеличить счет при поедании еды
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

            List<PlayerResult> playerResults = new List<PlayerResult>();

            if (File.Exists("C:\\Users\\opilane\\Source\\Repos\\snakeBohatyrov\\results.txt"))
            {
                playerResults = manager.LoadResultsFromFile("C:\\Users\\opilane\\Source\\Repos\\snakeBohatyrov\\results.txt");
            }

            playerResults.Add(new PlayerResult { Name = playerName, Score = gameScore });

            manager.SaveResultsToFile(playerResults, "C:\\Users\\opilane\\Source\\Repos\\snakeBohatyrov\\results.txt");

            Console.WriteLine("Результаты :");
            foreach (PlayerResult result in playerResults)
            {
                Console.WriteLine($"{result.Name}: {result.Score}");
            }
        }

        static void WriteGameOver()
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            WriteText("И Г Р А    О К О Н Ч Е Н А", xOffset + 1, yOffset++);
            yOffset++;
            WriteText("Автор: Oleksandr Bohatyrov", xOffset + 2, yOffset++);
            WriteText("============================", xOffset, yOffset++);
        }


        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
        


    }
}