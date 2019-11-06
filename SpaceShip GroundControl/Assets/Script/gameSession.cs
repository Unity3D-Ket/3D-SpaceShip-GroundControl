using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] Text livesText;

    private void Awake()
    {
        int numSessions = FindObjectsOfType<gameSession>().Length;

        if (numSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = playerLives.ToString();
    }

    public void initiateDeath()
    {
        if (playerLives > 1)
        {
            reduceLives();
        }
        else
            {
            FindObjectOfType<spaceShip>().deadSequence();
        }
    }

    public void reduceLives()
    {
        playerLives--;
        FindObjectOfType<spaceShip>().resetLvl();
        livesText.text = playerLives.ToString();
    }

    public void resetlives()
    {
        playerLives = 3;
        livesText.text = playerLives.ToString();
    }

    public void displayLifeOff()
    {
        gameObject.SetActive(false);
    }

}
