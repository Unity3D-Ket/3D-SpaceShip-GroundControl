﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerPickup : MonoBehaviour
{
    void OnCollisionEnter(Collision tagCollision)
    {
        switch (tagCollision.gameObject.tag)
        {
            case "Ship":
                FindObjectOfType<CountDownTimer>().addTime();
                Destroy(gameObject);
                break;
            default:
                break;
        }

    }

    //public void OnTriggerEnter(Collider tagOther)
    //{
    //    switch (tagOther.gameObject.tag)
    //    {
    //        case "Ship":
    //            FindObjectOfType<CountDownTimer>().addTime();
    //            Destroy(gameObject);
    //            break;
    //        default:
    //            break;
    //    }
    //}

}
