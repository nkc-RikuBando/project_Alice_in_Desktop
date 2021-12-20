using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.MySceneManager;
using Player;

public class PlayerDeadObj : MonoBehaviour
{
    // 画面外に行くとPlayerが死亡する処理

    [SerializeField] private GameObject _fadeObj;  

    private ISceneChange _sceneChange;
    private FadeEffect _fadeEffect;
    private GameObject _sceneChangeObj;

    private void Start()
    {
        _sceneChangeObj = GameObject.Find("SceneManager");
        _sceneChange = _sceneChangeObj.GetComponent<ISceneChange>();
        _fadeEffect = _fadeObj.GetComponent<FadeEffect>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerCheck = collision.gameObject.GetComponent<PlayerStatus>();

        // 当たったのがPlayerの場合
        if (playerCheck != null)
        {
            // シーンをリロード
            _fadeEffect.StartOutsideEffect();
        }
    }
}
