using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.Assertions;

public class Building : MonoBehaviour
{

    public float DestroyDelay = 0.5f;
    public float GracePeriod = 0.5f;
    public float TimeToBuild = 30.0f;
    
    float timeLeft = -1.0f;
    float graceLeft = -1.0f;
    int maxFloors = -1;
    int floors = -1;
    int floor_offset = 0;

    private AudioClip[] bomb_sounds = new AudioClip[5];
    private AudioClip[] blip_sounds = new AudioClip[5];
    public AudioSource audioSource;

    void Start()
    {
        floors = transform.childCount;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name == "Collider")
            {
                floors -= 1;
                floor_offset += 1;
            }
        }
        maxFloors = floors;
        for (int i = 0; i < 5; i++)
        {
            bomb_sounds[i] = ((AudioClip)Resources.Load("bomba" + i));
            blip_sounds[i] = ((AudioClip)Resources.Load("blip" + i));
        }
    }


    void Update()
    {
        // Comprobar si timer activo
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

        // Comprobar si timer activo
        if (graceLeft > 0)
        {
            graceLeft -= Time.deltaTime;
        }
    }

    public int GetFloors()
    {
        return floors;
    }

    public void DestroyFloor()
    {
        if (floors == 1 || graceLeft > 0)
            return;

        timeLeft = TimeToBuild;
        graceLeft = GracePeriod;
        if (floors > 2) {
            GameObject _child = transform.GetChild(floor_offset + floors - 1).gameObject;
            StartCoroutine(DestroyUpper(_child));
        } 
        else if (floors == 2)
        {
            GameObject _child0 = transform.GetChild(floor_offset + floors - 1).gameObject;
            GameObject _child1 = transform.GetChild(floor_offset + floors - 2).gameObject;
            StartCoroutine(DestroyLower(_child0, _child1));
        }
        int sound_index = Random.Range(0, 5);
        audioSource.PlayOneShot(blip_sounds[sound_index]);
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
        setRenderer(_child0, false);
        setRenderer(_child1, true);
        int sound_index = Random.Range(0, 5);
        audioSource.PlayOneShot(bomb_sounds[sound_index]);
    }

    void ReBuildFloor()
    {
        Assert.AreNotEqual(maxFloors, floors);
        if (floors >= 1) 
        {
            GameObject _child = transform.GetChild(floor_offset + floors).gameObject;
            setRenderer(_child, true);
        } 
        
        if (floors == 1)
        {
            GameObject _child = transform.GetChild(floor_offset + floors - 1).gameObject;
            setRenderer(_child, false);
        }
        floors++;
    }

    void setRenderer(GameObject _child, bool mode)
    {
        Renderer _rend = _child.GetComponent<Renderer>();
        if (_rend)
        {
            _rend.enabled = mode;
        } 
        else
        {
            for (int i = 0; i < _child.transform.childCount; i++)
            {
                setRenderer(_child.transform.GetChild(i).gameObject, mode);
            }
        }
    }

}
