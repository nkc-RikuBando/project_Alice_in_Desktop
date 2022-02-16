using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class TipsGenerator : MonoBehaviour
{
    [SerializeField] private GameObject tipsObj;
    private GameObject obj;
    private bool generateFlg = true;
    private bool enterFlg = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!generateFlg) return;
        var player = collision.GetComponent<PlayerSet>();

        // Player‚Ìê‡
        if (player != null)
        {
            // “–‚½‚Á‚½ˆ—
            obj = Instantiate(tipsObj);
            obj.transform.position = transform.position;
            generateFlg = false;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!enterFlg) return;
        var player = collision.GetComponent<PlayerSet>();

        // Player‚Ìê‡
        if (player != null)
        {
            obj.GetComponent<Animator>().SetTrigger("PopUp");
            enterFlg = false;
        }
    }
}
