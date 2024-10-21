using Tools;
using UnityEngine;

namespace Tangled.Balls
{
    public class BlackBall : BaseBall
    {
        [ReadOnly] [SerializeField] private Transform whiteBallTransform;
        [ReadOnly] [SerializeField] private float pointRadius;

        bool GetWhiteBall()
        {
            return Vector3.Distance(whiteBallTransform.position, transform.position) < pointRadius * 2;
        }
        protected override void Awake()
        {
            base.Awake();
            whiteBallTransform = TangledManager.Instance.WhiteBallPos;
            pointRadius = SpawnManager.Instance.PointRadius;
        }

        protected void Update()
        {
            if (GetWhiteBall()) TangledManager.Instance.WhiteDestroy();
        }
    }
}