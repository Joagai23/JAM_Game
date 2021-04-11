using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destruye el edificio al golpear, asignar en el armadillo
public class BuildingCollider : MonoBehaviour
{

    private Building _building;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Building")
        {
            _building = collision.gameObject.GetComponent<Building>();
            int _floors = _building.GetFloors();
            this.GetComponent<Puntuacion>().Puntos(_floors);
            _building.DestroyFloor();
        } 
    }

}
