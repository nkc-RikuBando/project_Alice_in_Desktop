using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.MySceneManager;
using Player;

public class PlayerDeadObj : MonoBehaviour
{
    // ��ʊO�ɍs����Player�����S���鏈��

    private ISceneChange _sceneChange;
    private GameObject _sceneChangeObj;

    private void Start()
    {
        _sceneChangeObj = GameObject.Find("SceneManager");
        _sceneChange = _sceneChangeObj.GetComponent<ISceneChange>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerCheck = collision.gameObject.GetComponent<PlayerStatus>();

        // ���������̂�Player�̏ꍇ
        if (playerCheck != null)
        {
            // �V�[���������[�h
            _sceneChange.ReloadScene();
        }
    }
}
