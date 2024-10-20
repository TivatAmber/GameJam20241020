using Tools;
using UnityEngine;

namespace ShootGame
{
    public class Words: MonoBehaviour
    {
        [ReadOnly] [SerializeField] private int nowTargetIndex;
        [ReadOnly] [SerializeField] private Vector3 nowTargetPos;
        [ReadOnly] [SerializeField] private float pointRadius;
        [ReadOnly] [SerializeField] private int endIndex;
        [SerializeField] private bool good;
        [SerializeField] private float speed;

        private void Awake()
        {
            nowTargetIndex = 0;
            nowTargetPos = PathManager.Instance.PointList[nowTargetIndex].position;
            pointRadius = PathManager.Instance.PointRadius;
            endIndex = PathManager.Instance.PointList.Count;
            transform.position = nowTargetPos;
        }

        private bool GetTarget()
        {
            return Vector3.Distance(transform.position, nowTargetPos) < pointRadius;
        }

        private bool GetEnd()
        {
            return GetTarget() && nowTargetIndex == endIndex;
        }

        private void SetNextTarget()
        {
            nowTargetIndex += 1;
            if (nowTargetIndex == endIndex) return;
            nowTargetPos = PathManager.Instance.PointList[nowTargetIndex].position;
        }

        private void Update()
        {
            transform.position += (nowTargetPos - transform.position).normalized * (speed * Time.deltaTime);
            if (GetEnd())
            {
                AddScore();
                Die();
            }
            else if (GetTarget()) SetNextTarget();
        }

        private void AddScore()
        {
            if (good) ShootGameManager.Instance.AddGoodNum();
            else ShootGameManager.Instance.AddBadNum();
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}