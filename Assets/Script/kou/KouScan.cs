using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KouScan : MonoBehaviour {

    Dictionary<string, int> frame = new Dictionary<string, int>();
    int point_num = 0;
    int[,] point;  //9宫算时存储每个像素上的状态
    Texture2D mTexture = null;

    public int _baiseCol = 100;
    public int _cutCol = 40;

    public KouUI ui = null;

    void Start()
    {
        frame.Add("starX", 0);
        frame.Add("starY", 0);
        frame.Add("overX", 0);
        frame.Add("overY", 0);
    }

    void Update () {
		
	}

    public void JudgeKou(Texture2D mTex)
    {
        mTexture = mTex;

        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        string tempName = Convert.ToInt64(ts.TotalMilliseconds).ToString();

        string path = Application.persistentDataPath + tempName + ".png";

        mTexture = Tools.ScaleTexture(mTexture, mTexture.width / 8, mTexture.height / 8);

        frame["starX"] = 0;
        frame["starY"] = 0;
        frame["overX"] = 0;
        frame["overY"] = 0;

        FrameTexture(mTexture);//计算 边界

        Rect rec = new Rect(frame["starX"], frame["starY"], frame["overX"] - frame["starX"], frame["overY"] - frame["starY"]);

        Texture2D tex = new Texture2D((int)rec.width, (int)rec.height);

        //-----------------------------------------------9宫算

        point = new int[tex.width, tex.height];

        Judge(tex.width, tex.height);//计算线框外的所有点并赋值

        for (int i = 0; i < tex.width; i++) // 开始绘制  凡是 > 0 的点  都是线框外的点
        {
            for (int j = 0; j < tex.height; j++)
            {
                var col = mTexture.GetPixel(frame["starX"] + i, frame["starY"] + j);
                if (point[i, j] > 0)
                {
                    col.a = 0;
                    tex.SetPixel(i, j, col);
                }
                else
                    tex.SetPixel(i, j, col);
            }
        }
        //----------------------------------------
        byte[] bytes = tex.EncodeToPNG();

        System.IO.File.WriteAllBytes(path, bytes);

        var tempTex = new Texture2D((int)rec.width, (int)rec.height);
        tempTex.LoadImage(bytes);

        Sprite sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f));

        ui.img.sprite = sprite;
    }
    
    
    void Judge(int width, int height) // 循环计算
    {
        for (int k = 0; k < point.GetLength(0) * 2; k++)
        {
            if (k == 0) // 第一次左上角定位
            {
                point_num++;
                if (Judge9(0, 0))
                {
                    point[0, 0] = 1;
                }
            }
            else
            {//  循环查询 是否还有需要计算的
                point_num++; // 新点 ID  叠加
                bool find = false;
                int num = 0;
                for (int i = 0; i < point.GetLength(0); i++)
                {
                    for (int j = 0; j < point.GetLength(1); j++)
                    {
                        if (point[i, j] > 0)
                        {
                            if (point[i, j] == point_num - 1)  // 如果图片中还有匹配最新计算的点  就再计算
                            {
                                if (Judge9(i, j))  // true， 找到了新的空点，    false  全图搜索没有新点
                                {
                                    find = true;
                                }
                                num++;
                            }
                        }
                    }
                }
                if (num == 0)//全图搜索没有新点
                {
                    return;
                }
            }
        }
    }

    bool Judge9(int i, int j)// 9宫计算  周边8个点
    {
        bool find = false;

        for (int t = i - 1; t < i + 2; t++)
        {
            for (int y = j - 1; y < j + 2; y++)
            {
                if (t == i && y == j) // 自身点不计算
                {
                    //Debug.LogWarning(1);
                }
                else
                {
                    if (t >= 0 && y >= 0 && t < point.GetLength(0) && y < point.GetLength(1))  //边界外不计算
                    {
                        if (JudgeColor(t, y))//判断颜色
                        {
                            find = true;
                        }
                    }
                }
            }
        }
        return find;
    }

    bool JudgeColor(int i, int j)
    {
        if (point[i, j] < 1) //若这个点没有被赋值和计算过  就开始判断
        {

            var col = mTexture.GetPixel(frame["starX"] + i, frame["starY"] + j); //up
            if (col.r * 255 > _baiseCol && col.g * 255 > _baiseCol && col.b * 255 > _baiseCol)
            {
                point[i, j] = point_num;   // 若颜色是白色   就给这个点赋值，作为线框外的点
                return true;
            }
        }
        return false;
    }


    void FrameTexture(Texture2D tex) // 计算线框边界
    {
        for (int i = 0; i < tex.width; i++)
        {
            for (int j = 0; j < tex.height; j++)
            {
                var temp = tex.GetPixel(i, j);
                if (temp.r * 255 < _cutCol && temp.g * 255 < _cutCol && temp.b * 255 < _cutCol)
                {
                    if (frame["starX"] == 0)
                    {
                        frame["starX"] = i;
                    }
                    else if (i > frame["starX"])
                    {
                        if (frame["overX"] == 0)
                        {
                            frame["overX"] = i;
                        }
                        else if (i > frame["overX"])
                        {
                            frame["overX"] = i;
                        }
                    }
                    else if (i < frame["starX"])
                    {
                        frame["starX"] = i;
                    }

                    if (frame["starY"] == 0)
                    {
                        frame["starY"] = j;
                    }
                    else if (j > frame["starY"])
                    {
                        if (frame["overY"] == 0)
                        {
                            frame["overY"] = j;
                        }
                        else if (j > frame["overY"])
                        {
                            frame["overY"] = j;
                        }
                    }
                    else if (j < frame["starY"])
                    {
                        frame["starY"] = j;
                    }
                }
            }
        }

        //foreach (var key in frame)
        //{
        //    //Debug.LogError(key.Key + " = " + key.Value);
        //}
        if (frame["starX"] > 5)
            frame["starX"] -= 5;

        if (frame["starY"] > 5)
            frame["starY"] -= 5;

        if (frame["overX"] < Screen.width - 5)
            frame["overX"] += 5;

        if (frame["overY"] < Screen.height - 5)
            frame["overY"] += 5;
    }

}
