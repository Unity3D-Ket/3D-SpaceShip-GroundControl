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
    }

    public void playerRotationInput()
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

    public void shipThrustInput()
    {
        float mSpeed = mainSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            //print("Spaced Key was Pressed");
            shipBody.AddRelativeForce(Vector3.up * mainSpeed);
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
            }

        activation.Play();
    }

    void OnCollisionEnter(Collision tagCollision)
    {

        if (player != playerState.Alive) {return;}

        switch (tagCollision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Completed":
                completeSequence();
                break;
            default:
                deadSequence();
                break;
        }
    }

    private void completeSequence()
    {
        player = playerState.Transcending;
        SFX.Stop();
        SFX.PlayOneShot(completed);
        victory.Play();
        Invoke("loadScene", 1f);
    }

    private void deadSequence()
    {
        print("Dead");
        player = playerState.Dying;
        SFX.Stop();
        SFX.PlayOneShot(dead);
        death.Play();
        Invoke("loadMainLevel", 1f);
    }

    public void loadMainLevel() //TODO Create & Load Main Menu
    {
        SceneManager.LoadScene(0);
    }

    public void loadScene()
    {
        SceneManager.LoadScene(1);
    }
}
