using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Transform armadillo;
    public float rotationRate = 0.5f;
    public float rotationSpeed = 5.0f;

    private float xRotation;

    private float xTargetRotation;

    public float XTargetRotation
    {
        get { return xTargetRotation; }
        set { xTargetRotation = value; }
    }

    public void Rotate(Vector3 direction, float speed)
    {
        xRotation = Mathf.Abs(direction.x) * speed;
        transform.forward = direction;
    }

    private void Update()
    {
        float time = Time.deltaTime;

        XAxisRotation(time);

        armadillo.transform.Rotate(0.0f, -xRotation * rotationSpeed, 0.0f, Space.Self);
    }

    private void XAxisRotation(float time)
    {
        float offset = xTargetRotation - xRotation;
        xRotation += Mathf.Clamp(offset, -rotationRate * time, rotationRate * time);
    }
}
