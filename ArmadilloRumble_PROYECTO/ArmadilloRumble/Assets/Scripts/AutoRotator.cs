using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour
{
    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, speed * Time.deltaTime, 0.0f, Space.Self);
    }
}
