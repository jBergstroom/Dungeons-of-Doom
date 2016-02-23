using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Weapon : Item
    {
        public Weapon(int weight): base (SwordName(), weight)
        {
            AttackDamage = SwordDamage();
        }

        public int AttackDamage { get; set; }

        public override void PickUp(Player player)
        {
            player.AttackDamage += 10;
            base.PickUp(player);
        }

        public static string SwordName()
        {
            Random rand = new Random();

            string[] names = {"Ashbringer", "The Grandfather", "Smörkniv", "Tooth Pick", "Master Sword", "Wooden stick", "Triforce", "Buster Sword", "Lightsaber", "Deaths Bite"};

            return names[rand.Next(0, 10)];
        }
        public static int SwordDamage()
        {
            Random rand = new Random();
            return rand.Next(10, 31);
        }
    }
}
