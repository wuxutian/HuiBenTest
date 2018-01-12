using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;
using System.Text;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Tools {

    public static JSONNode SensitiveWord = null;

    //读取创建预制
    public static GameObject createGameObject(string path)
    {
        if (path == null || path == "") return null;
        GameObject obj = null;
        try
        {
            obj = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            NameReset(obj);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("!!!! path = " + path);
        }
        return obj;
    }

    //读取创建预制 并设置父类
    public static GameObject createGameObjectTr(string path, GameObject go)
    {
        if (path == null || path == "") return null;
        GameObject obj = null;
        try
        {
            obj = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            NameReset(obj);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("!!!! path = " + path);
        }
        try
        {
            obj.transform.parent = go.transform;
            obj.transform.localPosition = Vector3.zero;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("!!!! GO  Tr = Null");
        }
        return obj;
    }


    // 读取对象 Object
    public static UnityEngine.Object LoadResources(string path)
    {
        UnityEngine.Object obj = Resources.Load(path);
        if (obj == null) return null;
        UnityEngine.Object go = UnityEngine.Object.Instantiate(obj);
        return path != null ? go : null;
    }

    // 读取 音效
    public static AudioClip LoadAudio(string path) 
    {
        AudioClip audio = new AudioClip();

        try
        {
            audio = (AudioClip)Resources.Load(path, typeof(AudioClip));
        }
        catch (System.Exception ex)
        {
            Debug.LogError("!!!! audio  = Null  path =" + path);
            return null;
        }
        return audio;
    }

    // 修改替换 图片
    public static void SetObjectImage(GameObject go, string path, string img_path)
    {
        try
        {
            Sprite temp = Resources.Load(img_path, typeof(Sprite)) as Sprite;
            
            go.transform.FindChild(path).GetComponent<Image>().overrideSprite = temp;
        }
        catch
        {
            Debug.LogError("!!!替换图片出错 == " + go.name + " || " + path + " ||" + img_path);
        }
    }


    //返回 text
    public static Text GetUGUI_Text(GameObject go, string path)
    {
        if (go == null || path == null) { return null; }
        Text text;
        if (path == "")
        {
            text = go.GetComponent<Text>();
        }
        else
            text = go.transform.FindChild(path).GetComponent<Text>();

        return text;
    }



    //重置 预制件名字
    public static void NameReset(GameObject go)
    {
        int fpos = go.name.IndexOf("(");
        if (fpos >= 0)
        {
            go.name = go.name.Substring(0, fpos);
        }
    }


    //秒  转 小时 分 秒
    public static string FormatTime_H(int seconds)
    {
        int intH = seconds / 3600;
        string strH = intH < 10 ? "0" + intH.ToString() : intH.ToString();
        int intM = (seconds % 3600) / 60;
        string strM = intM < 10 ? "0" + intM.ToString() : intM.ToString();
        int intS = seconds % 3600 % 60;
        string strS = intS < 10 ? "0" + intS.ToString() : intS.ToString();
        return strH + ":" + strM + ":" + strS;
    }

    //秒  转 分 秒
    public static string FormatTime_M(int seconds)
    {
        int intM = seconds / 60;
        string strM = intM < 10 ? "0" + intM.ToString() : intM.ToString();
        int intS = seconds % 60;
        string strS = intS < 10 ? "0" + intS.ToString() : intS.ToString();
        return strM + ":" + strS;
    }


    // 颜色字符（0xffffffff）转换 color
    public static Color ColorFromString(string colorstring)
    {

        int r = VFromChar(colorstring[0]) * 16 + VFromChar(colorstring[1]);
        int g = VFromChar(colorstring[2]) * 16 + VFromChar(colorstring[3]);
        int b = VFromChar(colorstring[4]) * 16 + VFromChar(colorstring[5]);
        int a = VFromChar(colorstring[6]) * 16 + VFromChar(colorstring[7]);
        return new UnityEngine.Color(r * 1f / 255, g * 1f / 255, b * 1f / 255, a * 1f / 255);
    }
    static int VFromChar(int c)
    {
        if (c >= '0' && c <= '9')
        {
            return c - '0';
        }
        else if (c >= 'A' && c <= 'F')
        {
            return c - 'A' + 10;
        }
        else
        {
            return c - 'a' + 10;
        }
    }

    // 3D物体在2D屏幕上的位置
    public static Vector3 GetUIPosBy3DGameObj(GameObject gobj3d,
                                   Camera camer3d, Camera camera2d, float z, float y)
    {
        Vector3 v1 = camer3d.WorldToViewportPoint(new Vector3(gobj3d.transform.position.x, y, gobj3d.transform.position.z));
        Vector3 v2 = camera2d.ViewportToWorldPoint(v1);
        v2.z = z;
        return v2;
    }

    //设置2d物体 到 3D物体在屏幕上的位置
    public static void SetUIPosBy3DGameObj(GameObject gobj2d, GameObject gobj3d,
                                   Camera camer3d, Camera camera2d, float z, Vector3 offset)
    {
        Vector3 v1 = camer3d.WorldToViewportPoint(gobj3d.transform.position);
        Vector3 v2 = camera2d.ViewportToWorldPoint(v1);
        v2.z = z;
        gobj2d.transform.position = v2 + offset;
    }


    //返回物体内名字为 “” 的gameobject
    static GameObject findGo = null;
    public static GameObject GetNameFindGameObject(GameObject go, string name)
    {
        findGo = null;
        GetFindGameObjectName(go, name);

        if (findGo != null)
        {
            return findGo;
        }
        return findGo;
    }

    static void GetFindGameObjectName(GameObject go, string name)
    {
        bool find = false;
        for (int i = 0; i < go.transform.childCount; i++)
        {
            if (go.transform.GetChild(i).name == name)
            {
                find = true;
                findGo = go.transform.GetChild(i).gameObject;
                return;
            }
        }
        if (!find)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                if (go.transform.GetChild(i).childCount > 0)
                {
                    GetFindGameObjectName(go.transform.GetChild(i).gameObject, name);
                }
            }
        }
    }

    //写 二进制文件
	public static void WriteBytes(string path, byte[] bytes){
		FileStream fs = new FileStream(path, FileMode.Create);
		fs.Write(bytes, 0, bytes.Length);
		fs.Flush();
		fs.Close();
	}

    //写 txt文件
    public static void WriteTxt(string path, string text) 
    {
        FileStream fs = new FileStream(path, FileMode.Create);
        byte[] data = System.Text.Encoding.UTF8.GetBytes(text.ToString());
        fs.Write(data, 0, data.Length);
        fs.Flush();
        fs.Close();
    }

    //删除文件
    public static void RemoveTxt(string path)
    {
        File.Delete(path);
    }

    // 项目内部 读文件 String
    public static string LoadGameString(string path)
    {
        string txt = ((TextAsset)Resources.Load(path)).text;
        return txt;
    }

    // 项目内部 读文件 json
    public static JSONNode LoadGameJson(string path)
    {
        string txt = ((TextAsset)Resources.Load(path)).text;
        JSONNode json = JSONClass.Parse(txt);
        return json;
    }
    
    //项目内  读取二进制文件
	public static byte[] LoadBytes(string path)
	{
		FileStream file = new FileStream(path, FileMode.Open);
		int len = (int)file.Length;
		byte[] byData = new byte[len];
		file.Read(byData, 0, len);
		return byData;
	}

     //外部 读txt文件
    public static string LoadString(string path)
    {
        FileStream file = new FileStream(path, FileMode.Open);
        int len = (int)file.Length;
        byte[] byData = new byte[len];
        file.Read(byData, 0, len);
        string text = Encoding.UTF8.GetString(byData);
        file.Close();
        return text;
    }

    //字符串是否 有中文字
    public static bool IsChinese(string text)
    {
        for (int i = 0; i < text.Length; i++ )
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(text[i].ToString(), @"^[\u4e00-\u9fa5]+$"))
            {
                return true;
            }
        }
        return false;
    }

    //字符串是否 有 特殊符号
    public static bool IsSymbol(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (!char.IsLetter(text[i]) && !char.IsNumber(text[i]))
            {
                return true;
            }
        }
        return false;
    }

    //字符串长度（中文字为2个字符）
    public static int GetStringLength(string text)
    {
        int num = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(text[i].ToString(), @"^[\u4e00-\u9fa5]+$"))
            {
                num++;
            }
        }

        return text.Length + num;
    }

    // 中英字  是否超出长度
    public static bool IsStringLength(string text, int num)
    {
        if (text.Length > num) return true;

        int temp = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(text[i].ToString(), @"^[\u4e00-\u9fa5]+$"))
            {
                temp++;
            }
        }
        Debug.LogError(text + "==" + temp + "===" + text.Length  + "  " + temp);
        if (text.Length + temp > num)
        {
            return true;
        }
        return false;
    }

    //字符串 是否 纯数字
    public static bool IsNumber(string str) 
    {
        for (int i = 0; i < str.Length; i++ )
        {
            if (!Char.IsNumber(str, i))
            {
                return false;
            }
        }
        return true;
    }

    // 是否 是正确 的邮箱地址
    public static bool IsEmail(string str_email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(str_email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    } 

    //解析时间戳
    public static string[] GetTimeStamp_ch(string _time)
    {
        long timeStamp = long.Parse(_time);
        System.DateTime dtStart = System.TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        long lTime = timeStamp * 10000000;

        System.TimeSpan toNow = new System.TimeSpan(lTime);

        System.DateTime dtResult = dtStart.Add(toNow);
        string date = dtResult.ToShortDateString().ToString();
        string time = dtResult.ToString("HH:mm:ss");
        string[] date_arr = date.Split('/');
        string[] time_arr = time.Split(':');
        string secondarr = time_arr[2];
        char[] second = secondarr.ToCharArray();

        string[] result = new string[]{ date_arr[2] + "年" + date_arr[0] + "月" + date_arr[1] + "日",
            time_arr[0] + ":" +time_arr[1] + ":" + second[0] + second[1]};

        return result;
    }
    public static string[] GetTimeStamp(string _time)
    {
        long timeStamp = long.Parse(_time);
        System.DateTime dtStart = System.TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        long lTime = timeStamp * 10000000;

        System.TimeSpan toNow = new System.TimeSpan(lTime);

        System.DateTime dtResult = dtStart.Add(toNow);
        string date = dtResult.ToShortDateString().ToString();
        string time = dtResult.ToString("HH:mm:ss");
        string[] date_arr = date.Split('/');
        string[] time_arr = time.Split(':');
        string secondarr = time_arr[2];
        char[] second = secondarr.ToCharArray();

        string[] result = new string[]{ date_arr[2] + "/" + date_arr[0] + "/" + date_arr[1],
            time_arr[0] + ":" +time_arr[1] + ":" + second[0] + second[1]};

        return result;
    }

    public static bool IsSensitive(string name)
    {
        bool temp = false;

        if (SensitiveWord == null)
        {
            SensitiveWord = LoadGameJson("Data/SensitiveWord");
        }
        for (int i = 0; i < SensitiveWord.Count; i++)
        {
            if (name.IndexOf((string)SensitiveWord[i]) >= 0 )
            {
                temp = true;
            }
        }
        return temp;
    }

    public static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);

        for (int i = 0; i < result.height; ++i)
        {
            for (int j = 0; j < result.width; ++j)
            {
                Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
                result.SetPixel(j, i, newColor);
            }
        }

        result.Apply();
        return result;
    }



}

