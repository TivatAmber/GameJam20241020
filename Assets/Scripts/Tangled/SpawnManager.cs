using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tangled
{
    public class SpawnManager: Singleton<SpawnManager>
    {
        [ReadOnly] [SerializeField] private Transform blackSpawnPointsFather;
        [ReadOnly] [SerializeField] private Transform whiteSpawnPoint;
        [ReadOnly] [SerializeField] private List<Transform> blackSpawnPoints = new();
        [ReadOnly] [SerializeField] private Transform targetPoint;
        [SerializeField] private float pointRadius;
        [SerializeField] private float targetRadius;
        [ReadOnly] [SerializeField] private bool showPath;
        public bool ShowPath
        {
            get => showPath;
            set => showPath = value;
        }
        public Transform BlackSpawnPointsFather
        {
            set => blackSpawnPointsFather = value;
        }

        public Transform WhiteSpawnPoint
        {
            get => whiteSpawnPoint;
            set => whiteSpawnPoint = value;
        }
        public IReadOnlyList<Transform> BlackSpawnPoints
        {
            get => blackSpawnPoints.AsReadOnly();
            set => blackSpawnPoints = new List<Transform>(value);
        }

        public Transform TargetPoint
        {
            get => targetPoint;
            set => targetPoint = value;
        }
        

        public float PointRadius => pointRadius;
        public float TargetRadius => targetRadius;
    }
}