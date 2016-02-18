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
                player.Health--;

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
                }
            }
            //Placerar ut ett monster i världen
            Random createRand = new Random();
            int k = 0;
            do
            {
                int monsterX = createRand.Next(0, WorldWidth);
                Thread.Sleep(20);
                int monsterY = createRand.Next(0, WorldHeight);

                if (world[monsterX, monsterY].MonsterInRoom == null)
                {
                    int monsterType = createRand.Next(0, 10);
                    world[monsterX, monsterY].MonsterInRoom = new Monster(monsterType, 30, 10);
                }
            } while (k > 7);

            //world[2, 2].MonsterInRoom = new Monster("Monster", 30, 10);

            world[4, 4].ItemInRoom = new Item("Sword", 10);
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
                    if (player.X == x && player.Y == y)
                    {
                        Console.Write("|P|");
                    }
                    else if (room.MonsterInRoom != null)
                    {
                        Monster monster = room.MonsterInRoom;
                        Console.Write("|M|");
                    }
                    else if (room.ItemInRoom != null)
                    { Item item = room.ItemInRoom; Console.Write("|I|"); }
                    else { Console.Write("|o|"); }
                }
                Console.WriteLine();
            }
            CheckRoomContent();
        }

        private void CheckRoomContent()
        {
            if (world[player.X, player.Y].MonsterInRoom != null)
            {
                MonsterEncounter();

            }
            else if (world[player.X, player.Y].ItemInRoom != null)
            {
                foundItem();
            }
        }

        private void MonsterEncounter()
        {
            Console.Clear();
            Monster thisMonster = world[player.X, player.Y].MonsterInRoom;
            Console.WriteLine($"You have encountered {world[player.X, player.Y].MonsterInRoom.Name}. BATTLE!");
            Console.Beep(15000, 500);
            Random rng = new Random();

            do
            {
                Console.WriteLine("Press any key to attack the monster!");
                Console.ReadKey();
                int hitVal = rng.Next(1, 21);
                if (hitVal > 10)
                {
                    int damage = rng.Next((player.AttackDamage/4), player.AttackDamage +1);
                    Console.WriteLine($"You rolled {hitVal}! You strike the monster for {damage}!");
                    thisMonster.Health -= damage;
                    if (thisMonster.Health <= 0)
                    {
                        Console.WriteLine("You have killed the monster!");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine($"You rolled {hitVal}. You miss!");
                }

                int monsterHit = rng.Next(1, 101);
                if (monsterHit > 70)
                {
                    int monsterdamage = rng.Next(thisMonster.AttackDamage / 2, thisMonster.AttackDamage);
                    Console.WriteLine($"{thisMonster.Name} hits you back. {thisMonster.Name} hits you for {monsterdamage}");
                    player.Health -= monsterdamage;
                    
                }

            } while (thisMonster.Health > 0 || player.Health > 0);
            world[player.X, player.Y].MonsterInRoom = null;
        }
    }
}
