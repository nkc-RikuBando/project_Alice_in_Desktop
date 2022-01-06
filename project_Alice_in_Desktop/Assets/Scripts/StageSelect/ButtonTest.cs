using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    // ボタンが押された時にシーンを変更する処理

    private int _clickCount = 0;
    private float _timeCount = 0;

    private void Update()
    {
        TimeCount();
        ClickCountAction();
    }


    // 入力カウントメソッド
    public void OnClickCount()
    {
        if (_clickCount < 2) _clickCount++;
    }


    // 時間計測メソッド
    private void TimeCount()
    {
        // 計測開始
        if (_clickCount == 1)
        {
            _timeCount += Time.deltaTime;
        }
    }

    // 入力カウントアクションメソッド
    private void ClickCountAction()
    {
        // 2秒以内に2回目の入力があればアクションする
        if (_timeCount < 2f)
        {
            SceneChange();
        }
        else
        {
            _clickCount = 0;
            _timeCount = 0;
        }
    }

    // シーン変更メソッド
    private void SceneChange()
    {
        if (_clickCount == 2)
        {
            _clickCount = 0;
            _timeCount  = 0;
            Debug.Log("シーンを変更");
        }
    }

}
