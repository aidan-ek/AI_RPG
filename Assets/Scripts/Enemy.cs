using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   

    public static EnemyInstance currentEnemy;

    public class EnemyInstance {
        public string name;
        public string species;
        public int maxHealth;
        public int health;
        public int physicalAmp;
        public int magicAmp;
        public int defense;
        public string skill;

        public EnemyInstance(string eName, string eSpecies, int maxHP, int physAmp, int magAmp, int def, string eSkill) {
            this.name = eName;
            this.species = eSpecies;
            this.maxHealth = maxHP;
            this.health = maxHP;
            this.physicalAmp = physAmp;
            this.magicAmp = magAmp;
            this.defense = def;
            this.skill = eSkill;
        }
    }

    
}
