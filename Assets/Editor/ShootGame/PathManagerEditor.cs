using System.Collections.Generic;
using System.Linq;
using ShootGame;
using UnityEditor;
using UnityEngine;

namespace Editor.Chapter1
{
    [CustomEditor(typeof(PathManager))]
    public class PathManagerEditor : UnityEditor.Editor
    {
        private void SettingPath()
        {
            var t = target as PathManager;
            var newList = new List<Transform>();
            var startPoint = t.transform.Find("Start");
            var endPoint = t.transform.Find("End");
            var pointsFather = t.transform.Find("Points");
            newList.Add(startPoint);
            newList.AddRange(pointsFather.GetComponentsInChildren<Transform>().Where(transform => transform.name != pointsFather.name));
            newList.Add(endPoint);

            t.StartPoint = startPoint;
            t.EndPoint = endPoint;
            t.PointsFather = pointsFather;
            t.PointList = newList;
        }

        public override void OnInspectorGUI()
        {
            var t = target as PathManager;
            base.OnInspectorGUI();
            if (GUILayout.Button("Show/Hide Path")) t.ShowPath = !t.ShowPath;
            if (GUILayout.Button("Set Path")) SettingPath();
        }
        
        private void OnSceneGUI()
        {
            var t = target as PathManager;
            if (!t.ShowPath) return;
            var list = t.PointList;
            
            for (var i = 0; i < list.Count; i++)
            {
                EditorGUI.BeginChangeCheck();
                Handles.color = Color.red;
                if (i != list.Count - 1) Handles.DrawLine(list[i].position, list[i + 1].position);
                var newPos = Handles.PositionHandle(list[i].position, Quaternion.identity);
                
                Handles.color = Color.cyan;
                Handles.DrawWireDisc(list[i].position, Vector3.forward, t.PointRadius);
                
                if (!EditorGUI.EndChangeCheck()) continue;
                Undo.RecordObject(list[i], "Change Point Pos");
                list[i].position = newPos;
            }
        }
    }
}