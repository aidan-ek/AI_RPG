using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Cache;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Text.Json;


public class GameEngine : MonoBehaviour
{
    // constants
    const double DEF_MODIFIER = 0.985;

    // buttons
    public GameObject attackBox;
    public GameObject skillBox;
    public GameObject infoBox;
    public Button attackButton;
    public Button skillButton;
    public Button infoButton;

    // text values
    public TMP_Text attackText;
    public TMP_Text pHealthText;
    public TMP_Text eHealthText;
    public TMP_Text narrator;
    public TMP_Text enemyName;
    public TMP_Text playerName;
    string narratorFinal; // used to slow type the narrator text

    // game logic
    bool yourTurn = true;
    bool init = false;
    int delayTimer = 0;
    Enemy.EnemyInstance genEnemy;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0; // disable vsync
        Application.targetFrameRate = 30; // caps framerate at 30

        // detects button presses
        attackButton.onClick.AddListener(AttackPress);
        attackButton.onClick.AddListener(SkillPress);
        attackButton.onClick.AddListener(InfoPress);

        // Generates initial enemies
        GenerateEnemies();
        

    }

    // Update is called once per frame
    void Update()
    {  
        if (init) {
            UpdateNarrator();
            
            // updates all text values on screen
            enemyName.text = Enemy.currentEnemy.name;
            playerName.text = Player.Stats.name;
            eHealthText.text = Enemy.currentEnemy.health + "/" + Enemy.currentEnemy.maxHealth;
            pHealthText.text = Player.Stats.health + "/" + Player.Stats.getMaxHealth();
            attackText.text = "ATTACK (" + Player.Stats.getPhysicalAmp() + ")";
            
            // hides buttons when no action available
            attackBox.SetActive(yourTurn && delayTimer == 0);
            skillBox.SetActive(yourTurn && delayTimer == 0);
            infoBox.SetActive(yourTurn && delayTimer == 0);

            // checks if the text is done writing and if the delay between turns is up
            if (string.IsNullOrEmpty(narratorFinal)) {
                if (delayTimer > 0) {
                    delayTimer--;
                } else if (delayTimer == 0) {
                    if (!yourTurn) {
                        enemyTurn();
                    }
                    
                }
                
            }
        } else {
            narrator.text = "Enemies Generating...";
            attackBox.SetActive(false);
            skillBox.SetActive(false);
            infoBox.SetActive(false);
        }

    }

    // triggers on each button press
    void AttackPress() {
        SlowText(Player.Stats.name + " attacked " + Enemy.currentEnemy.name + " for " + damageEnemy(Player.Stats.getPhysicalAmp()) + " damage.");
        yourTurn = false;
        delayTimer = 50;
    }
    void SkillPress() {
        
    }
    void InfoPress() {
        
    }

    // execute during enemy's turn
    void enemyTurn() {
        if (Enemy.currentEnemy.health > 0) {
            SlowText(Enemy.currentEnemy.name + " attacked " + Player.Stats.name + " for " + damagePlayer(Enemy.currentEnemy.physicalAmp) + " damage.");
            yourTurn = true;
        } else {
            enemyDeath();
        }
        delayTimer = 50;
    }
    
    // run on enemy death
    void enemyDeath() {
        SlowText(Enemy.currentEnemy.name + " died. You won!");
        Enemy.currentEnemy = genEnemy;
        GenerateEnemies();
        yourTurn = true;
        // add loot choice here and next enemy spawn, etc.
    }

    // calculates, and damages the enemy, also returning the damage dealt after defense
    int damageEnemy(int dmg) {
        int output = (int)((double)dmg * Math.Pow(DEF_MODIFIER, Enemy.currentEnemy.defense));
        Enemy.currentEnemy.health -= output;
        return output;
    }
    int damagePlayer(int dmg) {
        int output = (int)((double)dmg * Math.Pow(DEF_MODIFIER, Player.Stats.getDefense()));
        Player.Stats.health -= output;
        return output;
    }

    // adds one letter of the text output per frame to give a typing effect
    void SlowText(string input) {
        narrator.text = "";
        narratorFinal = input;
    }
    // updates the slow typing text
    void UpdateNarrator() {
        if (!string.IsNullOrEmpty(narratorFinal)) {
            narrator.text += narratorFinal.Substring(0, 1);
            narratorFinal = narratorFinal.Substring(1, narratorFinal.Length-1);
        }
    }

//-----------------------------------------------------------------------------------------
// Cohere API access
//-----------------------------------------------------------------------------------------
    private const string URI = "https://api.cohere.ai/v1/generate";

    public static string cohereOutput;

    public void GenerateEnemies()
    {
        UnityEngine.Debug.Log("generate_enemy");
        StartCoroutine(PostData_Coroutine());
    }

    IEnumerator PostData_Coroutine()
    {
        UnityEngine.Debug.Log("we are here");
        //using (UnityWebRequest request = UnityWebRequest.Post(URI, "{\"max_tokens\": 300, \"temperature\":2.0, \"prompt\": \"Create 5 enemies for a fantasy RPG each with the following attributes listed on separate lines, Name: unique name, Health: an Integer between 10 and 50, Damage: an Integer between 5 and 20, Magic Damage: an Integer between 5 and 20, Defense: an Integer between 1 to 10, Species: Choose from the following; Orc, Goblin, Giant, Elf, Undead, Dragon, Merfolk, Giant Spider, Special Ability: Choose from the following; Swiftness, Rage, Poison Spray, Fireball.\"}", "application/json"))
        using (UnityWebRequest request = UnityWebRequest.Post(URI, "{\"max_tokens\": 300, \"temperature\":2.0, \"prompt\": \"Create 1 enemy for a fantasy RPG with the following attributes listed on separate lines, Name: unique name, Health: an Integer between 10 and 50, Damage: an Integer between 5 and 20, Magic Damage: an Integer between 5 and 20, Defense: an Integer between 1 to 10, Species: Choose one from the following - Orc, Goblin, Giant, Elf, Undead, Dragon, Merfolk, Giant Spider, Special Ability: Choose one from the following - Swiftness, Rage, Poison Spray, Fireball, Blade Sharpen. Output these in JSON format.\"}", "application/json"))
        {
            request.SetRequestHeader("accept", "application/json");
            request.SetRequestHeader("authorization", "Bearer yOle6J3A5BHkXpIbS0AWrxnS8E4mPQisFcY6l792");
            UnityEngine.Debug.Log("in the using");
            yield return request.SendWebRequest();
            UnityEngine.Debug.Log("returned from wait");
            if (request.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.Log(request.error);
            }
            else
            {
                UnityEngine.Debug.Log("Form upload complete!");
            }
            string dirtyOutput = request.downloadHandler.text; 
            int first = dirtyOutput.IndexOf("text\":\" ");
            int last = dirtyOutput.IndexOf("}]");
            // cleaning output to be readable as an object
            cohereOutput = dirtyOutput.Substring(first + 8, last - first - 9);
            cohereOutput = cohereOutput.Replace("\\n","");
            cohereOutput = cohereOutput.Replace("  \\","");
            cohereOutput = cohereOutput.Replace("\\","");
            cohereOutput = cohereOutput.Replace("Special Ability","skill");
            cohereOutput = cohereOutput.Replace("Magic Damage","magicAmp");
            cohereOutput = cohereOutput.Replace("Damage","physicalAmp");
            cohereOutput = cohereOutput.Replace("Name","name");
            cohereOutput = cohereOutput.Replace("Health","maxHealth");
            cohereOutput = cohereOutput.Replace("Defense","defense");
            cohereOutput = cohereOutput.Replace("Species","species");
            UnityEngine.Debug.Log("COHERE: " + cohereOutput);
            var options = new JsonSerializerOptions { IncludeFields = true };
            CohereReturn newGen = JsonSerializer.Deserialize<CohereReturn>(cohereOutput);
            genEnemy = new Enemy.EnemyInstance(newGen.name, newGen.species, newGen.maxHealth, newGen.physicalAmp, newGen.magicAmp, newGen.defense, newGen.skill);
            UnityEngine.Debug.Log("ENEMY HEALTH: " + genEnemy.health);
            if (!init) {
                Enemy.currentEnemy = genEnemy;
                GenerateEnemies();
                init = true;
            }
        }

    }

    public class CohereReturn
    {
        public string name { get; set; }
        public int maxHealth { get; set; }
        public int physicalAmp { get; set; }
        public int magicAmp { get; set; }
        public int defense { get; set; }
        public string species { get; set; }
        public string skill { get; set; }
    }



}
   
