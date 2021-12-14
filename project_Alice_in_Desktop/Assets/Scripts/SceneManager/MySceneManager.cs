using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Connector.MySceneManager;

namespace MySceneManager
{
    public class MySceneManager : MonoBehaviour, ISceneChange
    {
        // �V�[�����Ǘ����鏈��


        // �����V�[�����ēx�Ăяo��
        void ISceneChange.ReloadScene()
        {
            FadeManager.Instance.LoadScene(SceneManager.GetActiveScene().name, 0.5f);
            Debug.Log("�V�[�������[�h");
        }

        // �V�[����ύX
        void ISceneChange.SceneChange(string name)
        {
            FadeManager.Instance.LoadScene(name, 0.5f);
        }
    }

}
