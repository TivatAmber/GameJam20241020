using System.Collections.Generic;
using System.Linq;
using GameBase;
using Tools;
using TouchGame;
using UnityEngine;

namespace PuzzleGame
{
    public class AnchorPoint: TouchPoint
    {
        [SerializeField] private List<AnchorPoint> nextActivate = new();
        public void ActiveNext()
        {
            foreach (var nextPoint in nextActivate.Where(nextPoint => !nextPoint.activate))
            {
                nextPoint.activate = true;
            }
        }
    }
}