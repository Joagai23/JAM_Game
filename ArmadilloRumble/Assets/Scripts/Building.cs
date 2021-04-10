using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Building : MonoBehaviour
{

    public float DestroyDelay = 0.5f;
    public float TimeToBuild = 30.0f;
    
    float timeLeft = -1.0f;
    int maxFloors = -1;
    int floors = -1;

    void Start()
    {
        floors = transform.childCount;
        maxFloors = floors;
    }


    void Update()
    {
        // Comprobar si tiemr activo
        if (timeLeft > 0) 
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0) 
            {
                // Reconstruir un piso si acaba
                ReBuildFloor();
                if (floors < maxFloors)
                    timeLeft = TimeToBuild;
            }
        }
    }

    public int GetFloors()
    {
        return floors;
    }

    public void DestroyFloor()
    {
        if (floors == 1)
            return;

        timeLeft = TimeToBuild;
        if (floors > 2) {
            GameObject _child = transform.GetChild(floors - 1).gameObject;
            StartCoroutine(DestroyUpper(_child));
        } 
        else if (floors == 2)
        {
            GameObject _child0 = transform.GetChild(floors - 1).gameObject;
            GameObject _child1 = transform.GetChild(floors - 2).gameObject;
            StartCoroutine(DestroyLower(_child0, _child1));
        }
        floors--;
    }

    private IEnumerator DestroyUpper(GameObject _child)
    {
        yield return new WaitForSeconds(DestroyDelay);
        Renderer _rend = _child.GetComponent<Renderer>();
        _rend.enabled = false;
    }

    private IEnumerator DestroyLower(GameObject _child0, GameObject _child1)
    {
        yield return new WaitForSeconds(DestroyDelay);
        Renderer _rend0 = _child0.GetComponent<Renderer>();
        _rend0.enabled = false;
        Renderer _rend1 = _child1.GetComponent<Renderer>();
        _rend1.enabled = true;
    }

    void ReBuildFloor()
    {
        Assert.AreNotEqual(maxFloors, floors);
        if (floors >= 1) 
        {
            GameObject _child = transform.GetChild(floors).gameObject;
            Renderer _rend = _child.GetComponent<Renderer>();
            _rend.enabled = true;
        } 
        
        if (floors == 1)
        {
            GameObject _child = transform.GetChild(floors - 1).gameObject;
            Renderer _rend = _child.GetComponent<Renderer>();
            _rend.enabled = false;
        }
        floors++;
    }

}
