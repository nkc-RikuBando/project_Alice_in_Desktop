using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearFlg : MonoBehaviour, IGetKey
{// 鍵を知っている。

    [SerializeField]private List<GameObject> keyList = new List<GameObject>();

    [System.NonSerialized] public bool clearFlg; // 

    [SerializeField] private string sceneName;        // シーン移動先の名前
    [SerializeField] private float fadeTime;　　　　　// フェードする時間

    void Start()
    {
        if (keyList.Count <= 0) Clear();
    }

    public void AddKey(GameObject set)
    {
        keyList.Add(set);
    }

    // 鍵が消えたらリストから消す
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
