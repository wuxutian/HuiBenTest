using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorUI : MonoBehaviour {

    [SerializeField]
    JudgeColor judge = null;

    [SerializeField]
    GameObject img = null;

    [SerializeField]
    Text Z_Num = null;

    [SerializeField]
    Text L_Num = null;

    public Text Z_Max = null;
    
    public Text L_Max = null;

    public Text button_Text = null;

    GameObject Z_U_img = null;
    GameObject Z_M_img = null;
    GameObject L_U_img = null;
    GameObject L_M_img = null;
   

    int z_u_r = 0;
    int z_u_g = 0;
    int z_u_b = 0;

    int z_m_r = 0;
    int z_m_g = 0;
    int z_m_b = 0;

    int l_u_r = 0;
    int l_u_g = 0;
    int l_u_b = 0;

    int l_m_r = 0;
    int l_m_g = 0;
    int l_m_b = 0;
    
    // Use this for initialization
    void Start () {
        
        InitImg();

        //StartCoroutine(DelayColor());
    }

    void InitImg()
    {
        z_u_r = (int)(judge.Z_U_C.r * 255);
        z_u_g = (int)(judge.Z_U_C.g * 255);
        z_u_b = (int)(judge.Z_U_C.b * 255);

        z_m_r = (int)(judge.Z_M_C.r * 255);
        z_m_g = (int)(judge.Z_M_C.g * 255);
        z_m_b = (int)(judge.Z_M_C.b * 255);

        l_u_r = (int)(judge.L_U_C.r * 255);
        l_u_g = (int)(judge.L_U_C.g * 255);
        l_u_b = (int)(judge.L_U_C.b * 255);

        l_m_r = (int)(judge.L_M_C.r * 255);
        l_m_g = (int)(judge.L_M_C.g * 255);
        l_m_b = (int)(judge.L_M_C.b * 255);

        
        Z_U_img = GameObject.Instantiate(img);
        Z_U_img.transform.parent = img.transform.parent;
        Z_U_img.GetComponent<Image>().color = judge.Z_U_C;
        Z_U_img.transform.localPosition = new Vector2(-280, 480);
        Z_U_img.name = "Z_U_img";

        Z_M_img = GameObject.Instantiate(img);
        Z_M_img.transform.parent = img.transform.parent;
        Z_M_img.GetComponent<Image>().color = judge.Z_M_C;
        Z_M_img.transform.localPosition = new Vector2(-280, -60);
        Z_M_img.name = "Z_M_img";

        L_U_img = GameObject.Instantiate(img);
        L_U_img.transform.parent = img.transform.parent;
        L_U_img.GetComponent<Image>().color = judge.L_U_C;
        L_U_img.transform.localPosition = new Vector2(280, 480);
        L_U_img.name = "L_U_img";

        L_M_img = GameObject.Instantiate(img);
        L_M_img.transform.parent = img.transform.parent;
        L_M_img.GetComponent<Image>().color = judge.L_M_C;
        L_M_img.transform.localPosition = new Vector2(280, -60);
        L_M_img.name = "L_M_img";


        Z_U_img.GetComponent<Image>().color = judge.Z_U_C;
        Z_M_img.GetComponent<Image>().color = judge.Z_M_C;
        L_U_img.GetComponent<Image>().color = judge.L_U_C;
        L_M_img.GetComponent<Image>().color = judge.L_M_C;

    }

    // Update is called once per frame
    void Update () {
        
    }

    public void TextNum(int z_num, int l_num)
    {
        Z_Num.text = z_num.ToString();
        L_Num.text = l_num.ToString();
    }



    //IEnumerator DelayColor()
    //{
    //while (!judge.initCol) { yield return 0; }
    //Debug.LogError(judge.Z_U_C);


    //}

    private void OnGUI()
    {
        GUIStyle style_1 = new GUIStyle();
        style_1.normal.textColor = Color.red;
        style_1.fontSize = 40;

        GUIStyle style_2 = new GUIStyle();
        style_2.normal.textColor = Color.yellow;
        style_2.fontSize = 40;

        GUI.skin.button.fontSize = 60;

        int x = 20;
        int y = 20;

        if (GUI.RepeatButton(new Rect(x, y, 180, 100), "+"))
        {
            z_u_r += 1;
        }
        GUI.Label(new Rect(x + 180 + 5, y, 60, 60), "z_u_r", style_1);
        GUI.Label(new Rect(x + 180 + 40, y + 50, 60, 60), z_u_r.ToString(), style_2);
        if (GUI.RepeatButton(new Rect(x + 300, y, 180, 100), "-"))
        {
            z_u_r -= 1;
        }

        y = 170;
        if (GUI.RepeatButton(new Rect(x, y, 180, 100), "+"))
        {
            z_u_g += 1;
        }
        GUI.Label(new Rect(x + 180 + 5, y, 60, 60), "z_u_g", style_1);
        GUI.Label(new Rect(x + 180 + 40, y + 50, 60, 60), z_u_g.ToString(), style_2);
        if (GUI.RepeatButton(new Rect(x + 300, y, 180, 100), "-"))
        {
            z_u_g -= 1;
        }

        y = 320;
        if (GUI.RepeatButton(new Rect(x, y, 180, 100), "+"))
        {
            z_u_b += 1;
        }
        GUI.Label(new Rect(x + 180 + 5, y, 60, 60), "z_u_b", style_1);
        GUI.Label(new Rect(x + 180 + 40, y + 50, 60, 60), z_u_b.ToString(), style_2);
        if (GUI.RepeatButton(new Rect(x + 300, y, 180, 100), "-"))
        {
            z_u_b -= 1;
        }

        y = 550;
        if (GUI.RepeatButton(new Rect(x, y, 180, 100), "+"))
        {
            z_m_r += 1;
        }
        GUI.Label(new Rect(x + 180 + 5, y, 60, 60), "z_m_r", style_1);
        GUI.Label(new Rect(x + 180 + 40, y + 50, 60, 60), z_m_r.ToString(), style_2);
        if (GUI.RepeatButton(new Rect(x + 300, y, 180, 100), "-"))
        {
            z_m_r -= 1;
        }

        y = 700;
        if (GUI.RepeatButton(new Rect(x, y, 180, 100), "+"))
        {
            z_m_g += 1;
        }
        GUI.Label(new Rect(x + 180 + 5, y, 60, 60), "z_m_g", style_1);
        GUI.Label(new Rect(x + 180 + 40, y + 50, 60, 60), z_m_g.ToString(), style_2);
        if (GUI.RepeatButton(new Rect(x + 300, y, 180, 100), "-"))
        {
            z_m_g -= 1;
        }

        y = 850;
        if (GUI.RepeatButton(new Rect(x, y, 180, 100), "+"))
        {
            z_m_b += 1;
        }
        GUI.Label(new Rect(x + 180 + 5, y, 60, 60), "z_m_b", style_1);
        GUI.Label(new Rect(x + 180 + 40, y + 50, 60, 60), z_m_b.ToString(), style_2);
        if (GUI.RepeatButton(new Rect(x + 300, y, 180, 100), "-"))
        {
            z_m_b -= 1;
        }



        y = 20;
        x = Screen.width - 200;
        if (GUI.RepeatButton(new Rect(x, y, 180, 100), "-"))
        {
            l_u_r -= 1;
        }
        GUI.Label(new Rect(x - 95, y, 60, 60), "l_u_r", style_1);
        GUI.Label(new Rect(x - 60, y + 50, 60, 60), l_u_r.ToString(), style_2);
        if (GUI.RepeatButton(new Rect(x - 280, y, 180, 100), "+"))
        {
            l_u_r += 1;
        }

        y = 170;
        if (GUI.RepeatButton(new Rect(x, y, 180, 100), "-"))
        {
            l_u_g -= 1;
        }
        GUI.Label(new Rect(x - 95, y, 60, 60), "l_u_g", style_1);
        GUI.Label(new Rect(x - 60, y + 50, 60, 60), l_u_g.ToString(), style_2);
        if (GUI.RepeatButton(new Rect(x - 280, y, 180, 100), "+"))
        {
            l_u_g += 1;
        }

        y = 320;
        if (GUI.RepeatButton(new Rect(x, y, 180, 100), "-"))
        {
            l_u_b -= 1;
        }
        GUI.Label(new Rect(x - 95, y, 60, 60), "l_u_b", style_1);
        GUI.Label(new Rect(x - 60, y + 50, 60, 60), l_u_b.ToString(), style_2);
        if (GUI.RepeatButton(new Rect(x - 280, y, 180, 100), "+"))
        {
            l_u_b += 1;
        }

        y = 550;
        if (GUI.RepeatButton(new Rect(x, y, 180, 100), "-"))
        {
            l_m_r -= 1;
        }
        GUI.Label(new Rect(x - 95, y, 60, 60), "l_m_r", style_1);
        GUI.Label(new Rect(x - 60, y + 50, 60, 60), l_m_r.ToString(), style_2);
        if (GUI.RepeatButton(new Rect(x - 280, y, 180, 100), "+"))
        {
            l_m_r += 1;
        }

        y = 700;
        if (GUI.RepeatButton(new Rect(x, y, 180, 100), "-"))
        {
            l_m_g -= 1;
        }
        GUI.Label(new Rect(x - 95, y, 60, 60), "l_m_g", style_1);
        GUI.Label(new Rect(x - 60, y + 50, 60, 60), l_m_g.ToString(), style_2);
        if (GUI.RepeatButton(new Rect(x - 280, y, 180, 100), "+"))
        {
            l_m_g += 1;
        }

        y = 850;
        if (GUI.RepeatButton(new Rect(x, y, 180, 100), "-"))
        {
            l_m_b -= 1;
        }
        GUI.Label(new Rect(x - 95, y, 60, 60), "l_m_b", style_1);
        GUI.Label(new Rect(x - 60, y + 50, 60, 60), l_m_b.ToString(), style_2);
        if (GUI.RepeatButton(new Rect(x - 280, y, 180, 100), "+"))
        {
            l_m_b += 1;
        }

        judge.Z_U_C = new Color((float)z_u_r / 255, (float)z_u_g / 255, (float)z_u_b / 255, 1);
        judge.Z_M_C = new Color((float)z_m_r / 255, (float)z_m_g / 255, (float)z_m_b / 255, 1);
        judge.L_U_C = new Color((float)l_u_r / 255, (float)l_u_g / 255, (float)l_u_b / 255, 1);
        judge.L_M_C = new Color((float)l_m_r / 255, (float)l_m_g / 255, (float)l_m_b / 255, 1);
        Z_U_img.GetComponent<Image>().color = judge.Z_U_C;
        Z_M_img.GetComponent<Image>().color = judge.Z_M_C;
        L_U_img.GetComponent<Image>().color = judge.L_U_C;
        L_M_img.GetComponent<Image>().color = judge.L_M_C;


    }
}
