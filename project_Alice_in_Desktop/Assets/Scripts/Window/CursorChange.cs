using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChange : MonoBehaviour
{
    [SerializeField] private Texture2D cursolTexture;

    public Texture2D GetTexture()
    {
        return cursolTexture;
    }

}
