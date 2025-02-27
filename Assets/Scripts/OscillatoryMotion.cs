using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillatory : MonoBehaviour
{
    Vector3 startingPosition;

    [SerializeField]
    Vector3 movementVector;

    [SerializeField]
    [Range(0, 1)]
    float movementFactor;

    [SerializeField]
    float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position; // Initializing startingPosition
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        } // Avoid division by zero if period is too small

        float cycles = Time.time / period; // Continually growing over time
        const float tau = Mathf.PI * 2; // Constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // Going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // Recalculated to go from 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
