using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Window
{
    public class CursorChange : MonoBehaviour
    {
        // �}�E�X�J�[�\���̕ύX
        [SerializeField] private Texture2D cursolTexture;

        public Texture2D GetTexture()
        {
            return cursolTexture;
        }
    }
}
