using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

public class BgmSet : MonoBehaviour
{
    public void Awake()
    {
        GetGameObject.BgmObj = gameObject;
    }
}
