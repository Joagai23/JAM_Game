using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicGameTimer : MonoBehaviour
{
    
    public GameTimer tiempo;
    public Image img;
    public float max;
    // Start is called before the first frame update
    void Start()
    {
        img = this.GetComponent<Image>();
        max = 60f;
    }

    // Update is called once per frame
    void Update()
    {
            if (tiempo.timeLeft > 0)
            {
                img.fillAmount = tiempo.timeLeft / max;
            }
    }
}
