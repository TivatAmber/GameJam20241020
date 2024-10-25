using Tools;
using UnityEngine;

namespace Tangled.Balls
{
    public class WhiteBall: BaseBall
    {
        [SerializeField] private Vector3 targetPos;
        [SerializeField] private float targetRadius;
        protected override void Awake()
        {
            base.Awake();
            targetPos = SpawnManager.Instance.TargetPoint.position;
            targetRadius = SpawnManager.Instance.TargetRadius;
        }

        private bool GetTarget()
        {
            return Vector3.Distance(targetPos, transform.position) < targetRadius;
        }
        protected void Update()
        {
            if (GetTarget()) TangledManager.Instance.WhiteGetTarget();
        }
    }
}