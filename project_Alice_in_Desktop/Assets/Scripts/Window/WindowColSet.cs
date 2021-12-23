using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

public class WindowColSet : MonoBehaviour
{
    public void Awake()
    {
        GetGameObject.WindowColObject = gameObject;
    }
}
