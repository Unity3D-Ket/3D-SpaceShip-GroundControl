using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spaceShip : MonoBehaviour
{
    Rigidbody shipBody;
    AudioSource SFX;

    [Header("Movement")]
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float mainSpeed = 1f;
    [Header ("SFX")]
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip completed;
    [SerializeField] AudioClip dead;
    [Header("VFX")]
    [SerializeField] ParticleSystem activation;
    [SerializeField] ParticleSystem victory;
    [SerializeField] ParticleSystem death;
    [Header("Scene Delay")]
    [SerializeField] float lvlLoadDelay = 1f;

    bool collisionEnabled = true;

    enum playerState {Alive, Dying, Transcending}

    playerState player = playerState.Alive;

    // Start is called before the first frame update
    void Start()
    {
        shipBody = GetComponent<Rigidbody>();
        SFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == playerState.Alive)
        {
            playerRotationInput();
            shipThrustInput();
        }

        if (Debug.isDebugBuild)
        {
            debugInput();
        }
       
    }

    public void debugInput()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            loadNextLevel();
        } else if (Input.GetKeyDown(KeyCode.K))
            {
                loadPreviousLevel();
            }else if (Input.GetKeyDown(KeyCode.C))
                {
                    collisionEnabled = !collisionEnabled;
                }
    }

    public void playerRotationInput()
    {
        float rSpeed = rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            manualRotation(rSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            manualRotation(-rSpeed);
        }
    }

    public void manualRotation(float rSpeed)
    {
        shipBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rSpeed);
        shipBody.freezeRotation = false;
    }

    public void shipThrustInput()
    {
        float mSpeed = mainSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            //print("Spaced Key was Pressed");
            shipBody.AddRelativeForce(Vector3.up * mainSpeed * Time.deltaTime);
            applyThrust();
        }
        else
            {
            SFX.Stop();
            activation.Stop();
            }
    }

    public void applyThrust()
    {
        if (!SFX.isPlaying)
        {
           SFX.PlayOneShot(mainEngine);
           activation.Play();
        }      
    }

    void OnCollisionEnter(Collision tagCollision)
    {

        if (player != playerState.Alive || !collisionEnabled) { return; }

        switch (tagCollision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Power":
                break;
            case "Completed":
                completeSequence();
                break;
            default:
                FindObjectOfType<gameSession>().initiateDeath();
                //deadSequence(); 
                break;
        }

    }

    public void completeSequence()
    {
        player = playerState.Transcending;
        SFX.Stop();
        SFX.PlayOneShot(completed);
        death.Stop();
        victory.Play();
        Invoke("loadNextLevel", lvlLoadDelay);
    }

    public void resetLvl()
    {
        //print("Dead");
        player = playerState.Dying;
        SFX.Stop();
        SFX.PlayOneShot(dead);
        death.Play();
        Invoke("loadCurrentSccene", lvlLoadDelay);     
    }

    public void deadSequence()
    {
        //print("Dead");
        player = playerState.Dying;
        SFX.Stop();
        SFX.PlayOneShot(dead);
        death.Play();
        Invoke("loadMainLevel", lvlLoadDelay);
    }

    public void loadCurrentSccene()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void loadMainLevel() 
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<gameSession>().resetlives();
        FindObjectOfType<gameSession>().displayLifeOff();
    }

    public void loadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        print(currentScene);
        int nextScene = currentScene +1;
        SceneManager.LoadScene(nextScene);
        FindObjectOfType<gameSession>().resetlives();

        if (currentScene == 5)
        {
            FindObjectOfType<gameSession>().displayLifeOff();
        }

    }

    public void loadPreviousLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene -1;
        SceneManager.LoadScene(nextScene);
    }
}
