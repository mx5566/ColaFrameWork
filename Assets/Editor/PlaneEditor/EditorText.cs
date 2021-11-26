using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class EditorTextWindow : EditorWindow
{
    public static void Init()
    {
        var window = GetWindow<EditorTextWindow>("文本列表");
        window.Show();
    }

    List<string> listText;
    //列表显示
    ReorderableList reorderableList;
    //滚动窗口需要
    Vector2 scrollPos;
    // 选中的文本
    public string clickText = string.Empty;

    public void SetTextList(List<string> list)
    {
        this.listText = list;
    }

    private void OnEnable()
    {
        reorderableList = new ReorderableList(listText, typeof(string), true, true, false, false);
        reorderableList.elementHeight = 20;
        reorderableList.drawElementCallback =
            (rect, index, isActive, isFocused) =>
            {
                var element = listText[index];
                rect.height -= 4;
                rect.y += 2;
                if (index == reorderableList.index)
                {
                    GUI.backgroundColor = Color.red;
                }
                else
                {
                    GUI.backgroundColor = Color.yellow;
                }
                EditorGUI.HelpBox(rect, "", MessageType.None);
                EditorGUI.LabelField(rect, element);
                GUI.backgroundColor = Color.white;

            };

        var defaultColor = GUI.backgroundColor;
        reorderableList.drawElementBackgroundCallback = (rect, index, isActive, isFocused) =>
        {
            GUI.backgroundColor = Color.red;
        };
        reorderableList.drawHeaderCallback = (rect) =>
            EditorGUI.LabelField(rect, "片段信息");

        reorderableList.onSelectCallback = (ReorderableList list) =>
        {
            clickText = listText[list.index];
        };
    }

    void OnGUI()
    {
        if (reorderableList != null)
        {
            scrollPos = 
                EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width-10), GUILayout.Height(position.height - 50));
            reorderableList.DoLayoutList();
            EditorGUILayout.EndScrollView();
        }
    }
}