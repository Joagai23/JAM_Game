using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpMoverEsfera : MonoBehaviour
{
    public float speed = 1;

    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }

    }
}
