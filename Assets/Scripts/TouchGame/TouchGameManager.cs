using System;
using System.Collections.Generic;
using GameBase;
using Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace TouchGame
{
    public class TouchGameManager : Singleton<TouchGameManager>
    {
        [ReadOnly] [SerializeField] private bool showConfig;
        [ReadOnly] [SerializeField] private Camera nowCamera;
        [ReadOnly] [SerializeField] private List<TouchItem> touchItems = new();
        [ReadOnly] [SerializeField] private List<int> doneIndex = new ();
        
        [Header("Size")]
        [SerializeField] private float fieldWidth;
        [SerializeField] private float fieldHeight;
        [Header("Game Set")]
        [SerializeField] private TouchPoint anchorPointPrefab;
        [SerializeField] private KeyCode dragButton;
        [SerializeField] private float anchorPointRadius;

        public bool ShowConfig
        {
            get => showConfig;
            set => showConfig = value;
        }
        public Camera NowCamera
        {
            set => nowCamera = value;
        }
        public IReadOnlyList<TouchItem> AdsorbedObjects
        {
            get => touchItems.AsReadOnly();
            set => touchItems = new List<TouchItem>(value);
        }
        public float FieldWidth => fieldWidth;
        public float FieldHeight => fieldHeight;
        public TouchPoint AnchorPointPrefab => anchorPointPrefab;
        public float AnchorRadius => anchorPointRadius;
        
        public KeyCode DragButton => dragButton;
        
        public bool CheckInField(Vector3 pos)
        {
            var maxX = transform.position.x + fieldWidth / 2f;
            var minX = transform.position.x - fieldWidth / 2f;
            var maxY = transform.position.y + fieldHeight / 2f;
            var minY = transform.position.y - fieldHeight / 2f;
            return minX <= pos.x && pos.x <= maxX &&
                   minY <= pos.y && pos.y <= maxY;
        }
        public void Done(int index)
        {
            if (!doneIndex.Contains(index)) doneIndex.Add(index);
        }
        private void Update()
        {
            if (doneIndex.Count == touchItems.Count) EndGame();
        }

        private void EndGame()
        {
            // TODO
        }
    }
}
