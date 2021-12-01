using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSetting : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    private bool isChild = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isChild)
            {
                transform.SetParent(parent.transform);
                isChild = true;
            }
            else
            {
                transform.SetParent(null);
                isChild = false;
            }
        }
    }
}
