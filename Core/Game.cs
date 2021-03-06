﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Utils;


namespace Core
{
    public class Game
    {

        //Ska representera spelflödet
        const int WorldWidth = 20;
        const int WorldHeight = 10;
        public static int difficulty = 3;
        Player player;
        public bool endGame = false;

        Room[,] world;

        public void Start()
        {//efter att main har anropat denna så ska denna ta över
            StartScreen();
            Console.ForegroundColor = ConsoleColor.Red;
            CreatePlayer();
            do
            {
                CreateWorld();
                bool playingGame = true;
                do
                {
                    Console.Clear();
                    DisplayStats();

                    DisplayWorld();
                    AskForMovement();
                    playingGame = HasWon(playingGame);
                } while (playingGame);

            } while (endGame == false);
        }

        private bool HasWon(bool playingGame)
        {
            if (player.Health <= 0)
            {
                GameOver("lost");
                return false;
            }
            else if (Monster.monsterAmount <= 0)
            {
                GameOver("won");
                return false;
            }
            return true;
        }

        private void StartScreen()
        {
            string[] intro = File.ReadAllLines("DungeonsOfDoomIntro.txt");

            for (int i = 0; i < intro.Length; i++)
            {
                Console.ForegroundColor = IntroColor(i);
                Console.WriteLine(intro[i]);
                if (i % 3 == 0)
                {
                    Thread.Sleep(100); //temp 1 for debugg, original 200
                }
            }
            Console.ReadKey();
        }

        private ConsoleColor IntroColor(int i)
        {
            if (i < 5)
            {
                return ConsoleColor.White;
            }
            else if (i < 10 && i > 4)
            {
                return ConsoleColor.Gray;
            }
            else if (i < 13 && i > 9)
            {
                return ConsoleColor.DarkGray;
            }
            else if (i == 13)
            {
                return ConsoleColor.DarkGreen;
            }
            else if (i > 13 && i < 18)
            {
                return ConsoleColor.DarkRed;
            }
            else if (i > 17 && i < 22)
            {
                return ConsoleColor.Red;
            }
            else if (i > 21 && i < 31)
            {
                return ConsoleColor.DarkYellow;
            }
            else if (i > 30 && i < 34)
            {
                return ConsoleColor.Magenta;
            }
            else if (i > 33 && i < 36)
            {
                return ConsoleColor.DarkMagenta;
            }
            else if (i > 35 && i < 38)
            {
                return ConsoleColor.DarkCyan;
            }
            else if (i > 37 && i < 40)
            {
                return ConsoleColor.Cyan;
            }
            else 
                return ConsoleColor.White;
        }

        private void DisplayStats()
        {
            Console.WriteLine($"LEVEL: {difficulty-2}");
            Console.WriteLine($"Name: {player.Name}");
            Console.WriteLine($"Health: {player.Health}");
            Console.WriteLine($"Attack Damage: {player.AttackDamage}");

            Console.WriteLine($"Position: {player.X},{player.Y}");
            Console.WriteLine("Backpack content:");
            int totalWeight = 0;
            foreach (ILuggable item in player.BackPack)
            {
                Console.Write($"{item.Name}({item.Weight} kg), ");
                totalWeight += item.Weight;
            }
            Console.WriteLine();
            Console.WriteLine("Total weight: " + totalWeight);
            Console.WriteLine();
        }

        private void AskForMovement()
        {
            ConsoleKeyInfo keyinfo = Console.ReadKey();
            int x = player.X;
            int y = player.Y;

            switch (keyinfo.Key)
            {
                case ConsoleKey.RightArrow: x++; break;
                case ConsoleKey.LeftArrow: x--; break;
                case ConsoleKey.UpArrow: y--; break;
                case ConsoleKey.DownArrow: y++; break;
            }


            if (x >= 0 && x < WorldWidth && y >= 0 && y < WorldHeight && world[x, y].Wall == false)
            {
                player.X = x;
                player.Y = y;
            }

            
        }

        private void FoundItem()//lägg i modifyplayer, override att vi inte ska lägga till potions i backpacken
        {
            Console.WriteLine($"You have found an item. It's a {world[player.X, player.Y].ItemInRoom.Name}. Do you want to pick up? (Y/N)");
            while (true)
            {
            ConsoleKeyInfo uInput = Console.ReadKey();
                if (uInput.Key == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    Console.WriteLine($"You have picket up {world[player.X, player.Y].ItemInRoom.Name}");
                    world[player.X, player.Y].ItemInRoom.PickUp(player);
                    world[player.X, player.Y].ItemInRoom = null;
                    break;
                }
                else if (uInput.Key == ConsoleKey.N)
                    break;
            }

        }

        private void GameOver(string condition)
        {
            if (condition == "lost")
            {
                Console.Clear();
                Console.WriteLine("Ha ha, you loose. The evil sorcerrer has conquered the world.");
                endGame = true;
                Console.ReadKey();
            }
            else if (condition == "won")
            {
                Console.Clear();
                Console.WriteLine("Congratulations adventurer, you have vanquished all the monsters! Want to continue down? (Y/N)");
                bool loop = true;
                do
                {
                    ConsoleKeyInfo winConInput = Console.ReadKey();
                    switch (winConInput.Key)
                    {
                        case ConsoleKey.Y:
                            difficulty++;
                            loop = false;
                            break;
                        case ConsoleKey.N:
                            endGame = true;
                            loop = false;
                            break;
                    }
                } while (loop == true);

            }
        }

        private void CreateWorld()
        {
            world = new Room[WorldWidth, WorldHeight];

            for (int y = 0; y < WorldHeight; y++)
            {
                for (int x = 0; x < WorldWidth; x++)
                {
                    world[x, y] = new Room();
                    world[x, y].Discovered = false;
                }
            }
            //Skapa väggar
            world[1, 1].Wall = true;
            world[1, 2].Wall = true;
            world[2, 1].Wall = true;
            world[2, 2].Wall = true;
            world[3, 3].Wall = true;
            world[3, 4].Wall = true;
            world[4, 5].Wall = true;
            world[4, 6].Wall = true;
            //Placerar ut items och monster
            int m = 0;
            do
            {
                int monsterX = RandomUtils.GetRandom(0, WorldWidth);
                Thread.Sleep(20);
                int monsterY = RandomUtils.GetRandom(0, WorldHeight);
                if (m % 3 == 0 && world[monsterX, monsterY].MonsterInRoom == null && world[monsterX, monsterY].Wall == false)
                {
                    world[monsterX, monsterY].MonsterInRoom = new Gremlin("Gremlin", 5);
                    m++;
                }
                else
                {
                    if (world[monsterX, monsterY].MonsterInRoom == null && world[monsterX, monsterY].Wall == false)
                    {
                        world[monsterX, monsterY].MonsterInRoom = new Monster("Ogre", 190);
                        m++;
                    }
                }
            } while (m < difficulty);
            int p = 0;
            do
            {//Potions
                int itemX = RandomUtils.GetRandom(0, WorldWidth);
                Thread.Sleep(20);
                int itemY = RandomUtils.GetRandom(0, WorldHeight);

                if (world[itemX, itemY].ItemInRoom == null && world[itemX, itemY].Wall == false)
                {
                    world[itemX, itemY].ItemInRoom = new Potion("Healing potion", 1, 20);
                    p++;
                }
            } while (p < 4);

            int w = 0;
            do
            {//Vapen
                int itemX = RandomUtils.GetRandom(0, WorldWidth);
                Thread.Sleep(20);
                int itemY = RandomUtils.GetRandom(0, WorldHeight);

                if (world[itemX, itemY].ItemInRoom == null && world[itemX, itemY].Wall == false)
                {
                    world[itemX, itemY].ItemInRoom = new Weapon(5);
                    w++;
                }
            } while (w < 4);
        }

        private void CreatePlayer()
        {
            player = new Player("Player", 100, 10, 120);
        }

        private void DisplayWorld()
        {
            PrintBoarder();

            for (int y = 0; y < WorldHeight; y++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Black;

                for (int x = 0; x < WorldWidth; x++)
                {
                    if (player.X == x && player.Y == y)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($" {player.Name.Substring(0, 1)}");
                        Console.BackgroundColor = ConsoleColor.Black; ;
                    }
                    else
                    {
                        Room room = world[x, y];
                        if (room.Wall == true)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.Write("  ");
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        else if (room.Discovered == true)
                        {
                            if (room.MonsterInRoom != null && room.MonsterInRoom.Health > 0)
                            {
                                Monster monster = room.MonsterInRoom;
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.Write($" {monster.Name.Substring(0, 1)}");
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                            else if (room.ItemInRoom != null)
                            {
                                Item item = room.ItemInRoom;
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.Write(" I");
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                            else {
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                                Console.Write("  ");
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                        }
                        else
                        {
                            Console.Write("  ");
                        }
                    }
                }
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
            PrintBoarder();
            CheckRoomContent();
        }

        private static void PrintBoarder()
        {
            for (int i = 0; i < WorldWidth + 2; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine();
        }

        private void CheckRoomContent()
        {
            world[player.X, player.Y].Discovered = true;
            //world[player.X, player.Y].discovered = true;
            if (world[player.X, player.Y].MonsterInRoom != null && world[player.X, player.Y].MonsterInRoom.Health > 0)
            {
                Encounter();
            }
            else if (world[player.X, player.Y].ItemInRoom != null)
            {
                FoundItem();
            }
        }

        private void Encounter()
        {

            Console.WriteLine($"You have encountered {world[player.X, player.Y].MonsterInRoom.Name}. BATTLE!");
            do
            {
                Console.WriteLine("Press any key to attack the monster!");
                Console.ReadKey();
                Console.WriteLine(player.Hit(world[player.X, player.Y].MonsterInRoom));

                if (world[player.X, player.Y].MonsterInRoom.Health > 0)
                {
                    Console.WriteLine(world[player.X, player.Y].MonsterInRoom.Hit(player));
                }
                else
                {
                    Console.WriteLine($"You have killed the {world[player.X, player.Y].MonsterInRoom.Name}!");
                }
            } while (player.Health > 0 && world[player.X, player.Y].MonsterInRoom.Health > 0);

            if (player.Health > 0)
            {
                player.BackPack.Add(world[player.X, player.Y].MonsterInRoom);
                world[player.X, player.Y].MonsterInRoom = null;
                Monster.monsterAmount--;
            }
            else
            {
                GameOver("lost");
            }
        }
    }
}
