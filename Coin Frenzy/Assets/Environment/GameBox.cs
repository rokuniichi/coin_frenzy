using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBox : MonoBehaviour
{
    private const float OFFSET_X = 500.0f;
    private const float OFFSET_Z = 250.0f;
    Vector3 tempPosition;
    public Transform groundTf;
    // Start is called before the first frame update
    void Start()
    {
        tempPosition = groundTf.position;
        tempPosition.x += OFFSET_X;
        tempPosition.z += OFFSET_Z;
        transform.position = tempPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
