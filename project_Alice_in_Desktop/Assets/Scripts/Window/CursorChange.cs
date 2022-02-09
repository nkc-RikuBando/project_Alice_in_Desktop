using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Window
{
    public class CursorChange : MonoBehaviour
    {
        // マウスカーソルの変更
        [SerializeField] private Texture2D cursolTexture;

        public Texture2D GetTexture()
        {
            return cursolTexture;
        }
    }
}
