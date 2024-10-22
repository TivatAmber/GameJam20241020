using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

namespace PuzzleGame
{
    public class AnchorPoint: MonoBehaviour
    {
        [SerializeField] private bool activate;
        [SerializeField] private List<AnchorPoint> nextActivate = new();
        [ReadOnly] [SerializeField] private int targetIndex;

        public int TargetIndex
        {
            get => targetIndex;
            set => targetIndex = value;
        }
        public bool Activate => activate;

        public void ActiveNext()
        {
            foreach (var nextPoint in nextActivate.Where(nextPoint => !nextPoint.activate))
            {
                nextPoint.activate = true;
            }
        }
    }
}