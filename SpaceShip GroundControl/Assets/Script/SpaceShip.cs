using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            case "Fuel":
                //TODO - Add Fuel
                break;
            case "Damage":
                //TODO - Minus Fuel
                break;
            case "Power1":
                //TODO
                break;
            default:
                print("Dead");
                break;

        }
    }
}
