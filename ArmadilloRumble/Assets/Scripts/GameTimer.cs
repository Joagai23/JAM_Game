using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{

    public float GameOverTime = 60.0f;

    public float timeLeft = -1.0f;

    void Start()
    {
        timeLeft = GameOverTime;
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                int points = this.GetComponent<Puntuacion>().GetPoints();
                PlayerPrefs.SetString("Points", points.ToString());
                SceneManager.LoadScene("EndMenu");
            }
        }
    }
}
