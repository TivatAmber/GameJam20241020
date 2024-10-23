using System.Collections.Generic;
using System.Linq;
using Tools;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PuzzleGame
{
    public class PuzzleGameManager : BaseManager<PuzzleGameManager>
    {
        [ReadOnly] [SerializeField] private bool showConfig;
        [ReadOnly] [SerializeField] private float nowInterval;
        [ReadOnly] [SerializeField] private Transform endPoint;
        [ReadOnly] [SerializeField] private Camera nowCamera;
        [ReadOnly] [SerializeField] private List<PuzzlePiece> pieces = new();
        [ReadOnly] [SerializeField] private List<int> inFieldIndex = new();
        [ReadOnly] [SerializeField] private List<int> doneIndex = new ();

        [SerializeField] private AnchorPoint anchorPointPrefab;
        [SerializeField] private KeyCode dragButton;
        [SerializeField] private float endPointRadius;
        [SerializeField] private float pieceMoveSpeed;
        [SerializeField] private float summonInterval;
        [SerializeField] private float summonRadius;

        public bool ShowConfig
        {
            get => showConfig;
            set => showConfig = value;
        }
        public Camera NowCamera
        {
            set => nowCamera = value;
        }

        public Transform EndPoint
        {
            get => endPoint;
            set => endPoint = value;
        }

        public IReadOnlyList<PuzzlePiece> Pieces
        {
            get => pieces.AsReadOnly();
            set => pieces = new List<PuzzlePiece>(value);
        }

        // public IReadOnlyDictionary<int, AnchorPoint> PointDict
        // {
        //     get => _pointDict;
        //     set => _pointDict = new Dictionary<int, AnchorPoint>(value);
        // }

        public KeyCode DragButton => dragButton;
        public float EndPointRadius => endPointRadius;
        public float PieceMoveSpeed => pieceMoveSpeed;

        public AnchorPoint AnchorPointPrefab => anchorPointPrefab;

        protected override void OnStart()
        {
            base.OnStart();
            inFieldIndex.Clear();
        }

        private void Update()
        {
            // Debug.Log(nowCamera.aspect);
            if (nowInterval >= summonInterval)
            {
                if (Summon())
                    nowInterval -= summonInterval;
            }
            else nowInterval += Time.deltaTime;
            if (doneIndex.Count == pieces.Count) EndGame();
        }

        private bool Summon()
        {
            var tempList =
                (from piece in pieces where !inFieldIndex.Contains(piece.Index) select piece).ToList();
            if (tempList.Count == 0) return false;
            var index = Random.Range(0, tempList.Count);
            Instantiate(tempList[index].gameObject, ClampInRect(Random.insideUnitCircle.normalized * summonRadius),
                Quaternion.Euler(0, 0, Random.Range(0, 360f)));
            inFieldIndex.Add(tempList[index].Index);
            return true;
        }

        private Vector3 ClampInRect(Vector2 pos)
        {
            var xOffset = nowCamera.orthographicSize * nowCamera.aspect / 2f;
            var yOffset = nowCamera.orthographicSize / 2f;
            var minX = transform.position.x - xOffset;
            var maxX = transform.position.x + xOffset;
            var minY = transform.position.y - yOffset;
            var maxY = transform.position.y + yOffset;
            var newX = Mathf.Clamp(pos.x, minX, maxX);
            var newY = Mathf.Clamp(pos.y, minY, maxY);
            return new Vector3(newX, newY, 0f);
        }

        public void Done(int index)
        {
            if (!doneIndex.Contains(index)) doneIndex.Add(index);
        }

        public void Remove(int index)
        {
            if (inFieldIndex.Contains(index)) inFieldIndex.Remove(index);
        }

        public override void EndGame()
        {
            base.EndGame();
        }
    }
}
