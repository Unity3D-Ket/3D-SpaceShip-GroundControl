using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerPickup : MonoBehaviour
{
    public void OnTriggerEnter(Collider tagOther)
    {
        Debug.Log("Outside Switch Statement: Object2 was touched");

        switch (tagOther.gameObject.tag)
        {
            case "Ship":
                Debug.Log("Object2 was touched");
                FindObjectOfType<CountDownTimer>().addTime();
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

}
