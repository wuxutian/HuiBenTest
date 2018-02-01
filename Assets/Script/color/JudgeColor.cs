using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeColor : MonoBehaviour
{

    Texture2D texture2D = null;
    Camera cam;
    RenderTexture renderTexture;

    [SerializeField]
    ColorUI UI = null;

    public Color Z_U_C;
    public Color Z_M_C;
    public Color L_U_C;
    public Color L_M_C;

    int Z_num = 0;
    int L_num = 0;

    int Z_num_Max = 0;
    int L_num_Max = 0;
    bool Z_button_State = false;
    bool L_button_State = false;

    public bool initCol = false;
    void Start()
    {
        //StartCoroutine(InitMaxColNum());
    }

    // Update is called once per frame
    void Update()
    {
        //if (initCol)
        {
            JudgeColorButton();
        }
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

        if (!renderTexture)
        { 
            renderTexture = new RenderTexture(Screen.width / 10, Screen.height / 10, -50);
        }

        cam.targetTexture = renderTexture;

        int width = renderTexture.width;
        int height = renderTexture.height;
        texture2D = new Texture2D(width, height, TextureFormat.ARGB32, false);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture2D.Apply();

        //UI.img.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
        JudgeNum();

    }

    void JudgeNum()
    {
        Z_num = 0;
        L_num = 0;

        for (int i = 0; i < texture2D.width; i += 5)
        {
            for (int j = texture2D.height / 3; j < texture2D.height / 3 * 2; j += 5)
            {
                Color col = texture2D.GetPixel(i, j);

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
        //UI.L_U_img.GetComponent<Image>().color = texture2D  .GetPixel(Screen.width / 2 - 50, Screen.height / 2 - 220);
        UI.TextNum(Z_num, L_num);
        //Debug.LogError(Z_num + "   " + L_num);
        //if (initCol)
        {
            Destroy(texture2D);
        }

        if (Z_num > Z_num_Max)
        {
            Z_num_Max = Z_num;
            UI.Z_Max.text = Z_num_Max.ToString();
        }
        if (L_num > L_num_Max)
        {
            L_num_Max = L_num;
            UI.L_Max.text = L_num_Max.ToString();
        }
    }

    IEnumerator InitMaxColNum()
    {

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
            if (Z_num > Z_num_Max)
            {
                Z_num_Max = Z_num;
            }
            if (L_num > L_num_Max)
            {
                L_num_Max = L_num;
            }
        }
        initCol = true;
    }


    void JudgeColorButton()
    {
        if (Z_button_State)
        {
            if (Z_num > Z_num_Max / 2)
            {
                Z_button_State = false;
            }
        }
        else
        {
            if (Z_num < Z_num_Max / 2)
            {
                Z_button_State = true;
                UI.button_Text.text = "Z";
            }
        }

        if (L_button_State)
        {
            if (L_num > L_num_Max / 2)
            {
                L_button_State = false;
            }
        }
        else
        {
            if (L_num < L_num_Max / 2)
            {
                L_button_State = true;
                UI.button_Text.text = "L";
            }
        }
    }
}
