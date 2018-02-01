using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    bool fishDie = false;

    float fish_s_x = 1;

    public Texture2D tex2D = null;
    Tweener tweener = null;
    
    Vector2 mousePos = Vector2.zero;
    float moveTime = 0;

    bool isOnGUGI = false;

    void Start()
    {
        fish_s_x = transform.localScale.x;
        RandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
#if IPHONE || ANDROID
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
            if (EventSystem.current.IsPointerOverGameObject())
#endif
                isOnGUGI = true;

            else
                isOnGUGI = false;
        }

        if (!isOnGUGI && Input.GetMouseButtonUp(0))
        {
            tweener.Pause();
            tweener = null;

            moveTime = 0;
            mousePos = Input.mousePosition;
            RandomPos();
        }
    }
    
    void RandomPos()
    {
        float x = mousePos.x - Screen.width / 2;
        float y = mousePos.y - Screen.height / 2;
        if (mousePos == Vector2.zero)
        {
            x = Random.Range(-Screen.width * 1, Screen.width * 1);
            y = Random.Range(-Screen.height * 1, Screen.height * 1);
            moveTime = Random.Range(2.5f, 4);
        }
        else {
            moveTime = Random.Range(1f, 2);
            mousePos = Vector2.zero;
        }

            
        if (x > transform.localPosition.x)
        {
            transform.localScale = new Vector3(-fish_s_x, transform.localScale.y, transform.localScale.z);
        }
        else {
            transform.localScale = new Vector3(fish_s_x, transform.localScale.y, transform.localScale.z);
        }
        //Debug.LogError(x + "  " + y + "      " + mousePos + moveTime);
        Image image = transform.GetComponent<Image>();
        tweener = image.rectTransform.DOLocalMove(new Vector3(x, y, 0), moveTime);
        tweener.SetUpdate(true);
        tweener.SetEase(Ease.Linear);
        tweener.onComplete = delegate ()
        {
            if (!fishDie)
                RandomPos();
        };
            
    }
}
