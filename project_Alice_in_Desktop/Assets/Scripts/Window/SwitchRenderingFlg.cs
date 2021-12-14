using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRenderingFlg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        IRenderingFlgSettable _renderingFlgSettable = col.gameObject.GetComponent<IRenderingFlgSettable>();
        if (_renderingFlgSettable != null)
        {
            _renderingFlgSettable.SetRenderingFlg(true);
            Debug.Log(col.gameObject.name);
        }

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        IRenderingFlgSettable _renderingFlgSettable = col.gameObject.GetComponent<IRenderingFlgSettable>();
        if (_renderingFlgSettable != null)
        {
            _renderingFlgSettable.SetRenderingFlg(false);
            Debug.Log(col.gameObject.name);
        }

    }

}
