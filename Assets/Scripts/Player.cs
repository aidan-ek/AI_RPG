using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Stats.weapon = new Weapon("Training Sword", 15);
        Stats.offhand = new Offhand("Wooden Shield", 0, 0, 3);
        Stats.helmet = new Helmet("Leather Cap", 0, 0, 2, 15);
        Stats.chestplate = new Chestplate("Cloth Shirt", 0, 0, 1, 15);
        Stats.boots = new Boots("Leather Shoes", 0, 0, 2, 15);
        Stats.health = Stats.getMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static class Stats
    {
        public static int health = 1;
        public static Weapon weapon;
        public static Offhand offhand;
        public static Helmet helmet;
        public static Chestplate chestplate;
        public static Boots boots;


        public static int getMaxHealth()
        {
            return 5 + helmet.health + chestplate.health + boots.health;
        }
        public static int getPhysicalAmp()
        {
            return weapon.physicalAmp + helmet.physicalAmp + chestplate.physicalAmp + boots.physicalAmp + offhand.physicalAmp;
        }
        public static int getDefense()
        {
            return offhand.defense + helmet.defense + chestplate.defense + boots.defense;
        }

    }

    public class Weapon
    {
        public string name;
        public int physicalAmp;
        //public Skill skill;

        public Weapon(string wName, int physAmp)
        {
            this.name = wName;
            this.physicalAmp = physAmp;
        }
    }

    public class Offhand
    {
        public string name;
        public int magicAmp;
        public int physicalAmp;
        public int defense;

        public Offhand(string oName, int magAmp, int physAmp, int def)
        {
            this.name = oName;
            this.magicAmp = magAmp;
            this.defense = def;
            this.physicalAmp = physAmp;
            /*if (magAmp > def)
            {
                this.magicAmp = (int)((float)magAmp * 1.2);
                this.defense = 0;
                this.physicalAmp = 0;
            } else
            {
                this.magicAmp = 0;
                this.defense = def;
                this.physicalAmp = physAmp;
            }*/
        }
    }

    public abstract class Armour
    {
        public string name;
        public int physicalAmp;
        public int magicAmp;
        public int defense;
        public int health;
    }

    public class Helmet : Armour
    {
        public Helmet(string hName, int physAmp, int magAmp, int def, int hp)
        {
            this.name = hName;
            this.physicalAmp = physAmp;
            this.magicAmp = magAmp;
            this.defense = def;
            this.health = hp;
        }
    }

    public class Chestplate : Armour
    {
        public Chestplate(string cName, int physAmp, int magAmp, int def, int hp)
        {
            this.name = cName;
            this.physicalAmp = physAmp;
            this.magicAmp = magAmp;
            this.defense = def;
            this.health = hp;
        }
    }

    public class Boots : Armour
    {
        public Boots(string bName, int physAmp, int magAmp, int def, int hp)
        {
            this.name = bName;
            this.physicalAmp = physAmp;
            this.magicAmp = magAmp;
            this.defense = def;
            this.health = hp;
        }
    }

    /*    abstract class Skill
    {
        public string name;
        public string description;
        public abstract void effect();

        public Skill(string skillName)
        {
            name = skillName;
        }
    }

    class Rage : Skill
    {
        public string name = 
        public void effect(Stats p, Enemy, e)
        {
            e.health -= p.physicap.yourHealth
        }
    }*/

}
