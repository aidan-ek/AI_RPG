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
    public TMP_Text eHealthText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        eHealthText.text = Enemy.Stats.health + "/" + Enemy.Stats.maxHealth;
        pHealthText.text = Player.Stats.health + "/" + Player.Stats.getMaxHealth();
        attackText.text = "ATTACK (" + Player.Stats.getPhysicalAmp() + ")";

    }

}
   
