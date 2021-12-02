using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionGet : MonoBehaviour
{
    Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = transform.position;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerPos;
    }
}
