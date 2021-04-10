using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{

    public float gameOverTime = 60.0f;

    public float timeLeft = -1.0f;

    public Text timeUp;


    void Start()
    {
        timeLeft = gameOverTime;
    }

    void Update()
    {
        if (timeLeft > gameOverTime) { timeLeft = gameOverTime; }
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timeUp.text = timeLeft.ToString("F0");
            if (timeLeft <= 0)
            {
                int points = this.GetComponent<Puntuacion>().GetPoints();
                PlayerPrefs.SetString("Points", points.ToString());
                SceneManager.LoadScene("EndMenu");
            }
        }
    }
}
