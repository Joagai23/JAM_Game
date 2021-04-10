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
    private float comboW;
    private float comboH;

    private GUIStyle guiStyle = new GUIStyle();
    private AudioClip lose_sound;
    public AudioSource audioSource;

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

        guiStyle.font = (Font)Resources.Load("AldotheApache");
        lose_sound = ((AudioClip)Resources.Load("lose"));
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

        if (timer > 30) { timer = 30; }
    }

    public int GetPoints()
    {
        return points;
    }

    public void Puntos(int floors)
    {
        if (floors > 2)
        {
            if (combo)
            {
                points += (int)(UpperPoints * hitCombo * multiplier);
            }
            else
            {
                points += UpperPoints;
            }
        }
        else if (floors == 2)
        {
            hitCombo++;
            if (combo)
            {
                points += (int)(LowerPoints * hitCombo * multiplier);
                GetComponent<GameTimer>().timeLeft += 5;
                timer += 10;
            }
            else
            {
                points += LowerPoints;
                if (hitCombo == 1)
                {
                    comboW = 0.91f;
                    timer = 30;
                    combo = true;
                }
            }
        }
        else if (floors == 1)
        {
            points -= LossPoints;
            timer -= 15;
            audioSource.PlayOneShot(lose_sound);
        }

    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 100), "<color=red>Score: " + points + "</color>", guiStyle);

        if (combo)
        {

            string TextoCombo = "<color=red>Combo!";
            if (hitCombo >= 2)
            {
                TextoCombo += "X" + hitCombo.ToString();
                comboW = 0.9f;
            };
            TextoCombo += "</color>";
            GUI.Label(new Rect((float)(Screen.width * comboW), (float)(Screen.height * comboH), 100, 100), TextoCombo, guiStyle);
        }
    }
}
