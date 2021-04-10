using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puntuacion : MonoBehaviour
{
    public int points;
    public int hitCombo;
    public bool combo;
    public float multiplier;
    public float timer;
    public int UpperPoints;
    public int LowerPoints;
    public int LossPoints;
    public float comboW;
    public float comboH;

    void Start()
    {
        points = 0;
        hitCombo = 0;
        combo = false;
        timer = 0f;
        multiplier = 1.5f;
        LowerPoints = 2000;
        UpperPoints = 500;
        LossPoints = 250;
        comboH = 0.82f;
        comboW = 0.91f;

    }


    void Update()
    {
        if (timer <= 0)
        {
            combo = false;
            hitCombo = 0;
        }
        else
        {
            timer -= Time.deltaTime;
            
        }

        if (timer > 30){ timer = 30; }
    }
    public void Puntos(int floors)
    {
            if (floors > 2)
            {
                if (combo) {
                    points +=  (int) (UpperPoints * hitCombo * multiplier);
                }
                else{
                    points += UpperPoints;
                }
            }else if (floors == 2)
            {

                if (combo)
                {
                    points += (int)(LowerPoints * hitCombo * multiplier);
                }
                else
                {
                    points += LowerPoints;
                }
                hitCombo++;
                if (hitCombo == 1)
                {

                comboW = 0.91f;
                timer = 30;
                    combo = true;
                }
                else
                {
                    timer += 10;
                }
            }
            else if (floors==1)
            {
                points -= LossPoints;
                timer -= 15;
            }
        
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), "<color=red>Punticos: " + points + "</color>");
        
        if (combo)
        {

            string TextoCombo = "<color=red>Combo!";
            if (hitCombo >= 2)
            {
                TextoCombo += "X" + hitCombo.ToString();
                comboW = 0.9f;
            };
            TextoCombo += "</color>";
            GUI.Label(new Rect((float) (Screen.width* comboW), (float)(Screen.height * comboH), 100, 100), TextoCombo);
        }
    }

}
