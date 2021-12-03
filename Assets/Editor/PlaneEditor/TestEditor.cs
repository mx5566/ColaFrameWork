using UnityEngine;
using System.Collections;
using UnityEditor;


namespace ColaFramework.ToolKit
{
    public class NewBehaviourScript : Editor
    {
        [DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.InSelectionHierarchy)]
        static void DrawGameObjectName(Transform transform, GizmoType gizmoType)
        {
            Handles.Label(transform.position, transform.gameObject.name);
        }
    }
}