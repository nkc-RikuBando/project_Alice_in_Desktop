using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Window
{
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

            RaycastHit2D hit2d = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), 10f, mouseTouchableLayer);

            if (hit2d)
            {
                col = hit2d.collider;

                if (col != null && hit2d.collider.GetComponent<CursorChange>().GetTexture()!=null)
                {
                    cursorTex = hit2d.collider.GetComponent<CursorChange>().GetTexture();
                    Cursor.SetCursor(cursorTex, hotSpot, CursorMode.Auto);
                    currentTex = cursorTex;
                }
            }
            else if (hit2d.collider == null && currentTex != defaultTex)
            {
                Cursor.SetCursor(defaultTex, hotSpot, CursorMode.Auto);
                currentTex = defaultTex;
            }
            
        }
    }
}