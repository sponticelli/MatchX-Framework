using UnityEngine;
using System.Collections;
using UnityEditor;

namespace ZigZaggle.Core.FSM
{
    [CustomEditor(typeof(MBState), true)]
    public class MBStateEditor : Editor
    {
        private MBState _target;

        private void OnEnable()
        {
            _target = target as MBState;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (!Application.isPlaying)
            {
                GUILayout.BeginHorizontal();
                DrawActivateButton();
                DrawDisactivateAllButton();
                GUILayout.EndHorizontal();
            }
            else
            {
                DrawChangeStateButton();
            }
        }

        private void DrawChangeStateButton()
        {
            GUI.color = Color.green;
            if (GUILayout.Button("Change State"))
            {
                _target.ChangeState(_target.gameObject);
            }
        }

        private void DrawDisactivateAllButton()
        {
            GUI.color = Color.red;
            if (!GUILayout.Button("Disactivate All")) return;
            Undo.RegisterCompleteObjectUndo(_target.transform.parent.transform, "Disactivate All");
            foreach (Transform item in _target.transform.parent.transform)
            {
                item.gameObject.SetActive(false);
            }
        }

        private void DrawActivateButton()
        {
            GUI.color = Color.green;
            if (!GUILayout.Button("Activate")) return;
            foreach (Transform item in _target.transform.parent.transform)
            {
                if (item != _target.transform) item.gameObject.SetActive(false);
                Undo.RegisterCompleteObjectUndo(_target, "Activate");
                _target.gameObject.SetActive(true);
            }
        }
    }
}