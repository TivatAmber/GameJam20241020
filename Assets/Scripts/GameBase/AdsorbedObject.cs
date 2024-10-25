using Tools;
using UnityEngine;

namespace GameBase
{
    [RequireComponent(typeof(Collider2D))]
    public class AdsorbedObject: MonoBehaviour
    {
        [SerializeField] protected int index;
        [SerializeField] protected Camera nowCamera;
        [SerializeField] protected Collider2D nowCollider2D;
        [SerializeField] protected KeyCode dragButton;
        public Collider2D Collider2D
        {
            set => nowCollider2D = value;
        }
        public int Index => index;
        protected bool CheckDragging()
        {
            if (!Input.GetKeyDown(dragButton)) return false;
            
            Vector2 mousePos = nowCamera.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(mousePos, Vector2.zero);
            return hit.collider && hit.collider == nowCollider2D;
        }
    }
}