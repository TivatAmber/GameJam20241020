using System.Collections.Generic;
using ShootGame;
using UnityEditor;
using UnityEngine;

namespace Editor.ShootGame
{
    [CustomEditor(typeof(ShootGameManager))]
    public class ShootGameManagerEditor: UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Fit Prob")) FitProbNum();
            
            
            var t = target as ShootGameManager;
            if (t.WordsPrefab.Count != t.WordsProbList.Count) return;
            var sumProb = 0f;
            for (var i = 0; i < t.WordsPrefab.Count; i++)
            {
                if (!t.WordsPrefab[i]) t.TempProbList[i] = EditorGUILayout.Slider("None", t.WordsProbList[i], 0f, 1f);
                else t.TempProbList[i] = EditorGUILayout.Slider(t.WordsPrefab[i].name + "Prob", t.WordsProbList[i], 0f, 1f);
                sumProb += t.TempProbList[i];
            }

            if (!(sumProb <= 1.0f)) return;
            for (var i = 0; i < t.WordsPrefab.Count; i++) t.WordsProbList[i] = t.TempProbList[i];
        }

        private void FitProbNum()
        {
            var t = target as ShootGameManager;
            Debug.Log(t.WordsPrefab.Count + " B " + t.WordsPrefab.Count);
            if (t.WordsProbList.Count < t.WordsPrefab.Count)
            {
                t.WordsProbList.Add(0f);
                t.TempProbList.Add(0f);
            }
            else if (t.WordsProbList.Count > t.WordsPrefab.Count)
            {
                t.WordsProbList.RemoveAt(t.WordsProbList.Count - 1);
                t.TempProbList.RemoveAt(t.TempProbList.Count - 1);
            }
            Debug.Log(t.WordsPrefab.Count + " F " + t.WordsPrefab.Count);
        }
    }
}