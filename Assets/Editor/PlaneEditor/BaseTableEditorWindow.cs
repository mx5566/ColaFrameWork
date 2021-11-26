using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ColaFramework.Foundation;
using LitJson;
using System.Text.RegularExpressions;

class BaseTableEditorWindow : EditorWindow
{
    // 窗口名字
    public static string winName;
    // 全选
    protected bool isAllSelect = false;
    // 记录所有被选中的行的索引
    protected List<int> allSelectRow = new List<int>();
    // 被选中的行 复选框
    protected Dictionary<int, bool> mapToggleRows = new Dictionary<int, bool>();
    // 纪录ScrollView滚动的位置
    protected Vector2 ScrollViewContentOffset = Vector2.zero;
    // 文件名字
    protected string fileName;
    // 是否可编辑
    protected bool isEdit = false;
    // 可编辑范围变量
    protected EditorGUILayout.ToggleGroupScope toggleGroupScope;

    void OnEnable()
    {
        LoadTable();
    }

    protected virtual void OnLoad()
    {
        Debug.LogFormat("OnLoad...{0}", winName);
    }

    protected void LoadTable()
    {
        bool isExist = FileHelper.IsDirectoryExist(AppConst.jsonFilePath);
        if (!isExist)
        {
            FileHelper.Mkdir(AppConst.jsonFilePath);
        }

        OnLoad();
    }

    public void DrawHead()
    {
        toggleGroupScope = new EditorGUILayout.ToggleGroupScope("是否可编辑", isEdit);
        isEdit = toggleGroupScope.enabled;

        GUILayout.Space(10);

        using (var hscope = new EditorGUILayout.HorizontalScope())
        {
            GUI.Box(hscope.rect, "");
            
            // 全选复选框
            SelectAll();

            // 删除按钮
            Delete();

            // 保存按钮
            Save();

            // 重新加载按钮
            Reload();
        }

    }

    protected void  SelectAll()
    {
        bool tempSelect = isAllSelect;
        tempSelect = GUILayout.Toggle(isAllSelect, new GUIContent("全选"), GUILayout.Width(100));
        if (isAllSelect != tempSelect)
        {
            //
            isAllSelect = tempSelect;

            OnSelectAll();
        }
    }

    protected virtual void OnSelectAll()
    {
        Debug.LogFormat("OnSelectAll...{0}", winName);
    }

    protected void Delete()
    {
        bool ret =  GUILayout.Button("删除选中的内容", GUILayout.Width(100));

        if (ret)
        {
            if (allSelectRow.Count <= 0)
            {
                EditorUtility.DisplayDialog("警告", "没有要删除的数据", "确认");
            }
            else
            {
                bool dTemp = EditorUtility.DisplayDialog("警告", "数据删除后无法恢复", "确认", "取消");
                if (dTemp)
                {
                    OnDelete();
                }
            }
        }
    }

    protected virtual void OnDelete()
    {
        Debug.LogFormat("OnDelete...{0}", winName);
    }

    protected void Save()
    {
        if (GUILayout.Button("保存", GUILayout.Width(100)))
        {
            OnSave();
        }
    }
    protected virtual void OnSave()
    {
        Debug.LogFormat("OnSave...{0}", winName);
    }

    protected virtual void Reload()
    {
        if (GUILayout.Button("重新加载文件", GUILayout.Width(100)))
        {
            bool ret = EditorUtility.DisplayDialog("警告", "如果有内容修改会被覆盖掉？", "确定", "取消");
            if (ret)
            {
                allSelectRow.Clear();
                mapToggleRows.Clear();
                isAllSelect = false;
                OnReload();        
            }
        }
    }

    protected virtual void OnReload()
    {
        Debug.LogFormat("OnReload...{0}", winName);
    }

    protected string ToJson<T>(T t)
    {
        return Regex.Unescape(JsonMapper.ToJson(t));
    }

    protected T LoadJsonData<T>(string fileName)
    {
        string fName = AppConst.jsonFilePath + "/" + fileName;
        string strContent = FileHelper.ReadString(fName);

        T t = JsonMapper.ToObject<T>(strContent);

        return t;
    }
}

