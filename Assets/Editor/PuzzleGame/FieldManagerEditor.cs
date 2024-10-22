using System.Collections.Generic;
using System.Linq;
using PuzzleGame;
using UnityEditor;
using UnityEngine;

namespace Editor.PuzzleGame
{
    [CustomEditor(typeof(FieldManager))]
    public class FieldManagerEditor: UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            var t = target as FieldManager;
            if (!t.ShowConfig) return;
            var anchorPoints = t.AnchorPoints;
            
            Handles.color = Color.red;
            Handles.DrawWireCube(t.transform.position, new Vector3(t.FieldWidth, t.FieldHeight, 1f));

            foreach (var anchorPoint in anchorPoints)
            {
                var anchorPointTrans = anchorPoint.transform;
                EditorGUI.BeginChangeCheck();
                Handles.color = Color.cyan;
                Handles.DrawWireDisc(anchorPointTrans.position, Vector3.forward, t.AnchorRadius);

                var newPos = Handles.PositionHandle(anchorPointTrans.position, Quaternion.identity);
                if (!EditorGUI.EndChangeCheck() || !t.CheckInField(newPos)) continue;
                Undo.RecordObject(anchorPointTrans, "Change Anchor Point Pos");
                anchorPointTrans.position = newPos;
            }
        }

        public override void OnInspectorGUI()
        {
            var t = target as FieldManager;
            base.OnInspectorGUI();
            if (GUILayout.Button("Show/Hide Config")) t.ShowConfig = !t.ShowConfig;
        }
    }
}