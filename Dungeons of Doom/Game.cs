using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    class Game
    {

        //Ska representera spelflödet
        const int WorldWidth = 20;
        const int WorldHeight = 10;

        Player player;

        Room[,] world;

        public void Start()
        {//efter att main har anropat denna så ska denna ta över
            CreatePlayer();
            CreateWorld();

            do
            {
                Console.Clear();
                DisaplayStats();

                DisplayWorld();
                AskForMovement();
                //player.Health--;

            } while (player.Health > 0);


            GameOver();

        }

        private void DisaplayStats()
        {
            Console.WriteLine($"Name: {player.Name}");
            Console.WriteLine($"Health: {player.Health}");
            Console.WriteLine($"Attack Damage: {player.AttackDamage}");

            Console.WriteLine($"Position: {player.X},{player.Y}");
            Console.WriteLine("Backpack content:");
            foreach (Item item in player.BackPack)
            {
                Console.Write($"{item.Name} ");
            }
            Console.WriteLine();
        }

        private void AskForMovement()
        {
            Console.WriteLine("Move!");
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


            if (x >= 0 && x < WorldWidth && y >= 0 && y < WorldHeight)
            {
                player.X = x;
                player.Y = y;
                world[player.X, player.Y].discovered = true;

            }

        }



        private void foundItem()
        {
            Console.WriteLine($"You have found an item. It's a {world[player.X, player.Y].ItemInRoom.Name}. Do you want to pick up? (Y/N)");
            ConsoleKeyInfo uInput = Console.ReadKey();

            if (uInput.Key == ConsoleKey.Y)
            {
                player.BackPack.Add(world[player.X, player.Y].ItemInRoom);
                Console.WriteLine($"You have picket up {world[player.X, player.Y].ItemInRoom.Name}");
                world[player.X, player.Y].ItemInRoom = null;
                player.AttackDamage += 20;
            }

        }

        private void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Ha ha, you loose. The evil sorcerrer has conquered the world.");
        }

        private void CreateWorld()
        {
            world = new Room[WorldWidth, WorldHeight];

            for (int y = 0; y < WorldHeight; y++)
            {
                for (int x = 0; x < WorldWidth; x++)
                {
                    world[x, y] = new Room();
                    world[x, y].discovered = false;
                }
            }
            //Placerar ut ett monster i världen
            Random createRand = new Random();
            int m = 0;
            do
            {
                int monsterX = createRand.Next(0, WorldWidth);
                Thread.Sleep(20);
                int monsterY = createRand.Next(0, WorldHeight);

                if (world[monsterX, monsterY].MonsterInRoom == null)
                {
                    world[monsterX, monsterY].MonsterInRoom = new Monster("Monster", 30, 10);
                }
                m++;
            } while (m < 3);

            //world[2, 2].MonsterInRoom = new Monster("Monster", 30, 10);

            world[4, 4].ItemInRoom = new weapons("Sword", 2, 10);
        }

        private void CreatePlayer()
        {
            player = new Player("Player", 100, 10);
        }

        private void DisplayWorld()
        {
            for (int y = 0; y < WorldHeight; y++)
            {
                for (int x = 0; x < WorldWidth; x++)
                {

                    Room room = world[x, y];
                    if (room.discovered == true)
                    {
                        if (player.X == x && player.Y == y)
                        {
                            world[player.X, player.Y].discovered = true;
                            Console.Write($"|{player.Name.Substring(0, 1)}|");
                        }
                        else if (room.MonsterInRoom != null && room.MonsterInRoom.Health > 0)
                        {
                            Monster monster = room.MonsterInRoom;
                            Console.Write($"|{monster.Name.Substring(0, 1)}|");
                        }
                        else if (room.ItemInRoom != null)
                        { Item item = room.ItemInRoom; Console.Write("|I|"); }
                        else { Console.Write("| |"); }
                    }
                    else
                    {
                        Console.Write("| |");
                    }

                }
                Console.WriteLine();
            }
            CheckRoomContent();
        }

        private void CheckRoomContent()
        {
            //world[player.X, player.Y].discovered = true;
            if (world[player.X, player.Y].MonsterInRoom != null && world[player.X, player.Y].MonsterInRoom.Health > 0)
            {
                Encounter();
            }
            else if (world[player.X, player.Y].ItemInRoom != null)
            {
                foundItem();
            }
        }

        private void Encounter()
        {
            do
            {
                player.Hit(world[player.X, player.Y].MonsterInRoom);
                if (world[player.X, player.Y].MonsterInRoom.Health > 0)
                {
                    world[player.X, player.Y].MonsterInRoom.Hit(player);
                }
                

            } while (player.Health > 0 && world[player.X, player.Y].MonsterInRoom.Health > 0);

        }
    }
}
