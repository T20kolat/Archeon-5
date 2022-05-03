using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelPick : MonoBehaviour
{

    Movement movement;
    public FuelBar fuelBar;
    public int fuelBonus = 400;



    private void Awake()
    {
        movement = FindObjectOfType<Movement>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (movement.currentFuel < movement.maxFuel)
        {
            Destroy(gameObject);
            movement.currentFuel = movement.currentFuel + fuelBonus;
            fuelBar.SetFuel(movement.currentFuel);
        }
    }

}
