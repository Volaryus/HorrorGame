using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraRotate : MonoBehaviour
{

    public float rotateSpeedY = 15f;
    public float rotateSpeedX = 0f;
    public float rotateSpeedZ = 0f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateSpeedX*Time.deltaTime, rotateSpeedY*Time.deltaTime, rotateSpeedZ*Time.deltaTime);
    }
}
