using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Stats.name = "GOOBA";
        Stats.species = "Goblin";
        Stats.maxHealth = 25;
        Stats.health = 25;
        Stats.physicalAmp = 10;
        Stats.defense = 5;
        Stats.magicAmp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static class Stats {
        public static string name;
        public static string species;
        public static int maxHealth;
        public static int health;
        public static int physicalAmp;
        public static int magicAmp;
        public static int defense;
    }
}
