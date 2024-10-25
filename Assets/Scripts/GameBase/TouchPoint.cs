using Tools;
using UnityEngine;

namespace GameBase
{
    public class TouchPoint : MonoBehaviour
    { 
        [SerializeField] protected int targetIndex;
        [SerializeField] protected bool activate;
        
        public bool Activate => activate;
        public int TargetIndex
        {
            get => targetIndex;
            set => targetIndex = value;
        }
    }
}
