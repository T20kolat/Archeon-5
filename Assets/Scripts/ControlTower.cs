using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTower : MonoBehaviour
{
    
    Animator towerAnim;
    void Start()
    {
        towerAnim = GetComponent<Animator>();
        towerAnim.Play("takeOff");
    }

   
}
