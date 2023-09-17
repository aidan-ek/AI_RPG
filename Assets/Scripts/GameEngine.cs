using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEngine : MonoBehaviour
{
    public GameObject attackButton;
    public GameObject skillButton;
    public GameObject infoButton;

    public TMP_Text attackText;
    public TMP_Text pHealthText;

    // Start is called before the first frame update
    void Start()
    {
        Player.weapon = new Weapon("Training Sword", 15);
        Player.offhand = new Offhand("Wooden Shield", 0, 0, 3);
        Player.helmet = new Helmet("Leather Cap", 0, 0, 2, 15);
        Player.chestplate = new Chestplate("Cloth Shirt", 0, 0, 1, 15);
        Player.boots = new Boots("Leather Shoes", 0, 0, 2, 15);
        Player.health = Player.getMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        pHealthText.text = Player.health + "/" + Player.getMaxHealth();
        attackText.text = "ATTACK (" + Player.getPhysicalAmp() + ")";
    }

    static class Player
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

    class Weapon
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

    class Offhand
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

    abstract class Armour
    {
        public string name;
        public int physicalAmp;
        public int magicAmp;
        public int defense;
        public int health;
    }

    class Helmet : Armour
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

    class Chestplate : Armour
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

    class Boots : Armour
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
        public void effect(Player p, Enemy, e)
        {
            e.health -= p.physicap.yourHealth
        }
    }*/
    
   
