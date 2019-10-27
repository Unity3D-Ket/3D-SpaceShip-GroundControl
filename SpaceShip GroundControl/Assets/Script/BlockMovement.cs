using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class BlockMovement : MonoBehaviour
{
    [SerializeField] Vector3 axisPoint;
    [SerializeField] float Speed = 10f;

    float movementFactor;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveCalculation();
    }

    public void moveCalculation()
    {
        if (Speed <= Mathf.Epsilon) { return;  }
        float cycles = Time.time / Speed;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        //print(rawSinWave);

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 movePoint = axisPoint * movementFactor;
        transform.position = startPos + movePoint;
    }
}
