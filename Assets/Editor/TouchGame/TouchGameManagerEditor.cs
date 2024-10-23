using System.Collections.Generic;
using GameBase;
using TouchGame;
using UnityEditor;
using UnityEngine;

namespace Editor.TouchGame
{
    [CustomEditor(typeof(TouchGameManager))]
    public class TouchGameManagerEditor : UnityEditor.Editor
    { 
        public override void OnInspectorGUI()
        {
            var t = target as TouchGameManager;
            base.OnInspectorGUI();
            if (GUILayout.Button("Init")) SettingManager();
            if (GUILayout.Button("Show/Hide Config")) t.ShowConfig = !t.ShowConfig;
            if (GUILayout.Button("Select Folder")) SelectFolder();
        }
        private void SelectFolder()
        {
            var t = target as TouchGameManager;
            var puzzlePath = EditorUtility.OpenFolderPanel("Select Puzzle Folder", "Assets/Prefab/TouchGame", "");
            puzzlePath = ConvertToUnityPath(puzzlePath);
            if (string.IsNullOrEmpty(puzzlePath)) return;

            var tempList = new List<TouchItem>();
            var prefabs = AssetDatabase.FindAssets("t:Prefab", new[] { puzzlePath });
            foreach (var guid in prefabs)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                if (AssetDatabase.LoadAssetAtPath<GameObject>(path).TryGetComponent<TouchItem>(out var puzzle))
                {
                    tempList.Add(puzzle);
                    Debug.Log("Load " + path);
                }
                else
                {
                    Debug.LogWarning("No Puzzle on " + path);
                }
            }

            var fieldTransform = t.transform;
            var tempAnchorPointList = new List<TouchPoint>(fieldTransform.GetComponentsInChildren<TouchPoint>());
            foreach (var lstPoints in tempAnchorPointList) DestroyImmediate(lstPoints.gameObject);
            tempAnchorPointList.Clear();
            foreach (var puzzle in tempList)
            {
                var nowPoint = Instantiate(t.AnchorPointPrefab.gameObject, fieldTransform).GetComponent<TouchPoint>();
                nowPoint.transform.position = fieldTransform.position;
                nowPoint.TargetIndex = puzzle.Index;
                nowPoint.name = "AnchorPoint" + puzzle.Index;
                    
                tempAnchorPointList.Add(nowPoint);
            }
            t.AdsorbedObjects = tempList;
            t.TouchPoints = tempAnchorPointList;
        }
                
        private string ConvertToUnityPath(string systemPath)
        {
            var projectPath = Application.dataPath;
            if (systemPath.StartsWith(projectPath))
            {
                return "Assets" + systemPath.Substring(projectPath.Length);
            }

            return string.Empty;
        }
                
        private void SettingManager()
        {
            var t = target as TouchGameManager;
            
            t.NowCamera = Camera.main;
        }
        
        private void OnSceneGUI()
        {
            var t = target as TouchGameManager;
            if (!t.ShowConfig) return;
            var anchorPoints = t.TouchPoints;
            
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
    }
}