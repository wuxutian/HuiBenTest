using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour {

    public GameObject baseGo = null;

    public Dictionary<string, Fish> fish_List = new Dictionary<string, Fish>();
    public Dictionary<string, Food> food_List = new Dictionary<string, Food>();
    int fish_num = 0;
    int food_num = 0;
    // Use this for initialization
    void Start () {
        
    }
	

	void Update () {
		
	}


    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 150, 100), "fish"))
        {
            Create("fish");
        }

        if (GUI.Button(new Rect(Screen.width - 170, 20, 150, 100), "food"))
        {
            Create("food");
        }
    }


    void Create(string name)
    {
        GameObject go = GameObject.Instantiate(baseGo);
        Texture2D tex = Tools.LoadTexture2D("yao/" + name);
        
        go.transform.parent = transform;
        go.transform.localScale = Vector3.one;
        //go.transform.localPosition = Vector3.zero;

        go.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        go.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(tex.width, tex.height);
        go.GetComponent<BoxCollider2D>().size = new Vector2(tex.width, tex.height);

        if (name == "fish")
        {
            fish_num++;
            go.name = name + fish_num.ToString();
            Fish fish = go.AddComponent<Fish>();
            fish.tex2D = tex;
            fish_List.Add(go.name, fish);
            go.transform.localPosition = new Vector2(50, 50);
        }
        else if (name == "food")
        {
            food_num++;
            go.name = name + food_num.ToString();
            Food food = go.AddComponent<Food>();
            food.tex2D = tex;
            food_List.Add(go.name, food);
            go.transform.localPosition = new Vector2(-50, -20);
        }
    }
}
