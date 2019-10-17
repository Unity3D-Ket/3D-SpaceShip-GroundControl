using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShip : MonoBehaviour
{
    Rigidbody shipBody;
    AudioSource SFX;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float mainSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        shipBody = GetComponent<Rigidbody>();
        SFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput();
        shipThrust();
    }

    private void playerInput()
    {
        shipBody.freezeRotation = true;

        float rSpeed = rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward *rSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rSpeed);
        }

        shipBody.freezeRotation = false;
    }

    private void shipThrust()
    {
        float mSpeed = mainSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            //print("Spaced Key was Pressed");
            shipBody.AddRelativeForce(Vector3.up * mainSpeed);

            if (!SFX.isPlaying)
            {
                SFX.Play();
            }

        }
        else
        {
            SFX.Stop();
        }
    }

    void OnCollisionEnter(Collision tagCollision)
    {
        switch (tagCollision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Completed":
                SceneManager.LoadScene("1");
                break;
            default:
                print("Dead");
                SceneManager.LoadScene("0");
                break;
        }
    }
}
