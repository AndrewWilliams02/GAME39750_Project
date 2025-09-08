using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameState : MonoBehaviour
{
    public static GameObject actions, attacks;
    public GameObject player, actionsPub, attacksPub;
    public bool playersTurnComplete = false;

    private void Start()
    {
        actions = actionsPub;
        attacks = attacksPub;
    }

    private void Update()
    {
        playersTurnComplete = player.GetComponent<player>().turnComplete;
        if (playersTurnComplete)
        {
            HideUI();
        }
    }

    public static void ActivateUI()
    {
        actions.SetActive(true);
        attacks.SetActive(false);
    }

    public static void HideUI()
    {
        actions.SetActive(false);
        attacks.SetActive(false);
    }

    public void AttackAction()
    {
        actions.SetActive(false);
        attacks.SetActive(true);
    }

    public void Back()
    {
        actions.SetActive(true);
        attacks.SetActive(false);
    }
}
