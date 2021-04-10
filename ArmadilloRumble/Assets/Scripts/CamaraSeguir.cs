using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSeguir : MonoBehaviour
{
    public GameObject armadillo;

    public float altura = 1;
    public float distancia = 3;
    public float angulo = -90;
    
    public float giro = 36f;
    
    void Update()
    {

        float cameraX = armadillo.transform.position.x + (distancia * Mathf.Cos(Mathf.Deg2Rad * angulo));
        float cameraY = armadillo.transform.position.y + altura;
        float cameraZ = armadillo.transform.position.z + (distancia * Mathf.Sin(Mathf.Deg2Rad * angulo));

        transform.position = new Vector3(cameraX, cameraY, cameraZ);

        if (Input.GetKey(KeyCode.Q))
        {
            angulo = angulo - giro * Time.deltaTime;
        }else if (Input.GetKey(KeyCode.E))
        {
            angulo = angulo + giro * Time.deltaTime;
        }

        transform.LookAt(armadillo.transform.position);

    }

}