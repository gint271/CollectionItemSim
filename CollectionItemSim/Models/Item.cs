using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionItemSim.Models
{

    public enum Rarity
    {
        BLUE,
        PURPLE,
        YELLOW
    }

    public class Item
    {
        public Rarity rarity;
        public int level; // Starts at 0, max is 15
        public int xp; // Thik the max is always 1000 for blue, don't know purple.
        public static Random rand = new Random();

        public Item(Rarity rarity)
        {
            if (rarity != Rarity.BLUE)
                throw new NotImplementedException("Don't know enough about purple/yellow collection items yet.");

            this.rarity = rarity;
            this.level = 0;
            this.xp = 0;
        }

        public void UseMaintenanceKit(Rarity kitRarity)
        {
            if (level == 15)
            {
                throw new InvalidOperationException("Item is already at max level");
            }

            double roll = rand.NextDouble() * 100;
            double superSuccessChance;
            int xpGain;
            //TODO: Add in yellow favorite items later. Is 3000 consistent for purples?
            int xpRequired = this.rarity == Rarity.BLUE ? 1000 : 3000;

            

            //TODO: Are these values the same when used against a purple item?
            switch(kitRarity)
            {
                case Rarity.BLUE:
                    superSuccessChance = 17.6;
                    xpGain = 200;
                    break;
                case Rarity.PURPLE:
                    superSuccessChance = 55;
                    xpGain = 500;
                    break;
                case Rarity.YELLOW:
                    superSuccessChance = 100;
                    xpGain = 1000;
                    break;
                default:
                    throw new ArgumentException("Invalid rarity");
            }

            if (roll < superSuccessChance)
            {
                Debug.WriteLine($"Roll of {roll} was less than requirement of {superSuccessChance}");
                int tier = level / 5;

                //TODO: Assume xp is set to 0 after super success.
                level = 5 * (tier + 1);
                xp = 0;

                Debug.WriteLine($"Item is now level {level}");
                return;
            } else
            {
                xp += xpGain;
                if (xp >= xpRequired)
                {
                    /*
                     * Excess xp will be removed when reaching Lv. 5, etc
                     */
                    level++;

                    if (level % 5 == 0)
                    {
                        xp = 0;
                    }
                    else
                    {
                        xp -= xpRequired;
                    }
                }

                Debug.WriteLine($"Item is now level {level} with {xp} xp");

                return;
            }
        }
    }
}
