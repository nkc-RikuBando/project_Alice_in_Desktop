using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using Player;


public class TestMAn : MonoBehaviour
{
    // テスト用

    [SerializeField] private GameObject _gameObject;

    private IPlayerAction _playerAction;
    private PlayerStatus _playerStatus;

    private void Start()
    {
        _playerAction = _gameObject.GetComponent<IPlayerAction>();
        _playerStatus = _gameObject.GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha0)) 
        //{
        //    _playerStatus._InputFlgAction = false;
        //    Debug.Log(_playerStatus._InputFlgAction);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha1)) 
        //{
        //    _playerStatus._InputFlgAction = true;
        //    Debug.Log(_playerStatus._InputFlgAction);
        //}

        ////Debug.Log(_playerAction.ActionKey_Down());

        //if (_playerAction.ActionKey_Down())
        //{
        //    Debug.Log(_playerAction.ActionKey_Down());
        //}

        //if (_playerAction.ActionKey()) 
        //{
        //    Debug.Log(_playerAction.ActionKey());
        //}

        //if (_playerAction.ActionKeyUp())
        //{
        //    Debug.Log(_playerAction.ActionKeyUp());
        //}
    }
}
