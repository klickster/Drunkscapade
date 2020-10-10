using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RotatePlayer(Vector3 rotation)
    {
        transform.Rotate(rotation);
    }
}
