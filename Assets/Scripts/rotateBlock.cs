using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateBlock : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
        void Update()
    {
        transform.Rotate(0,rotateSpeed,0*Time.deltaTime);
    }
}
