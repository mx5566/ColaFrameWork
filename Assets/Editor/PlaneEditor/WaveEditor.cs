using UnityEngine;
using System.Collections;
using UnityEditor;

namespace ColaFramework.ToolKit
{

    [CustomEditor(typeof(Wave), true)]
    public class WaveEditor : InspectorBase
    {
        private SerializedObject mObject;
        private SerializedProperty tsf;
        protected override void OnEnable()
        {
            base.OnEnable();

            tsf = serializedObject.FindProperty("pathPoints");
            

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
            if (tsf.arraySize >= 3)
            {
                return;
            }

            GameObject p = Instantiate<GameObject>(GameObject.Find("__BezierTemplatePoint"));

            p.transform.position = Vector3.left * (10.0f + Random.Range(1.0, 5.0));

            p.name = "point1";
            p.tag = "points";

            tsf.InsertArrayElementAtIndex(tsf.arraySize);
            tsf.GetArrayElementAtIndex(tsf.arraySize - 1).objectReferenceValue = p.transform;
            // p.transform.SetParent((mObject.targetObject as Wave).gameObject.transform);


        }

        private void OnSceneGUI()
        {
            
        }
    }
}
