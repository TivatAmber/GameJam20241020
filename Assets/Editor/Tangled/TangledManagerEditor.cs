using Tangled;
using UnityEditor;

namespace Editor.Tangled
{
    [CustomEditor(typeof(TangledManager))]
    public class TangledManagerEditor: UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
        
    }
}