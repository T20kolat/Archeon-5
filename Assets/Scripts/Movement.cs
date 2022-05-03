using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] AudioClip rocketBoost;
    [SerializeField] float thrustPower;
    [SerializeField] float rotationPower;
    [SerializeField] ParticleSystem rocketThruster;
    [SerializeField] ParticleSystem rocketThrusterTwo;
    [SerializeField] ParticleSystem side_LThrust;
    [SerializeField] ParticleSystem side_RThrust;

    [SerializeField] public int maxFuel;
    [SerializeField] public int currentFuel;
    public FuelBar fuelBar;

    Rigidbody myRb;
    AudioSource rocketAudio;

    void Start()
    {
        rocketAudio = this.GetComponent<AudioSource>();
        myRb = gameObject.GetComponent<Rigidbody>();
        currentFuel = maxFuel;
        fuelBar.SetMaxFuel(maxFuel);
    }
    void Update()
    {
        rocketThrust();
        rocketInput();
    }

    void rocketThrust()
    {
        // Rocket thrust 
        if (Input.GetKey(KeyCode.Space))
        {
            consumeFuel(2);
            moveRocket();
        }
        else // If not pressing space stop audio clip from playing!
        {
            rocketAudio.Stop();
        }

    }

    void consumeFuel(int consume)
    {
        currentFuel -= consume;
        fuelBar.SetFuel(currentFuel);
    }
    void rocketInput()
    {
        // Input for rocket rotation + and - for each direction !!
        if (Input.GetKey(KeyCode.A))
        {
            turnLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turnRight();
        }
    }

    void moveRocket()
    {
        if (currentFuel > 0)
        {
            rocketThruster.Play();
            rocketThrusterTwo.Play();
            // Add force to the point of direction which gives more realistic and desirable result for rocket!
            // Refactored to use Time.deltaTime for fps optimization!
            myRb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
            // Checking if rocket audio is not playing then play audio clip when pressing Space!!
            if (!rocketAudio.isPlaying)
            {
                rocketAudio.PlayOneShot(rocketBoost);
            }
        }

    }

    void turnLeft()
    {
        rocketRotation(-rotationPower);
        side_LThrust.Play();
    }

    void turnRight()
    {
        rocketRotation(rotationPower);
        side_RThrust.Play();
    }

    //float for transform.rotate for each frame!!
    void rocketRotation(float rotationOnFrame) // method overload for cleaner code!
    {
        myRb.freezeRotation = true; // Freezing rigidbody rotation so we can manually rotate!
        transform.Rotate(Vector3.right * rotationOnFrame * Time.deltaTime);
        myRb.freezeRotation = false; // After rotating manually we activate rigidbody rotation back!
    }



}



