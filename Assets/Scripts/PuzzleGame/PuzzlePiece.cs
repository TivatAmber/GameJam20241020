using System;
using Tools;
using UnityEngine;

namespace PuzzleGame
{
    [RequireComponent(typeof(Collider2D))]
    public class PuzzlePiece: MonoBehaviour
    {
        private enum Status
        {
            Moving,
            Touched,
            Dragging,
            Done,
        }
        [SerializeField] private int index;
        [ReadOnly] [SerializeField] private float moveSpeed;
        [ReadOnly] [SerializeField] private SpriteRenderer spriteRenderer;
        [ReadOnly] [SerializeField] private Collider2D nowCollider2D;

        [ReadOnly] [SerializeField] private Camera nowCamera;
        [ReadOnly] [SerializeField] private Transform movingTargetTransform;
        [ReadOnly] [SerializeField] private AnchorPoint anchorPoint;
        [ReadOnly] [SerializeField] private KeyCode dragButton;
        [ReadOnly] [SerializeField] private Status status;
        [ReadOnly] [SerializeField] private float endPointRadius;
        [ReadOnly] [SerializeField] private float anchorPointRadius;
        
        public SpriteRenderer SpriteRenderer
        {
            set => spriteRenderer = value;
        }

        public Collider2D Collider2D
        {
            set => nowCollider2D = value;
        }
        public int Index => index;

        public AnchorPoint AnchorPoint
        {
            set => anchorPoint = value;
        }
        private void Reset()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            nowCollider2D = GetComponent<Collider2D>();
        }

        private void Awake()
        {
            status = Status.Moving;
            movingTargetTransform = PuzzleGameManager.Instance.EndPoint;
            endPointRadius = PuzzleGameManager.Instance.EndPointRadius;
            dragButton = PuzzleGameManager.Instance.DragButton;
            moveSpeed = PuzzleGameManager.Instance.PieceMoveSpeed;
            anchorPointRadius = FieldManager.Instance.AnchorRadius;
            nowCamera = Camera.main;
        }
        
        private void Update()
        {
            HandleStatus();
        }

        private void HandleStatus()
        {
            switch (status)
            {
                case Status.Moving:
                    if (CheckDragging())
                    {
                        // TODO More Animation
                        transform.rotation = Quaternion.identity;
                        status = Status.Touched;
                    }
                    transform.position += (movingTargetTransform.position - transform.position).normalized * (moveSpeed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, movingTargetTransform.position) <= endPointRadius)
                    {
                        PuzzleGameManager.Instance.Remove(index);
                        Destroy(gameObject);
                    }
                    break;
                case Status.Touched:
                    if (CheckDragging()) status = Status.Dragging;
                    if (CheckDone())
                    {
                        status = Status.Done;
                        PuzzleGameManager.Instance.Done(index);
                        transform.position = anchorPoint.transform.position;
                        anchorPoint.ActiveNext();
                    }
                    break;
                case Status.Dragging:
                    if (Input.GetKeyUp(dragButton)) status = Status.Touched;
                    var mousePos = nowCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z -= nowCamera.transform.position.z;
                    transform.position = mousePos;
                    break;
                case Status.Done:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool CheckDragging()
        {
            if (!Input.GetKeyDown(dragButton)) return false;
            
            Vector2 mousePos = nowCamera.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(mousePos, Vector2.zero);
            return hit.collider && hit.collider == nowCollider2D;
        }

        private bool CheckDone()
        {
            if (!anchorPoint || !anchorPoint.Activate) return false;
            return Vector3.Distance(anchorPoint.transform.position, transform.position) < anchorPointRadius;
        }
    }
}