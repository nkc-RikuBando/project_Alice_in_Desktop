using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

public class PlayerSet : MonoBehaviour
{
    public void Awake()
    {
        GetGameObject.playerObject = gameObject;
    }
}
