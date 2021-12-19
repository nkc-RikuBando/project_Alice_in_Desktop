using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyListUI : MonoBehaviour, IGetKey
{
    [SerializeField] private List<GameObject> keyList = new List<GameObject>();

    void Start()
    {
        Debug.Log(keyList.Count);
    }

    public void AddKey(GameObject set)
    {
        keyList.Add(set);
    }

    public void GetKey(GameObject get)
    {
        keyList.Remove(get); // ƒŠƒXƒg‚©‚çŒ®‚ğÁ‚·
    }
}
