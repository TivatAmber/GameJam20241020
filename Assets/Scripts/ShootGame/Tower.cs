using Tools;
using UnityEngine;

namespace ShootGame
{
    public class Tower: MonoBehaviour
    {
        [SerializeField] private KeyCode attackButton;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float attackInterval;
        [SerializeField] private float nowInterval;

        private void Update()
        {
            if (Input.GetKey(attackButton) && nowInterval > attackInterval)
            {
                Attack();
                nowInterval -= attackInterval;
            } 
            else if (nowInterval < attackInterval)
            {
                nowInterval += Time.deltaTime;
            }
        }
        
        void Attack()
        {
            var targetPos = ShootGameManager.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z -= ShootGameManager.Instance.MainCamera.transform.position.z;
            var now = Instantiate(ShootGameManager.Instance.BulletPrefab, transform.position, Quaternion.identity)
                .GetComponent<Bullet>();
            now.SetSpeed(bulletSpeed).SetForward((targetPos - transform.position).normalized);
        }
    }
}