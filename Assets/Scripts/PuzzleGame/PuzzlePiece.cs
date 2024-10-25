using System;
using GameBase;
using Tools;
using UnityEngine;

namespace PuzzleGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PuzzlePiece: AdsorbedObject
    {
        private enum Status
        {
            Moving,
            Touched,
            Dragging,
            Done,
        }
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform movingTargetTransform;
        [SerializeField] private AnchorPoint anchorPoint;
        [SerializeField] private Status status;
        [SerializeField] private float endPointRadius;
        [SerializeField] private float anchorPointRadius;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotating;
        [SerializeField] private bool startRotate;
        [SerializeField] private bool done;
        
        [SerializeField] private Sprite sprite;
        
        public SpriteRenderer SpriteRenderer
        {
            set => spriteRenderer = value;
        }

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
            // Debug.Log(PuzzleGameManager.Instance.PointDict.Count);
            var nowList = FieldManager.Instance.AnchorPoints;
            foreach (var point in nowList)
            {
                if (point.TargetIndex != index) continue;
                anchorPoint = point;
                break;
            }

            anchorPointRadius = FieldManager.Instance.AnchorRadius;
            nowCamera = Camera.main;
        }
        
        private void Update()
        {
            HandleStatus();
        }

        private void HandleStatus()
        {
            if (startRotate)
            {
                var nowRotation = transform.rotation.eulerAngles;
                transform.Rotate(rotating * Time.deltaTime, 0f, 0f);
                // Debug.Log(nowRotation);

                if (!done && nowRotation.x > 85f)
                {
                    rotating = -rotating;
                    done = true;
                    spriteRenderer.sprite = sprite;
                }
                if (done && nowRotation.x > 180f)
                {
                    rotating = 0;
                    done = false;
                    nowRotation = -nowRotation;
                    transform.Rotate(nowRotation);
                }
            }

            switch (status)
            {
                case Status.Moving:
                    if (CheckDragging())
                    {
                        transform.rotation = Quaternion.identity;
                        status = Status.Touched;
                        rotating = 30f;
                        done = false;
                        startRotate = true;
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

        private bool CheckDone()
        {
            if (!anchorPoint || !anchorPoint.Activate) return false;
            return Vector3.Distance(anchorPoint.transform.position, transform.position) < anchorPointRadius;
        }
    }
}