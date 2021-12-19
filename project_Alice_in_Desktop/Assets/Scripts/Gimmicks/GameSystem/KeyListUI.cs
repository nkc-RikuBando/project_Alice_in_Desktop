using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyListUI : MonoBehaviour, IGetKey
{
    [SerializeField] private List<GameObject> keyList = new List<GameObject>();
    [SerializeField] private GameObject waitTimeUI; // 生成するUI
    private GameObject obj;
    private GameObject canvas; // キャンバスの保存

    void Start()
    {
        Debug.Log(keyList.Count);
        canvas = GameObject.Find("Canvas");   // キャンバスの取得
    }

    public void AddKey(GameObject set)
    {
        keyList.Add(set);
    }

    public void GetKey(GameObject get)
    {
        keyList.Remove(get); // リストから鍵を消す
    }
}
