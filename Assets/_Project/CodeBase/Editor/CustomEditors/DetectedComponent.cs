using BarrelHide.GameEntities.Characters;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;

namespace BarrelHide.CustomEditors
{
    [CustomEditor(typeof(GameEntities.Characters.DetectedComponent))]
    internal class DetectedComponent : Editor
    {
        private SerializedProperty detectedComponent;

        private void OnEnable()
        {
            detectedComponent = serializedObject.FindProperty("detectedComponent");
        }

        public override void OnInspectorGUI()
        { 
            DrawDefaultInspector(); 

            var assembly = Assembly.Load("Assembly-CSharp");
            if (assembly != null)
            {

                EditorGUI.BeginChangeCheck();  
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.PrefixLabel("Detected Type");

                Type[] types = assembly.GetTypes(); 
                List<string> optionsForDisplay = new List<string>();
                List<string> optionsForSerialization = new List<string>();

                foreach (var type in types)
                {
                    optionsForDisplay.Add(type.Name);
                    optionsForSerialization.Add(type.FullName);
                }

                int selectedIndex = optionsForSerialization.IndexOf(detectedComponent.stringValue);
                int newSelectedIndex = EditorGUILayout.Popup(selectedIndex, optionsForDisplay.ToArray());

                if (newSelectedIndex != selectedIndex)
                {
                    detectedComponent.stringValue = optionsForSerialization[newSelectedIndex];
                    serializedObject.ApplyModifiedProperties();
                }
                EditorGUILayout.EndHorizontal();
            } 
        }
    }
}
