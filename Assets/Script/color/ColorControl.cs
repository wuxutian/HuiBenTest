using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorControl : MonoBehaviour {

    [SerializeField]
    Image plane = null;

    [SerializeField]
    Image nowC = null;

    [SerializeField]
    GameObject chooseC = null;

    [SerializeField]
    GameObject textC = null;

    Texture2D tex2d;

    int TexPixelLength = 256;
    Color[,] arrayColor;
    // Use this for initialization
    void Start()
    {
        arrayColor = new Color[TexPixelLength, TexPixelLength];
        tex2d = new Texture2D(TexPixelLength, TexPixelLength, TextureFormat.RGB24, true);

        Init();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Init ()
    {
        InitPlane(Color.green);
    }

    void InitPlane(Color col)
    {
        Texture2D tex = new Texture2D(256, 256);
        
        Color[] CalcArray = CalcArrayColor(col);
        tex2d.SetPixels(CalcArray);
        tex2d.Apply();
        
        plane.sprite = Sprite.Create(tex2d, new Rect(0, 0, tex2d.width, tex2d.height), new Vector2(0.5f, 0.5f));

        Destroy(tex);
    }

    Color[] CalcArrayColor(Color endColor)   // 选色板
    {
        Color value = (endColor - Color.white) / (TexPixelLength - 1);
        for (int i = 0; i < TexPixelLength; i++)
        {
            arrayColor[i, TexPixelLength - 1] = Color.white + value * i;
        }
        for (int i = 0; i < TexPixelLength; i++)
        {
            value = (arrayColor[i, TexPixelLength - 1] - Color.black) / (TexPixelLength - 1);
            for (int j = 0; j < TexPixelLength; j++)
            {
                arrayColor[i, j] = Color.black + value * j;
            }
        }
        List<Color> listColor = new List<Color>();
        for (int i = 0; i < TexPixelLength; i++)
        {
            for (int j = 0; j < TexPixelLength; j++)
            {
                listColor.Add(arrayColor[j, i]);
            }
        }

        return listColor.ToArray();
    }


    void InitChooseEndCol()
    {
        Image img = chooseC.transform.FindChild("bg").GetComponent<Image>();

         

    }
    
}

