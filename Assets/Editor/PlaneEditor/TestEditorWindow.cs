using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorGUILayoutPopup : EditorWindow
{


    public string[] options = new string[] { "cube", "sphere", "plane" };


    public int index = 0;

    [MenuItem("Examples/Editor GUILayout Popup usage")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(EditorGUILayoutPopup));
        window.Show();
    }


    void OnGUI()
    {
        index = EditorGUILayout.Popup(index, options);

        if (GUILayout.Button("222", GUILayout.Width(50)))
        {
            string path = EditorUtility.OpenFolderPanel("Overwrite with prefab", "Assets", "");
            
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}