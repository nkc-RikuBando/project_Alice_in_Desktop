using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearFlg : MonoBehaviour, IGetKey
{// ����m���Ă���B

    [SerializeField] private List<GameObject> keyList = new List<GameObject>();

    [SerializeField] private FadeManager fadeManager;
    [SerializeField] private string sceneName;
    [SerializeField] private float fadeTime;

    bool flg;

    private void Start()
    {
        //keyList.RemoveAt(0);
    }

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
        flg = true;
        fadeManager.LoadScene(sceneName, fadeTime);
        Debug.Log("�V�[���ړ�?");
    }
}
