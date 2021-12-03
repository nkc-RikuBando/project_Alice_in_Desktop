using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoreMock : MonoBehaviour
{
    [SerializeField] Vector3 playerPos;
    private Transform playerTransform;

    public Transform PlayerTransform { get => playerTransform; } //�v���p�e�B�@�ǂݍ��݂͂ł��邯�Ǐ������݂͂ł��Ȃ�


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
