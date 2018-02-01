using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour {
    
    bool foodDie = true;
    int HP = 4;
    
    public Texture2D tex2D = null;

    MainGame main = null;

    // Use this for initialization
    void Start () {
        StartCoroutine(RandomPos());
        main = transform.parent.GetComponent<MainGame>();
        tex2D = GetComponent<Image>().sprite.texture;
        byte[] bytes = tex2D.EncodeToPNG();
        Texture2D tex = new Texture2D(tex2D.width, tex2D.height);
        tex.LoadImage(bytes);
        tex.Apply();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tex.SetPixel(i, j, new Color(0, 0, 0, 0));
            }
        }
        tex.SetPixel(0,0, new Color(0, 0, 0, 0));
        Sprite s = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        GetComponent<Image>().sprite = s;
    }
	
	// Update is called once per frame
	void Update () {
		 
	}

    void OnTriggerEnter2D(Collider2D col)
    {

        TextureControl(col.name);
    }
    void OnTriggerExit2D(Collider2D col)
    { }


    void TextureControl(string name)
    {
        if (HP == 0) { return; }
        if (name.Substring(0, 4) != "fish") { return; }

        Fish fish = main.fish_List[name];

        JudgePos(fish);

        tex2D = GetComponent<Image>().sprite.texture;
        byte[] bytes = tex2D.EncodeToPNG();

        Texture2D tex = new Texture2D(tex2D.width, tex2D.height);
        tex.LoadImage(bytes);
        tex.Apply();

        FoodAni(tex);
    }

    //计算 碰撞点
    void JudgePos(Fish fish)
    {
        Vector2 hPos = fish.transform.localPosition;
        Vector2 dPos = transform.localPosition;

        hPos = new Vector2(hPos.x + Screen.width / 2, hPos.y + Screen.height / 2);
        dPos = new Vector2(dPos.x + Screen.width / 2, dPos.y + Screen.height / 2);


        float temp_HX = 0;
        float temp_HY = 0;
        float temp_DX = 0;
        float temp_DY = 0;

        Vector2 pos = Vector2.zero;

        if (hPos.x > dPos.x)
        {
            temp_HX = hPos.x - fish.tex2D.width / 2;
            temp_DX = dPos.x + tex2D.width / 2;
            pos.x = (temp_DX - temp_HX) / 2;
        }
        else
        {
            temp_HX = hPos.x + fish.tex2D.width / 2;
            temp_DX = dPos.x - tex2D.width / 2;
            pos.x = (temp_HX - temp_DX) / 2;
        }

        if (hPos.y > dPos.y)
        {
            temp_HY = hPos.y - fish.tex2D.height / 2;
            temp_DY = dPos.y + tex2D.height / 2;
            pos.y = (temp_DY - temp_HY) / 2;
        }
        else {
            temp_HY = hPos.y + fish.tex2D.height / 2;
            temp_DY = dPos.y - tex2D.height / 2;
            pos.y = (temp_HY - temp_DY) / 2;
        }

        Debug.LogError(pos);
    }


    //  抠图
    void FoodAni(Texture2D tex)
    {
        int num_x = 0;
        int num_y = 0;
        if (HP == 1)
        {
            //num_x = tex.width / 2;
            //num_y = tex.height / 2;
            HP = 0;
            GetComponent<Image>().sprite = null;
            GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
        else if (HP == 2)
        {
            num_x = tex.width / 2;
            num_y = 0;
        }
        else if (HP == 3)
        {
            num_x = 0;
            num_y = 0;
        }
        else if (HP == 4)
        {
            num_x = 0;
            num_y = tex.height / 2;
        }
        if (HP > 0)
        {
            //if (tex.GetPixel(20, 20).r != 0)
            {
                for (int i = 0; i < tex.width / 2; i++)
                {
                    for (int j = 0; j < tex.height / 2; j++)
                    {
                        tex.SetPixel(num_x + i, num_y + j, new Color(0, 0, 0, 0));
                    }
                }
            }
            //yield return new WaitForSeconds(0.2f);

            Sprite s = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            GetComponent<Image>().sprite = s;
            HP--;
            //Destroy(tex2D);
            //tex2D = null;
        }
    }


    IEnumerator RandomPos()
    {
        float moveTime = 0;
        while (foodDie)
        {
            yield return new WaitForSeconds(moveTime);
            int x = Random.Range(-Screen.width * 1, Screen.width * 1);
            int y = Random.Range(-Screen.height * 1, Screen.height * 1);
            //int x = Random.Range(20, 20);
            //int y = Random.Range(20, 20);

            Vector3 pos = transform.localPosition;

            float dis_x = 0;
            if (x > pos.x)
            {
                dis_x = x - pos.x;
            }
            else if (pos.x > x)
            {
                dis_x = pos.x - x;
            }
            float dis_y = 0;
            if (y > pos.y)
            {
                dis_y = y - pos.y;
            }
            else if (pos.y > y)
            {
                dis_y = pos.y - y;
            }
            if (dis_x > dis_y)
            {
                moveTime = dis_x / 100;
            }
            else
            {
                moveTime = dis_y / 100;
            }
            
            Image image = transform.GetComponent<Image>();
            Tweener tweener = image.rectTransform.DOLocalMove(new Vector3(x, y, 0), moveTime);
            tweener.SetUpdate(true);
            tweener.SetEase(Ease.Linear);
            tweener.onComplete = delegate ()
            {
                if (HP == 0)
                {
                    foodDie = false;
                    Destroy(gameObject);
                }
            };
        }
    }
}
