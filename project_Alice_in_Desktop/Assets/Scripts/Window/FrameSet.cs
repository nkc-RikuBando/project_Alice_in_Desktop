using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

public class FrameSet : MonoBehaviour
{
    public void Awake()
    {
        GetGameObject.FrameObject = gameObject;
    }
}
