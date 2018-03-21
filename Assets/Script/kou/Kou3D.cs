using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAR;

public class Kou3D : MonoBehaviour {

    public Camera cam;
    RenderTexture renderTexture;
    ImageTargetBaseBehaviour targetBehaviour;

    public KouUI ui = null;
    public KouScan sacn = null;
         
    void Start()
    {
        targetBehaviour = GetComponentInParent<ImageTargetBaseBehaviour>();
        gameObject.layer = 31;
    }

    void Renderprepare()
    {
        if (!cam)
        {
            GameObject go = new GameObject("__cam");
            cam = go.AddComponent<Camera>();
            go.transform.parent = transform.parent;
            cam.hideFlags = HideFlags.HideAndDontSave;

            StartCoroutine(TestScanKou());
        }

        cam.CopyFrom(Camera.main);
        cam.depth = 0;
        cam.cullingMask = 31;

        if (!renderTexture)
        {
            renderTexture = new RenderTexture(Screen.width, Screen.height, -50);
        }

        cam.targetTexture = renderTexture;
        cam.Render();

        GetComponent<Renderer>().material.SetTexture("_MainTex", renderTexture);
    }

    void OnWillRenderObject()
    {
        if (!targetBehaviour || targetBehaviour.Target == null)
            return;
        Vector2 halfSize = targetBehaviour.Target.Size * 0.5f;
        Vector3 targetAnglePoint1 = transform.parent.TransformPoint(new Vector3(-halfSize.x, 0, halfSize.y));
        Vector3 targetAnglePoint2 = transform.parent.TransformPoint(new Vector3(-halfSize.x, 0, -halfSize.y));
        Vector3 targetAnglePoint3 = transform.parent.TransformPoint(new Vector3(halfSize.x, 0, halfSize.y));
        Vector3 targetAnglePoint4 = transform.parent.TransformPoint(new Vector3(halfSize.x, 0, -halfSize.y));
        Renderprepare();
        GetComponent<Renderer>().material.SetVector("_Uvpoint1", new Vector4(targetAnglePoint1.x, targetAnglePoint1.y, targetAnglePoint1.z, 1f));
        GetComponent<Renderer>().material.SetVector("_Uvpoint2", new Vector4(targetAnglePoint2.x, targetAnglePoint2.y, targetAnglePoint2.z, 1f));
        GetComponent<Renderer>().material.SetVector("_Uvpoint3", new Vector4(targetAnglePoint3.x, targetAnglePoint3.y, targetAnglePoint3.z, 1f));
        GetComponent<Renderer>().material.SetVector("_Uvpoint4", new Vector4(targetAnglePoint4.x, targetAnglePoint4.y, targetAnglePoint4.z, 1f));
    }

    IEnumerator TestScanKou()
    {

        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

        Rect mRect = new Rect(0, pos.y - Screen.width / 2, Screen.width, Screen.width);
        //Rect mRect = new Rect(0, pos.y - Screen.height / 2, Screen.width, Screen.width);

        yield return new WaitForEndOfFrame();
        Texture2D mTexture = new Texture2D((int)mRect.width, (int)mRect.height, TextureFormat.RGB24, false);
        mTexture.ReadPixels(mRect, 0, 0);
        mTexture.Apply();

        //Sprite sprite = Sprite.Create(mTexture, new Rect(0, 0, mTexture.width, mTexture.height), new Vector2(0.5f, 0.5f));
        //ui.img.sprite = sprite;

        sacn.JudgeKou(mTexture);
    }

    void OnDestroy()
    {
        if (renderTexture)
            DestroyImmediate(renderTexture);
        if (cam)
            DestroyImmediate(cam.gameObject);
    }
}
