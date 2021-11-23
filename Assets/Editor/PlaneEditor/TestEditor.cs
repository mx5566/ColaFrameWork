using UnityEditor;
using UnityEngine;


// Shows info of a GameObject depending on the selected option

public enum OPTIONS
{
    Position = 0,
    Rotation = 1,
    Scale = 2,
}


public class EditorGUIEnumPopup : EditorWindow
{
    OPTIONS display = OPTIONS.Position;

    [MenuItem("Examples/Editor GUI Enum Popup usage")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(EditorGUIEnumPopup));
        window.position = new Rect(0, 0, 220, 80);
        window.Show();
    }

    void OnGUI()
    {
        Transform selectedObj = Selection.activeTransform;

        display = (OPTIONS)EditorGUI.EnumPopup(
            new Rect(3, 3, position.width - 6, 15),
            "Show:",
            display);

        EditorGUI.LabelField(new Rect(0, 20, position.width, 15),
            "Name:",
            selectedObj ? selectedObj.name : "Select an Object");
        if (selectedObj)
        {
            switch (display)
            {
                case OPTIONS.Position:
                    EditorGUI.LabelField(new Rect(0, 40, position.width, 15),
                        "Position:",
                        selectedObj.position.ToString());
                    break;

                case OPTIONS.Rotation:
                    EditorGUI.LabelField(new Rect(0, 40, position.width, 15),
                        "Rotation:",
                        selectedObj.rotation.ToString());
                    break;

                case OPTIONS.Scale:
                    EditorGUI.LabelField(new Rect(0, 40, position.width, 15),
                        "Scale:",
                        selectedObj.localScale.ToString());
                    break;

                default:
                    Debug.LogError("Unrecognized Option");
                    break;
            }
        }

        if (GUI.Button(new Rect(3, position.height - 25, position.width - 6, 24), "Close"))
            this.Close();
    }
}
