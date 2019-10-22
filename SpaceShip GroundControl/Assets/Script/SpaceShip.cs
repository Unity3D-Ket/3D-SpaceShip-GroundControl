using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//UNABLE TO FIX
//There are inconsistent line endings in the 'Assets/spaceShip.cs' script.Some are Mac OS X(UNIX) and some are Windows.
//This might lead to incorrect line numbers in stacktraces and compiler errors. Many text editors can fix this using Convert Line Endings menu commands.
//UnityEditor.AssetDatabase:Refresh()
//SyntaxTree.VisualStudio.Unity.Bridge.<>c:<Refresh>b__11_0()
//SyntaxTree.VisualStudio.Unity.Bridge.<>c__DisplayClass40_0:<RunOnceOnUpdate>b__0()
//UnityEditor.EditorApplication:Internal_CallUpdateFunctions()

public class spaceShip : MonoBehaviour
{
    Rigidbody shipBody;
    AudioSource SFX;
    [SerializeField] float rotationSpeed = 1f, mainSpeed = 1f;


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

    public void playerInput()
    {
        shipBody.freezeRotation = true;
        float rSpeed = rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rSpeed);
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
                SceneManager.LoadScene(1);
                break;
            default:
                print("Dead");
                SceneManager.LoadScene(0);
                break;
        }
    }
}
