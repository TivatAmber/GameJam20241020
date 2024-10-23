using System.Collections.Generic;
using System.Linq;
using Inter;
using UnityEditor;
using UnityEngine;

namespace Editor.Inter
{
    [CustomEditor(typeof(InterManager))]
    public class InterManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Init Objects")) Setting();
        }

        void Setting()
        {
            var t = target as InterManager;
            var newList = new List<GameObject>(from obj in t.transform.GetComponentsInChildren<CanvasRenderer>()
                select obj.gameObject);

            t.ObjectList = newList;
        }
    }
}