using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace PuzzleGame
{
    public class FieldManager: Singleton<FieldManager>
    {
        [SerializeField] private float fieldWidth;
        [SerializeField] private float fieldHeight;
        [SerializeField] private float anchorRadius;
        
        [ReadOnly] [SerializeField] private List<AnchorPoint> anchorPoints = new ();
        [ReadOnly] [SerializeField] private bool showConfig;
        
        public float FieldWidth => fieldWidth;
        public float FieldHeight => fieldHeight;
        public Vector2 FieldSize => new(fieldWidth, fieldHeight);
        public float AnchorRadius => anchorRadius;
        
        public IReadOnlyList<AnchorPoint> AnchorPoints
        {
            get => anchorPoints.AsReadOnly();
            set => anchorPoints = new List<AnchorPoint>(value);
        }

        public bool ShowConfig
        {
            get => showConfig;
            set => showConfig = value;
        }

        public bool CheckInField(Vector3 pos)
        {
            var maxX = transform.position.x + fieldWidth / 2f;
            var minX = transform.position.x - fieldWidth / 2f;
            var maxY = transform.position.y + fieldHeight / 2f;
            var minY = transform.position.y - fieldHeight / 2f;
            return minX <= pos.x && pos.x <= maxX &&
                   minY <= pos.y && pos.y <= maxY;
        }
    }
}