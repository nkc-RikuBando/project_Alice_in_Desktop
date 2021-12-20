using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Window
{
    public class SwitchRenderingFlg : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            IRenderingFlgSettable _renderingFlgSettable = col.gameObject.GetComponent<IRenderingFlgSettable>();
            if (_renderingFlgSettable != null)
            {
                _renderingFlgSettable.SetRenderingFlg(true);
            }

        }

        private void OnTriggerExit2D(Collider2D col)
        {
            IRenderingFlgSettable _renderingFlgSettable = col.gameObject.GetComponent<IRenderingFlgSettable>();
            if (_renderingFlgSettable != null)
            {
                _renderingFlgSettable.SetRenderingFlg(false);
            }

        }
    }
}
