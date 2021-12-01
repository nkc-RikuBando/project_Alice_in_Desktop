using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskSizeChange : MonoBehaviour
{
    [SerializeField] SpriteRenderer waku_sr;
    
    float wakuX, wakuY;
    void Start()
    {

    }

    void Update()
    {
        wakuX = waku_sr.size.x;
        wakuY = waku_sr.size.y;

        transform.localScale = new Vector3(wakuX-1, wakuY-1, 1);
    }
}
