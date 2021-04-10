using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboTimer : MonoBehaviour
{

    public Puntuacion puntos;
    public Image img;
    public float max;
    // Start is called before the first frame update
    void Start()
    {
        img = this.GetComponent<Image>();
        max = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        if (puntos.combo)
        {
            this.GetComponent<Image>().enabled=true;
            transform.GetChild(0).GetComponent<Text>().enabled = true;
            if (puntos.timer > 0)
            {
                img.fillAmount = puntos.timer / max;
            }
        }
        else
        {
            this.GetComponent<Image>().enabled = false;
            transform.GetChild(0).GetComponent<Text>().enabled = false;
        }
    }
}
