using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOneWayWall : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerSet playerSet = collision.GetComponent<PlayerSet>();
        if (playerSet == null) return;
        Player.PlayerCollisionDetection detection = collision.GetComponent<Player.PlayerCollisionDetection>();
        if (detection != null) return; 
        animator.SetTrigger("Flip");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //animator.SetTrigger("Flip");
    }
}
