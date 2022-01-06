using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskSizeChangeForUI : MonoBehaviour
{
    private GameObject frameObj;
    private SpriteRenderer waku_sr;

    float wakuX, wakuY;

    private RectTransform maskRect;

    // Start is called before the first frame update
    void Start()
    {
        frameObj = GameObject.Find("frame");
        maskRect = gameObject.GetComponent<RectTransform>();
        waku_sr = frameObj.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        wakuX = waku_sr.size.x;
        wakuY = waku_sr.size.y;

        maskRect.position = frameObj.transform.position;
        maskRect.sizeDelta = new Vector3(wakuX, wakuY);

    }
}
