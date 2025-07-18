using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour
{
    public float xSpeed = 0.0f;
    public float ySpeed = 0.0f;
    public float zSpeed = 0.0f;

    private Quaternion originalRotation;
    private bool shouldRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldRotate)
        {
            transform.Rotate(
                xSpeed * Time.deltaTime,
                ySpeed * Time.deltaTime,
                zSpeed * Time.deltaTime
            );
        }

        
    }

    public void StopRotate()
    {
        shouldRotate = false;
        transform.rotation = originalRotation;
    }
}
