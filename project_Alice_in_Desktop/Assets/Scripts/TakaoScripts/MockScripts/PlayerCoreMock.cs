using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoreMock : MonoBehaviour
{
    [SerializeField] Vector3 playerPos;
    private Transform playerTransform;

    public Transform PlayerTransform { get => playerTransform; } //プロパティ　読み込みはできるけど書き込みはできない


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        playerTransform.position = playerPos;
    }
    
    public Vector3 GetPlayerPosition()
    {
        return playerPos;
    }
}
