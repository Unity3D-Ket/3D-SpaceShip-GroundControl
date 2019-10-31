using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] float startTime;
    float currentTime;
    bool triggerDeath = true;

    [SerializeField] Text countdownText;

    public void Start()
    {
        currentTime = startTime;
    }

    public void Update()
    {
        countdownMath();
    }

    public void countdownMath()
    {
        currentTime -= 1 * Time.deltaTime;

        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            if (triggerDeath)
            {
                StartCoroutine(activateDead());
            } 
        }
    }

    IEnumerator activateDead()
    {
        triggerDeath = false;
        activateSequence();
        yield return new WaitForSeconds(5f);
        triggerDeath = true;
    }

    public static void activateSequence()
    {
        FindObjectOfType<spaceShip>().deadSequence(); //ERROR Particle repeats 5xs
        //Debug.Log("Activated?");
    }

    public void addTime()
    {
        currentTime += 10;
    }
}
