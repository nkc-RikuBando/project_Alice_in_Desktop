using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Connector.MySceneManager;

namespace MySceneManager
{
    public class MySceneManager : MonoBehaviour, ISceneChange
    {
        // シーンを管理する処理


        // 同じシーンを再度呼び出し
        void ISceneChange.ReloadScene()
        {
            FadeManager.Instance.LoadScene(SceneManager.GetActiveScene().name, 0.5f);
            Debug.Log("シーンリロード");
        }

        // シーンを変更
        void ISceneChange.SceneChange(string name)
        {
            FadeManager.Instance.LoadScene(name, 0.5f);
        }
    }

}
