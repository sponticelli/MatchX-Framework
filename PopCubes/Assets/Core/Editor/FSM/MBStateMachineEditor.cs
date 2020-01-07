using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace ZigZaggle.Core.FSM
{
    [CustomEditor(typeof(MBStateMachine), true)]
    public class MBStateMachineEditor : Editor
    {
        private MBStateMachine _target;
        
        private void OnEnable()
        {
            _target = target as MBStateMachine;
        }
        
        public override void OnInspectorGUI()
        {
            DrawPropertiesExcluding(serializedObject, new string[]
            {
                "currentState",
                "unityEventsFolded",
                "defaultState",
                "verbose",
                "allowReentryStates",
                "Unity Events",
                "onStateExited",
                "onStateEntered"
            });
            
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("FSM", EditorStyles.boldLabel);
            
            if (_target.transform.childCount == 0)
            {
                DrawNotification("Add State child", Color.yellow);
                return;
            }
            
            if (EditorApplication.isPlaying)
            {
                DrawStateChangeButtons();
            }

            serializedObject.Update();

            

            EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultState"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("verbose"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("allowReentryStates"));

            _target.unityEventsFolded = EditorGUILayout.Foldout(_target.unityEventsFolded, "Unity Events", true);
            if (_target.unityEventsFolded)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onStateExited"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onStateEntered"));
            }

            serializedObject.ApplyModifiedProperties();

            if (!EditorApplication.isPlaying)
            {
                DrawDisactivateAllButton();
            }
        }

        private void DrawStateChangeButtons()
        {
            if (_target.transform.childCount == 0) return;
            var currentColor = GUI.color;
            for (var i = 0; i < _target.transform.childCount; i++)
            {
                var current = _target.transform.GetChild(i).gameObject;

                if (_target.currentState != null && current == _target.currentState)
                {
                    GUI.color = Color.green;
                }
                else
                {
                    GUI.color = Color.white;
                }

                if (GUILayout.Button(current.name)) _target.ChangeState(current);
            }

            GUI.color = currentColor;
            if (GUILayout.Button("Exit")) _target.Exit();
        }

        private void DrawDisactivateAllButton()
        {
            GUI.color = Color.red;
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Disactivate All"))
            {
                Undo.RegisterCompleteObjectUndo(_target.transform, "Disactivate All");
                foreach (Transform item in _target.transform)
                {
                    item.gameObject.SetActive(false);
                }
            }

            GUILayout.EndHorizontal();
            GUI.color = Color.white;
        }

        private void DrawNotification(string message, Color color)
        {
            var currentColor = GUI.color;
            GUI.color = color;
            EditorGUILayout.HelpBox(message, MessageType.Warning);
            GUI.color = currentColor;
        }
    }
}