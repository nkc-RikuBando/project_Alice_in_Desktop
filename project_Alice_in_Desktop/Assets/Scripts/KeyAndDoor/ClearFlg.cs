using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearFlg : MonoBehaviour, IGetKey
{// ����m���Ă���B

    [SerializeField]private List<GameObject> keyList = new List<GameObject>();

    [System.NonSerialized] public bool clearFlg; // 

    [SerializeField] private string sceneName;        // �V�[���ړ���̖��O
    [SerializeField] private float fadeTime;�@�@�@�@�@// �t�F�[�h���鎞��

    void Start()
    {
        if (keyList.Count <= 0) Clear();
    }

    public void AddKey(GameObject set)
    {
        keyList.Add(set);
    }

    // �����������烊�X�g�������
    public void GetKey(GameObject get)
    {
        keyList.Remove(get);
        if (keyList.Count <= 0)
        {
            Clear();
        }
        Destroy(get);
    }

    public void Clear()
    {
        //FadeManager.Instance.LoadScene(sceneName, fadeTime);
        clearFlg = true;
    }
}
