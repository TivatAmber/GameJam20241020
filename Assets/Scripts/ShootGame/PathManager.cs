using System.Collections.Generic;
using Tools;
using Unity.VisualScripting;
using UnityEngine;

namespace ShootGame
{
    public class PathManager : Tools.Singleton<PathManager>
    {
        [SerializeField] private float pointRadius;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
         [SerializeField] private Transform pointsFather;
        [SerializeField] private List<Transform> pointList = new();
         [SerializeField] private bool showPath;

        public bool ShowPath
        {
            get => showPath;
            set => showPath = value;
        }

        public float PointRadius => pointRadius;

        public Transform StartPoint
        {
            set => startPoint = value;
        }

        public Transform EndPoint
        {
            set => endPoint = value;
        }

        public Transform PointsFather
        {
            set => pointsFather = value;
        }
        public IReadOnlyList<Transform> PointList
        {
            get => pointList.AsReadOnly();
            set => pointList = new List<Transform>(value);
        }
    }
}
