using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public List<float> health = new List<float>();
    public GameObject[] enemies = new GameObject[2];
    public Slider[] healthBars = new Slider [2];

    float damage;
    public float damageMulti = 1;
    public float accuracy = 0.85f;
    public float critAccuracy = 0.10f;
    public float critMulti = 1.5f;

    public bool turnsComplete = true;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            health.Add((int)Random.Range(50, 65));
            healthBars[i].maxValue = health[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();

        if (!turnsComplete)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (Random.value <= critAccuracy)
                {
                    damageMulti = critMulti;
                }

                if (Random.value <= accuracy)
                {
                    damage = Random.Range(10, 15) * damageMulti;
                    player.GetComponent<Player>().health -= damage;
                    print(enemies[i].name + " dealt " + damage + " damage to the player.");
                } else
                {
                    print(enemies[i].name + " missed their attack.");
                }
            }

            damageMulti = 1;
            turnsComplete = true;
            player.GetComponent<Player>().turnComplete = false;
            GameState.ActivateUI();
        }

        DeathCheck();
    }

    void UpdateHealth()
    {
        for (int i = 0; i < health.Count; i++)
        {
            healthBars[i].value = health[i];
        }
    }

    void DeathCheck()
    {
        for (int i = 0; i < health.Count; i++)
        {
            if (health[i] <= 0)
            {
                enemies[i].gameObject.SetActive(false);
                healthBars[i].gameObject.SetActive(false);
            }
        }
    }
}
