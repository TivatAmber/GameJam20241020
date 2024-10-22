using System;
using System.Collections.Generic;
using PuzzleGame;
using UnityEditor;
using UnityEngine;

namespace Editor.PuzzleGame
{
    [CustomEditor(typeof(PuzzleGameManager))]
    public class PuzzleGameManagerEditor: UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var t = target as PuzzleGameManager;
            base.OnInspectorGUI();
            if (GUILayout.Button("Init")) SettingManager();
            if (GUILayout.Button("Show/Hide Config")) t.ShowConfig = !t.ShowConfig;
            if (GUILayout.Button("Select Folder")) SelectFolder();
        }

        private void SelectFolder()
        {
            var t = target as PuzzleGameManager;
            var puzzlePath = EditorUtility.OpenFolderPanel("Select Puzzle Folder", "Assets/Prefab/PuzzleGame", "");
            puzzlePath = ConvertToUnityPath(puzzlePath);
            if (string.IsNullOrEmpty(puzzlePath)) return;

            var tempList = new List<PuzzlePiece>();
            var prefabs = AssetDatabase.FindAssets("t:Prefab", new[] { puzzlePath });
            foreach (var guid in prefabs)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                if (AssetDatabase.LoadAssetAtPath<GameObject>(path).TryGetComponent<PuzzlePiece>(out var puzzle))
                {
                    tempList.Add(puzzle);
                    Debug.Log("Load " + path);
                }
                else
                {
                    Debug.LogWarning("No Puzzle on " + path);
                }
            }

            if (FieldManager.Instance)
            {
                var fieldTransform = FieldManager.Instance.transform;
                var tempAnchorPointList = new List<AnchorPoint>(FieldManager.Instance.GetComponentsInChildren<AnchorPoint>());
                foreach (var lstPoints in tempAnchorPointList) DestroyImmediate(lstPoints.gameObject);
                tempAnchorPointList.Clear();
                
                foreach (var puzzle in tempList)
                {
                    var nowPoint = Instantiate(t.AnchorPoint.gameObject, fieldTransform).GetComponent<AnchorPoint>();
                    nowPoint.transform.position = fieldTransform.position;
                    nowPoint.TargetIndex = puzzle.Index;
                    nowPoint.name = "AnchorPoint" + puzzle.Index;
                    
                    puzzle.AnchorPoint = nowPoint;
                    tempAnchorPointList.Add(nowPoint);
                }

                FieldManager.Instance.AnchorPoints = tempAnchorPointList;
            }

            t.Pieces = tempList;
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

        private void OnSceneGUI()
        {
            var t = target as PuzzleGameManager;
            if (!t.ShowConfig) return;
            var endPoint = t.EndPoint;
            
            EditorGUI.BeginChangeCheck();
            Handles.color = Color.cyan;
            var newPos = Handles.PositionHandle(endPoint.position, Quaternion.identity);
            Handles.DrawWireDisc(endPoint.position, Vector3.forward, t.EndPointRadius);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(endPoint, "Change EndPoint Pos");
                endPoint.position = newPos;
            }
            
            // Handles.color = Color.red;
            // Handles.DrawWireCube(t.transform.position, Vector3.one * t.FieldSize);
        }

        private void SettingManager()
        {
            var t = target as PuzzleGameManager;
            var endPoint = t.transform.Find("EndPoint");
            var puzzleList = new List<PuzzlePiece>();
            
            t.EndPoint = endPoint;
            t.NowCamera = Camera.main;
        }
    }
}