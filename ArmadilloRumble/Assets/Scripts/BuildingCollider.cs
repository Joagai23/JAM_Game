using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destruye el edificio al golpear, asignar en el armadillo
public class BuildingCollider : MonoBehaviour
{

    // Delay antes de destruir el piso
    public float destroy_delay = 0.5f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Building") 
        {
            int floors = collision.gameObject.transform.childCount;
            if (floors > 2) 
            {
                // Destruye los pisos superiores
                GameObject _child = collision.gameObject.transform.GetChild(floors - 1).gameObject;
                StartCoroutine(DestroyUpper(_child));
            } 
            
            if (floors == 2)
            {
                // Destruye piso inferior y lo destruye
                GameObject _child0 = collision.gameObject.transform.GetChild(floors - 1).gameObject;
                GameObject _child1 = collision.gameObject.transform.GetChild(floors - 2).gameObject;
                StartCoroutine(DestroyLower(_child0, _child1));
            }
        } 
    }

    private IEnumerator DestroyUpper(GameObject _child)
    {
        yield return new WaitForSeconds(destroy_delay);
        GameObject.Destroy(_child);
    }

    private IEnumerator DestroyLower(GameObject _child0, GameObject _child1)
    {
        yield return new WaitForSeconds(destroy_delay);
        GameObject.Destroy(_child0);
        Renderer rend = _child1.GetComponent<Renderer>();
        rend.enabled = true;
    }

}
