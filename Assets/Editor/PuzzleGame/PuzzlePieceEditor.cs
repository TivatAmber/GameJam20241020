using PuzzleGame;
using UnityEditor;
using UnityEngine;

namespace Editor.PuzzleGame
{
    [CustomEditor(typeof(PuzzlePiece))]
    public class PuzzlePieceEditor: UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Init Piece")) SettingPiece();
        }

        private void SettingPiece()
        {
            var t = target as PuzzlePiece;
            var spriteRenderer = t.GetComponent<SpriteRenderer>();
            var collider2D = t.GetComponent<Collider2D>();
            t.SpriteRenderer = spriteRenderer;
            t.Collider2D = collider2D;
        }
    }
}