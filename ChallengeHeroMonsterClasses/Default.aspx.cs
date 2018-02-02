using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChallengeHeroMonsterClasses
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Character hero = new Character("Spiderman", 100, 5);
            Character monster = new Character("Green Goblin", 150, 6);

            //Character[] charactersInBattle = new Character[] { hero, monster };
            Battle(hero,monster);
            DisplayFinalBattleResults(hero, monster);
        }

        private void Battle(Character c1, Character c2)
        { 
            int roundCount = 0;
            battleResult.Text += "<br /> ===BATTLE BEGIN=== <br /> <br />";
            Dice battleDice = new Dice();

            while(c1.Health > 0 && c2.Health > 0)
            {
                roundCount++;
                battleResult.Text += "-Round " + roundCount.ToString() + "- <br /> --------------------------- <br />";

                //calculate round attack damage and update health of characters based on attack
                int c1_roundAttack = c1.Attack(battleDice);
                int c2_roundAttack = c2.Attack(battleDice);
                string battleSummary = "";

                //DISPLAY BATTLE RESULTS
                //c1 attack

                //check for bonus attack
                if(c1_roundAttack == c1.DamageMaximum)
                {
                    int bonusDmg = c1.DamageMaximum / 2;
                    c2.Defend(c1_roundAttack + bonusDmg);

                    battleSummary += String.Format("{0} attacked inflicting {1} damage plus {2} bonus damage. <br />" +
                    "{3} now has {4} health. <br /> <br />", c1.Name, c1_roundAttack, bonusDmg, c2.Name, c2.Health);
                }
                else
                {
                    c2.Defend(c1_roundAttack);

                    battleSummary += String.Format("{0} attacked inflicting {1} damage. <br />" +
                   "{2} now has {3} health. <br /> <br />", c1.Name, c1_roundAttack, c2.Name, c2.Health);
                }

                //c2 attack
                //check for bonus attack
                if (c2_roundAttack == c2.DamageMaximum)
                {
                    int bonusDmg = c2.DamageMaximum / 2;
                    c1.Defend(c2_roundAttack + bonusDmg);

                    battleSummary += String.Format("{0} attacked inflicting {1} damage plus {2} bonus damage. <br />" +
                    "{3} now has {4} health. <br /> <br />", c2.Name, c2_roundAttack, bonusDmg, c1.Name, c1.Health);
                }
                else
                {
                    c1.Defend(c2_roundAttack);
                    battleSummary += String.Format("{0} attacked inflicting {1} damage. <br />" +
                   "{2} now has {3} health. <br /> <br />", c2.Name, c2_roundAttack, c1.Name, c1.Health);
                }

                battleResult.Text += battleSummary;
            }
        }

        private void DisplayFinalBattleResults(Character c1, Character c2)
        {
            string finalBattleResult = "The battle has ended <br />";

            if (c1.Health > 0)
            {
                finalBattleResult += "The hero lives and is victorious!";
            }
            else if(c2.Health > 0)
            {
                finalBattleResult += "The monster lives and is victorious!";
            }
            else
            {
                finalBattleResult += "Both the hero and the monster have perished";
            }

            finalBattleResultLabel.Text += finalBattleResult;
        }
    }

    public class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int DamageMaximum { get; set; }
        public bool AttackBonus { get; set; }

        public Character(string name, int health, int damageMaximum)
        {
            this.Name = name; this.Health = health; this.DamageMaximum = damageMaximum;
        }

        public int Attack(Dice dice)
        {
            dice.Sides = this.DamageMaximum;
            return dice.Roll();
        }

        public void Defend(int damage)
        {
            this.Health -= damage;
        }


    }
    public class Dice
    {
        public int Sides { get; set; }

        Random rand = new Random();
        public int Roll()
        {
            
            return rand.Next(1, this.Sides + 1); //plus 1 because Random.Next() only goes up to the second parameter.
        }
    }
}