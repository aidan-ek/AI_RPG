using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Cache;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : MonoBehaviour
{
    private const string URI = "https://api.cohere.ai/v1/generate";

    public string cohereOutput;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateEnemies()
    {
        UnityEngine.Debug.Log("generate_enemy");
        StartCoroutine(PostData_Coroutine());
    }

    IEnumerator PostData_Coroutine()
    {
        UnityEngine.Debug.Log("we are here");
        using (UnityWebRequest request = UnityWebRequest.Post(URI, "{\"max_tokens\": 300, \"temperature\":2.0, \"prompt\": \"Create 5 enemies for a fantasy RPG each with the following attributes listed on separate lines, Name: unique name, Health: an Integer between 10 and 50, Damage: an Integer between 5 and 20, Magic Damage: an Integer between 5 and 20, Defence: an Integer between 1 to 10, Species: Choose from the following; Orc, Goblin, Giant, Elf, Undead, Dragon, Merfolk, Giant Spider, Special Ability: Choose from the following; Swiftness, Rage, Poison Spray, Fireball.\"}", "application/json"))
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
