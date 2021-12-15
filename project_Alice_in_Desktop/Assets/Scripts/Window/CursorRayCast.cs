using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CursorRayCast : MonoBehaviour
{
    [SerializeField] private LayerMask mouseTouchableLayer;
    [SerializeField] private Texture2D defaultTex;
    [SerializeField] private Vector2 hotSpot;
    private Texture2D cursorTex;
    private Texture2D currentTex;
    private Collider2D col;

    void Update()
    {
        if (Input.GetMouseButton(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, mouseTouchableLayer);
        Debug.DrawRay((Vector2)ray.origin, (Vector2)ray.direction, Color.red, mouseTouchableLayer);

        if(hit2d.collider!=col)
        {
            col = hit2d.collider;
            
            if (col != null)
            {
                cursorTex = hit2d.collider.GetComponent<CursorChange>().GetTexture();
                Cursor.SetCursor(cursorTex, hotSpot, CursorMode.Auto);
                currentTex = cursorTex;
            }
        }
        else if(hit2d.collider == null && currentTex!=defaultTex )
        {
            Cursor.SetCursor(defaultTex,hotSpot, CursorMode.Auto);
            currentTex = defaultTex;
        }
    }
}
