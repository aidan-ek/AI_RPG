using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Cache;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;


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
    int delayTimer = 0;


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
        narrator.text = "Generating enemies...";
        while (string.IsNullOrEmpty(cohereOutput)) {}
        
        SlowText(Enemy.Stats.name + " appeared!");

    }

    // Update is called once per frame
    void Update()
    {  
        UpdateNarrator();

        // updates all text values on screen
        enemyName.text = Enemy.Stats.name;
        playerName.text = Player.Stats.name;
        eHealthText.text = Enemy.Stats.health + "/" + Enemy.Stats.maxHealth;
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


    }

    // triggers on each button press
    void AttackPress() {
        SlowText(Player.Stats.name + " attacked " + Enemy.Stats.name + " for " + damageEnemy(Player.Stats.getPhysicalAmp()) + " damage.");
        yourTurn = false;
        delayTimer = 50;
    }
    void SkillPress() {
        
    }
    void InfoPress() {
        
    }

    // execute during enemy's turn
    void enemyTurn() {
        if (Enemy.Stats.health > 0) {
            SlowText(Enemy.Stats.name + " attacked " + Player.Stats.name + " for " + damagePlayer(Enemy.Stats.physicalAmp) + " damage.");
            yourTurn = true;
        } else {
            enemyDeath();
        }
        delayTimer = 50;
    }
    
    // run on enemy death
    void enemyDeath() {
        SlowText(Enemy.Stats.name + " died. You won!");
        // add loot choice here and next enemy spawn, etc.
    }

    // calculates, and damages the enemy, also returning the damage dealt after defense
    int damageEnemy(int dmg) {
        int output = (int)((double)dmg * Math.Pow(DEF_MODIFIER, Enemy.Stats.defense));
        Enemy.Stats.health -= output;
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
        using (UnityWebRequest request = UnityWebRequest.Post(URI, "{\"max_tokens\": 300, \"temperature\":2.0, \"prompt\": \"Create 5 enemies for a fantasy RPG each with the following attributes listed on separate lines, Name: unique name, Health: an Integer between 10 and 50, Damage: an Integer between 5 and 20, Magic Damage: an Integer between 5 and 20, Defense: an Integer between 1 to 10, Species: Choose from the following; Orc, Goblin, Giant, Elf, Undead, Dragon, Merfolk, Giant Spider, Special Ability: Choose from the following; Swiftness, Rage, Poison Spray, Fireball.\"}", "application/json"))
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
            cohereOutput = request.downloadHandler.text;
            UnityEngine.Debug.Log("COHERE: " + cohereOutput);
        }

    }

}
   
