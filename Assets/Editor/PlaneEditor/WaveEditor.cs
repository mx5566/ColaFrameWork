using UnityEngine;
using System.Collections;
using UnityEditor;

namespace ColaFramework.ToolKit
{

    [CustomEditor(typeof(Wave), true)]
    public class WaveEditor : InspectorBase
    {
        private SerializedObject mObject;
        protected override void OnEnable()
        {
            base.OnEnable();

            SerializedProperty tsf = serializedObject.FindProperty("pathPoints");

            mObject = new SerializedObject(target);
        }

        protected override void DrawCustomGUI()
        {
            mObject.Update();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("add"))
            {
                AddPoint();
            }



            EditorGUILayout.EndHorizontal();
        }


        private void AddPoint()
        {
            GameObject go = new GameObject("1");
        }
    }
}
