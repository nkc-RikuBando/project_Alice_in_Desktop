using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    [SerializeField] LayerMask layerMask = 1 << 8;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ƒNƒŠƒbƒN‚µ‚½‚Æ‚«
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction,layerMask);

            if (hit2d) Debug.Log("ray hit at" + hit2d.collider.name);
        }
    }
}