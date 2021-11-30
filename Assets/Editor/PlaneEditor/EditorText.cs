using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;

namespace ColaFramework.ToolKit
{
    public class EditorTextWindow : EditorWindow
    {
        List<string> listText;
        // 列表显示
        ReorderableList reorderableList;
        // 滚动窗口需要
        Vector2 scrollPos;
        // 选中的文本
        public string clickText = string.Empty;
        // 点击的时间
        private double clickTime = 0f;
        // 父窗口
        public BaseTableEditorWindow parentWin;
        // key
        public string key;

        public void SetTextList(List<string> list)
        {
            listText = list;
        }

        public void SetInt(ref int id)
        {
            
        }

        public BaseTableEditorWindow ParentWin
        {
            get
            {
                return parentWin;
            }
            set
            {
                parentWin = value;
            }
        }

        private void OnEnable()
        {
            reorderableList = new ReorderableList(listText, typeof(string), false, true, false, false);
            reorderableList.elementHeight = 20;
            reorderableList.drawElementCallback =
                (rect, index, isActive, isFocused) =>
                {
                    var element = reorderableList.list[index];
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
                    EditorGUI.LabelField(rect, element.ToString());
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
                clickText = list.list[list.index].ToString();

                // Debug.LogFormat("------------clickText");
                // 如何两次点击的间隔
                if (EditorApplication.timeSinceStartup - clickTime < 0.3f)
                {
                    if (parentWin != null)
                    {
                        parentWin.keyValuePairs[key] = clickText;
                        parentWin.Repaint();
                    }
                }

                clickTime = EditorApplication.timeSinceStartup;
            };
            reorderableList.onRemoveCallback = (ReorderableList list) =>
            {
                //弹出一个对话框
                if (EditorUtility.DisplayDialog("警告", "是否确定删除", "是", "否"))
                {
                    //当点击 "是"
                    ReorderableList.defaultBehaviours.DoRemoveButton(list);
                }
            };

            reorderableList.onAddCallback = (ReorderableList list) =>
            {
                ReorderableList.defaultBehaviours.DoAddButton(list);
            };
        }

        void OnGUI()
        {
            if (reorderableList != null)
            {
                if (reorderableList.list == null)
                {
                    reorderableList.list = this.listText;
                }
                scrollPos =
                    EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width - 10), GUILayout.Height(position.height - 50));
                reorderableList.DoLayoutList();
                EditorGUILayout.EndScrollView();
            }
        }

        private void OnDisable()
        {
            if (parentWin != null)
            {
                parentWin.editorWindows.Remove(this);
                parentWin.keyValuePairs.Remove(key);
            }
        }
    }
}