using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float spinSpeed;
    public Vector3 axis;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(axis, spinSpeed * Time.deltaTime);
    }
}
