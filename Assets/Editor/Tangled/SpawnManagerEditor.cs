using System.Collections.Generic;
using System.Linq;
using Tangled;
using UnityEditor;
using UnityEngine;

namespace Editor.Tangled
{
    [CustomEditor(typeof(SpawnManager))]
    public class SpawnManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var t = target as SpawnManager;
            base.OnInspectorGUI();
            if (GUILayout.Button("Show/Hide Spawns")) t.ShowPath = !t.ShowPath;
            if (GUILayout.Button("Init Spawn Points")) SettingSpawnPoints();
        }

        private void SettingSpawnPoints()
        {
            var t = target as SpawnManager;
            var newList = new List<Transform>();
            var blackSpawnPointsFather = t.transform.Find("BlackSpawnPoints");
            var whiteSpawnPoint = t.transform.Find("WhiteSpawnPoint");
            var targetPoint = t.transform.Find("TargetPoint");
            newList.AddRange(blackSpawnPointsFather.GetComponentsInChildren<Transform>()
                .Where(transform => transform.name != blackSpawnPointsFather.name));

            t.BlackSpawnPointsFather = blackSpawnPointsFather;
            t.WhiteSpawnPoint = whiteSpawnPoint;
            t.TargetPoint = targetPoint;

            t.BlackSpawnPoints = newList;
        }
        
        private void OnSceneGUI()
        {
            var t = target as SpawnManager;
            if (!t.ShowPath) return;
            var blackSpawnList = t.BlackSpawnPoints;
            var whiteSpawn = t.WhiteSpawnPoint;
            var targetPoint = t.TargetPoint;
            
            EditorGUI.BeginChangeCheck();
            Handles.color = Color.white;
            Handles.DrawWireDisc(whiteSpawn.position, Vector3.forward, t.PointRadius);
            
            var newWhiteSpawnPos = Handles.PositionHandle(whiteSpawn.position, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(whiteSpawn, "Change White Spawn Point Pos");
                whiteSpawn.position = newWhiteSpawnPos;
            }
            
            EditorGUI.BeginChangeCheck();
            Handles.color = Color.cyan;
            Handles.DrawWireDisc(targetPoint.position, Vector3.forward, t.TargetRadius);

            var newTargetPos = Handles.PositionHandle(targetPoint.position, Quaternion.identity);
            if (!EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(targetPoint, "Change Target Point Pos");
                targetPoint.position = newTargetPos;
            }

            foreach (var blackTransform in blackSpawnList)
            {
                EditorGUI.BeginChangeCheck();
                Handles.color = Color.black;
                Handles.DrawWireDisc(blackTransform.position, Vector3.forward, t.PointRadius);

                var newPos = Handles.PositionHandle(blackTransform.position, Quaternion.identity);
                if (!EditorGUI.EndChangeCheck()) continue;
                Undo.RecordObject(blackTransform, "Change Black Spawn Point Pos");
                blackTransform.position = newPos;
            }
        }
    }
}