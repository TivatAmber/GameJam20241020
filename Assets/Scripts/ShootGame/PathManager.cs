using System.Collections.Generic;
using Tools;
using Unity.VisualScripting;
using UnityEngine;

namespace ShootGame
{
    public class PathManager : Tools.Singleton<PathManager>
    {
        [SerializeField] private float pointRadius;
        [ReadOnly] [SerializeField] private Transform startPoint;
        [ReadOnly] [SerializeField] private Transform endPoint;
        [ReadOnly] [SerializeField] private Transform pointsFather;
        [ReadOnly] [SerializeField] private List<Transform> pointList;
        [ReadOnly] [SerializeField] private bool showPath;

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
        public IList<Transform> PointList
        {
            get => pointList.AsReadOnly();
            set => pointList = new List<Transform>(value);
        }
    }
}
