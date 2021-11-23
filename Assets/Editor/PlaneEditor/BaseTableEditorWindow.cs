using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class BaseTableEditorWindow: EditorWindow
{
    // 窗口名字
    public string winName;
    // 全选
    protected bool isAllSelect = false;
    // 是否可编辑
    protected bool isEdit = false;
    // 记录所有被选中的行的索引
    protected List<int> allSelectRow = new List<int>();
    // 被选中的行 复选框
    protected Dictionary<int, bool> mapToggleRows = new Dictionary<int, bool>();
    // 纪录ScrollView滚动的位置
    protected Vector2 ScrollViewContentOffset = Vector2.zero;
    // 文件名字
    protected string fileName;

    protected void LoadTable()
    {
        
    }

}

