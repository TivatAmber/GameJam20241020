using System;
using GameBase;
using Tools;
using UnityEngine;

namespace TouchGame
{
    public class TouchItem: AdsorbedObject
    {
        private enum Status
        {
            Idle,
            Dragging,
            Done,
        }
        [ReadOnly] [SerializeField] private TouchPoint anchorPoint;
        [ReadOnly] [SerializeField] private Status status;
        [ReadOnly] [SerializeField] private float anchorPointRadius;

        public TouchPoint AnchorPoint
        {
            set => anchorPoint = value;
        }
        private void Reset()
        {
            nowCollider2D = GetComponent<Collider2D>();
        }

        private void Awake()
        {
            status = Status.Idle;
            dragButton = TouchGameManager.Instance.DragButton;
            anchorPointRadius = TouchGameManager.Instance.AnchorRadius;
            var nowList = TouchGameManager.Instance.TouchPoints;
            foreach (var point in nowList)
            {
                if (point.TargetIndex != index) continue;
                anchorPoint = point;
                break;
            }
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
                case Status.Dragging:
                    if (Input.GetKeyUp(dragButton)) status = Status.Idle;
                    var mousePos = nowCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z -= nowCamera.transform.position.z;
                    transform.position = mousePos;
                    break;
                case Status.Idle:
                    if (CheckDragging()) status = Status.Dragging;
                    if (CheckDone()) status = Status.Done;
                    break;
                case Status.Done:
                    TouchGameManager.Instance.Done(index);
                    // TODO Some Animation
                    Destroy(gameObject);
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