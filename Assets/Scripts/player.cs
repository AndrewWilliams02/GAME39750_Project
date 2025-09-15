using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health = 100;
    float damage;
    public float damageMulti = 1;
    public float accuracy = 0.85f;
    public float critAccuracy = 0.10f;
    public float critMulti = 1.5f;

    public int target;
    public GameObject enemyController;
    public GameObject selection;
    public List<GameObject> enemies = new List<GameObject>();
    public List<float> enemyHealth = new List<float>();
    public Slider healthBar;

    public bool turnComplete = false;
    public bool attackSelected;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = health;
        for (int i = 0; i < enemyController.GetComponent<Enemy>().enemies.Length; i++)
        {
            enemies.Add(enemyController.GetComponent<Enemy>().enemies[i]);
        }
        target = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                Collider2D enemyCollider = enemies[i].GetComponent<BoxCollider2D>();

                if (enemyCollider.OverlapPoint(worldPosition))
                {
                    target = i;
                    break;
                }
            }
            UpdateSelection();
            DeathCheck();
        }
    }

    void UpdateHealth()
    {
        healthBar.value = health;
    }

    void UpdateSelection()
    {
        Vector3 selectorPos;
        selectorPos = enemies[target].transform.position;
        selectorPos.x += 1.25f;
        selection.transform.position = selectorPos;
    }

    public void Restore()
    {
        enemyController.GetComponent<Enemy>().damageMulti = 0.5f;
        damage = Random.Range(10, 35);
        health += damage;
        print(this.gameObject.name + " restored themself, reducing damage taken by 50% this turn and healing " + damage + " HP.");

        turnComplete = true;
        enemyController.GetComponent<Enemy>().turnsComplete = false;
        GameState.HideUI();
    }

    public void NormalAttack()
    {
        damage = Random.Range(15, 20);
        enemyController.GetComponent<Enemy>().health[target] -= damage * damageMulti;
        print(this.gameObject.name + " dealt " + damage + " damage to " + enemies[target].name + ".");

        damageMulti = 1;
        turnComplete = true;
        enemyController.GetComponent<Enemy>().turnsComplete = false;
        GameState.HideUI();
    }

    public void RecoilAttack()
    {
        damage = Random.Range(15, 25);
        enemyController.GetComponent<Enemy>().health[target] -= (damage * 2) * damageMulti;
        health -= damage;
        print(this.gameObject.name + " dealt " + damage * 2 + " damage to " + enemies[target].name + " & dealt " + damage + " damage to themself.");

        damageMulti = 1;
        turnComplete = true;
        enemyController.GetComponent<Enemy>().turnsComplete = false;
        GameState.HideUI();
    }

    public void WideAttack()
    {
        damage = Random.Range(8, 12);
        for (int i = 0; i < enemies.Count; i++)
        {
            enemyController.GetComponent<Enemy>().health[i] -= damage * damageMulti;
        }
        print(this.gameObject.name + " dealt " + damage + " damage to all enemies.");

        damageMulti = 1;
        turnComplete = true;
        enemyController.GetComponent<Enemy>().turnsComplete = false;
        GameState.HideUI();
    }

    void DeathCheck()
    {
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
            healthBar.gameObject.SetActive(false);
        }
    }
}
