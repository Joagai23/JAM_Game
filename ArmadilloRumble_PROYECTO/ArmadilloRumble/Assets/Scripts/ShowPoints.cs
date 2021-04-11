using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPoints : MonoBehaviour
{

    private GUIStyle guiStyle = new GUIStyle(); 

    void Start()
    {
        guiStyle.fontSize = 64;
        guiStyle.font = (Font)Resources.Load("AldotheApache");
        guiStyle.alignment = TextAnchor.MiddleCenter;
    }

    void OnGUI()
    {
        string points = PlayerPrefs.GetString("Points");
        if (string.IsNullOrEmpty(points))
            points = "0";

        GUI.Label(new Rect(Screen.width * 0.1f, Screen.height * 0.3f, Screen.width * 0.8f, Screen.height * 0.1f),
            "<color=red>" + points + "</color>", guiStyle);
    }
}
