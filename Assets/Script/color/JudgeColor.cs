using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeColor : MonoBehaviour
{
    Camera cam;
    RenderTexture renderTexture;

    [SerializeField]
    Image img = null;

    public Color Z_U_C;
    public Color Z_M_C;
    public Color L_U_C;
    public Color L_M_C;

    int Z_num = 0;
    int L_num = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnWillRenderObject()
    {
        if (!cam)
        {
            GameObject go = new GameObject("__cam");
            cam = go.AddComponent<Camera>();
            go.transform.parent = transform.parent;
            cam.hideFlags = HideFlags.HideAndDontSave;
        }

        cam.CopyFrom(Camera.main);
        //cam.depth = 0;
        //cam.cullingMask = 31;

        if (!renderTexture)
        {
            renderTexture = new RenderTexture(Screen.width / 10, Screen.height / 10, -50);
        }

        cam.targetTexture = renderTexture;
        //cam.Render();

        int width = renderTexture.width;
        int height = renderTexture.height;
        Texture2D texture2D = new Texture2D(width, height, TextureFormat.ARGB32, false);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture2D.Apply();


        //img.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
        JudgeNum(texture2D);

    }

    void JudgeNum(Texture2D tex)
    {
        Z_num = 0;
        L_num = 0;

        for (int i = 0; i < tex.width; i+= 10)
        {
            for (int j = 0; j < tex.height; j+=10)
            {
                Color col = tex.GetPixel(i, j);

                if (col.r > Z_M_C.r && col.r < Z_U_C.r &&
                    col.g > Z_M_C.g && col.g < Z_U_C.g &&
                    col.b > Z_M_C.b && col.b < Z_U_C.b)
                {
                    Z_num++;
                }
                else if (col.r > L_M_C.r && col.r < L_U_C.r &&
                        col.g > L_M_C.g && col.g < L_U_C.g &&
                        col.b > L_M_C.b && col.b < L_U_C.b)
                {
                    L_num++;
                }

            }
        }
        Debug.LogError(Z_num + "   " + L_num);
    }
}
