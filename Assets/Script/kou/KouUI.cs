using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KouUI : MonoBehaviour {

    public KouScan kouScan = null;
    public Kou3D kou3d = null;
    public Image img = null;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnGUI()
    {
        GUIStyle style_tatle = new GUIStyle();
        style_tatle.normal.textColor = Color.yellow;
        style_tatle.fontSize = 25;

        if (GUI.Button(new Rect(20, 20, 180, 90), "----"))
        {
            kouScan._baiseCol -= 10;
        }
        GUI.Label(new Rect(220, 20, 80, 80), kouScan._baiseCol.ToString(), style_tatle);
        GUI.Label(new Rect(220, 50, 80, 80), "亮", style_tatle);
        if (GUI.Button(new Rect(280, 20, 180, 90), "++++"))
        {
            kouScan._baiseCol += 10;
        }

        if (GUI.Button(new Rect(Screen.width - 430, 20, 180, 90), "----"))
        {
            kouScan._cutCol -= 10;
        }
        GUI.Label(new Rect(Screen.width - 230, 20, 80, 80), kouScan._cutCol.ToString(), style_tatle);
        GUI.Label(new Rect(Screen.width - 230, 50, 80, 80), "暗", style_tatle);
        if (GUI.Button(new Rect(Screen.width - 170, 20, 180, 90), "++++"))
        {
            kouScan._cutCol += 10;
        }

        if (GUI.Button(new Rect(20, Screen.height - 220, 180, 100), "init"))
        {
            kou3d.cam = null;
            img.sprite = null;
        }
    }
}
