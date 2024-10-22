using Tools;
using UnityEngine;

namespace GameBase
{
    public class TouchPoint : MonoBehaviour
    {
        [ReadOnly] [SerializeField] protected int targetIndex;
        [SerializeField] protected bool activate;
        
        public bool Activate => activate;
        public int TargetIndex
        {
            set => targetIndex = value;
        }
    }
}
