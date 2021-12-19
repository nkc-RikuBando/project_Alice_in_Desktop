using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskSizeChangeForUI : MonoBehaviour
{
    [SerializeField] private GameObject wakuObj;
    private SpriteRenderer waku_sr;

    float wakuX, wakuY;

    private RectTransform maskRect;

    // Start is called before the first frame update
    void Start()
    {
        maskRect = gameObject.GetComponent<RectTransform>();
        waku_sr = wakuObj.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        wakuX = waku_sr.size.x;
        wakuY = waku_sr.size.y;

        maskRect.position = wakuObj.transform.position;
        maskRect.sizeDelta = new Vector3(wakuX, wakuY);

    }
}
