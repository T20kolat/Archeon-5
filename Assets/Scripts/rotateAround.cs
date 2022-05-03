using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateAround : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
        void Update()
    {
        transform.Rotate(rotateSpeed,0,0*Time.deltaTime);
    }
}
