using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Window;

public class Test2 : MonoBehaviour,IWindowTouch,IWindowLeave
{
    public void WindowLeaveAction()
    {
        Debug.Log("leave");
    }

    public void WindowTouchAction()
    {
        Debug.Log("touch");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
