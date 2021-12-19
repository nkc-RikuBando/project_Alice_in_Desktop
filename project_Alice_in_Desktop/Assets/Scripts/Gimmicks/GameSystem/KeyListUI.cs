using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyListUI : MonoBehaviour, IGetKey
{
    [SerializeField] private List<GameObject> keyList = new List<GameObject>();
    [SerializeField] private GameObject waitTimeUI; // ��������UI
    private GameObject obj;
    private GameObject canvas; // �L�����o�X�̕ۑ�

    void Start()
    {
        Debug.Log(keyList.Count);
        canvas = GameObject.Find("Canvas");   // �L�����o�X�̎擾
    }

    public void AddKey(GameObject set)
    {
        keyList.Add(set);
    }

    public void GetKey(GameObject get)
    {
        keyList.Remove(get); // ���X�g���献������
    }
}
