using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float movementFactor;
    [SerializeField] float period;

    void Start()
    {
        startPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        // protect against NaN error!
        if (period == 0) { return; }
        float cycles = Time.time / period; // Growing over time!
        const float tau = Mathf.PI * 2;     // Constant value of 6.283!
        float rawSinWave = Mathf.Sin(cycles * tau); // Going from -1 to 1!

        movementFactor = (rawSinWave + 1f) / 2f;    // Recalculated to go from 0 to 1 cleaner!

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPos + offset;

        // code below moves the targets position by the offset amount but the object keeps on moving which is not what we are looking for!
        // Vector3 targetPos = transform.position += offset * Time.deltaTime;
    }
}
