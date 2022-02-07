using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitHit : MonoBehaviour
{
    [SerializeField] GameObject keyItem;
    private Animator keyAnimator;
    private Animator animator;
    private Rigidbody2D rb2d;
    private bool stopFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        stopFlg = false;
        keyItem.SetActive(false);
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
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
            stopFlg = true;
            rb2d.isKinematic = false;
            animator.SetTrigger("Capture");
            Debug.Log("“–‚½‚Á‚½");
            StartCoroutine("AppearKey");
        }
    }

    public bool HitRabbitFlg()
    {
        return stopFlg;
    }

    IEnumerator AppearKey()
    {
        yield return new WaitForSeconds(0.6f);
        keyItem.SetActive(true);
        keyItem.transform.parent = null;
        keyAnimator = keyItem.GetComponent<Animator>();
        keyAnimator.SetTrigger("Spawn");
    }
}
