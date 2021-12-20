using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitHit : MonoBehaviour
{
    [SerializeField] GameObject keyItem;
    // Start is called before the first frame update
    void Start()
    {
        keyItem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var toPlayerHit = collision.gameObject.GetComponent<PlayerStatus>();

        if(toPlayerHit != null)
        {
            Debug.Log("ìñÇΩÇ¡ÇΩéÅÇÀ");
        }
    }
}
