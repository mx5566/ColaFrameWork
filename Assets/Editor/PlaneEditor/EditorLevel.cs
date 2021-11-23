using ColaFramework.Foundation;
using LitJson;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

enum WaveType
{
    WaveType_1 = 1,
    WaveType_2,
    WaveType_3,
    WaveType_4,
};

class LevelMgr : SerializedScriptableObject
{
    public List<Level> Levels;
}


class Level
{
    [LabelText("关卡ID")]
    public int ID;

    [LabelText("关卡阶段")]
    public List<Stage> Stage;

    [LabelText("测试字段")]
    public string Test;
}

class Stage
{
    [LabelText("时间阶段")]
    public int Time;

    [LabelText("触发的敌人")]
    public List<EnemyPlane> Planes;
}

class EnemyPlane
{
    public int ID;

    public WaveType Type;

    public int Num;
}


class EditorLevelWindow : BaseTableEditorWindow
{
    LevelMgr lMgr;

    FieldInfo[] levelFieldInfoArray;
    FieldInfo[] stageFieldInfoArray;


    Dictionary<int, bool> mapTogglePlanes = new Dictionary<int, bool>();
    List<bool> toggleStageValues = new List<bool>();//单选框信息

    // id的最大值
    int maxID = 0;
    List<int> idList = new List<int>();

    [MenuItem("CustomEditor/Plane/Level")]
    public static void GetWindow()
    {
        var window = GetWindow<EditorLevelWindow>("关卡编辑");
        window.Show();
    }

    private void OnEnable()
    {
        LoadLevelData();
    }

    private void OnGUI()
    {
        DrawLevelData();
    }

    private void DrawStageData(int id, ref List<Stage> stages)
    {
        //Debug.LogFormat("DrawStageData");
        SirenixEditorGUI.BeginHorizontalToolbar();

        for (int i = 0; i < stageFieldInfoArray.Length; ++i)
        {
            EditorGUILayout.LabelField(stageFieldInfoArray[i].Name, GUILayout.Width(100));
        }

        // 增加添加按钮
        if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
        {
            var stage = new Stage();
            stage.Time = -1;
            stage.Planes = new List<EnemyPlane>();

            stages.Add(stage);

            mapTogglePlanes[stages.Count - 1] = false;
        }

        SirenixEditorGUI.EndHorizontalToolbar();


        for (int i = 0; i < stages.Count; i++)
        {
            using (var stageScope = new EditorGUILayout.HorizontalScope())
            {
                GUI.Box(stageScope.rect, new GUIContent());

                for (int j = 0; j < stageFieldInfoArray.Length; j++)
                {
                    bool isArray = stageFieldInfoArray[j].FieldType.IsArray;
                    string name = stageFieldInfoArray[j].Name;
                    if (name == "Planes")
                    {
                        if (!mapTogglePlanes.ContainsKey(i))
                        {
                            mapTogglePlanes[i] = false;
                        }


                        string str = "显示";
                        if (mapTogglePlanes[i])
                        {
                            str = "隐藏";
                        }

                        GUIStyle style = EditorStyles.miniPullDown;
                        style.alignment = TextAnchor.MiddleCenter;
                        style.normal.textColor = Color.white;
                        if (mapTogglePlanes[i] = GUILayout.Toggle(mapTogglePlanes[i], str, style, GUILayout.Width(100)))
                        {
                            using (var vscope = new EditorGUILayout.VerticalScope())
                            {

                                GUI.Box(vscope.rect, new GUIContent());
                                FieldInfo[] tempPlanesFieldInfo = typeof(EnemyPlane).GetFields();

                                GUILayout.Space(26);
                                using (var planeScope = new EditorGUILayout.HorizontalScope())
                                {
                                    GUI.Box(planeScope.rect, new GUIContent());
                                    for (int k = 0; k < tempPlanesFieldInfo.Length; ++k)
                                    {
                                        //Debug.LogFormat("tempPlanesFieldInfo Name {0}", tempPlanesFieldInfo[k].Name);

                                        EditorGUILayout.LabelField(tempPlanesFieldInfo[k].Name, GUILayout.Width(100));
                                    }
                                }

                                // 所有的飞机数据
                                for (int l = 0; l < stages[i].Planes.Count; l++)
                                {
                                    using (var vscope1 = new EditorGUILayout.HorizontalScope())
                                    {
                                        for (int m = 0; m < tempPlanesFieldInfo.Length; m++)
                                        {

                                            //Debug.LogFormat("tempPlanesFieldInfo Name {0}  value{1}", tempPlanesFieldInfo[m].Name, tempPlanesFieldInfo[m].GetValue(stages[i].Planes[l]).ToString());

                                            GUI.Box(vscope1.rect, new GUIContent());

                                            if (tempPlanesFieldInfo[m].Name == "ID")
                                            {
                                                stages[i].Planes[l].ID = EditorGUILayout.IntField(stages[i].Planes[l].ID, GUILayout.Width(100));
                                            }
                                            else if (tempPlanesFieldInfo[m].Name == "Type")
                                            {
                                                stages[i].Planes[l].Type = (WaveType)EditorGUILayout.EnumPopup(stages[i].Planes[l].Type, GUILayout.Width(100));

                                                //stages[i].Planes[l].Type = (WaveType)waveType;// EditorGUILayout.IntField(stages[i].Planes[l].Type, GUILayout.Width(100));
                                            }
                                            else if (tempPlanesFieldInfo[m].Name == "Num")
                                            {
                                                stages[i].Planes[l].Num = EditorGUILayout.IntField(stages[i].Planes[l].Num, GUILayout.Width(100));
                                            }
                                        }

                                        // 增加删除按钮
                                        if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
                                        {
                                            stages[i].Planes.RemoveAt(l);

                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (stageFieldInfoArray[j].Name == "Time")
                        {
                            stages[i].Time = EditorGUILayout.IntField(stages[i].Time, GUILayout.Width(100));
                        }
                    }
                }

                // 增加阶段删除按钮
                if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
                {
                    int nCount = stages.Count;
                    stages.RemoveAt(i);
                    mapTogglePlanes.Remove(i);

                    // 删除之前 0 1 2 3 4
                    for (int l = i + 1; l < nCount; l++)
                    {
                        bool bOld = mapTogglePlanes[l];
                        mapTogglePlanes[l - 1] = bOld;
                        mapTogglePlanes.Remove(l);
                    }

                    return;
                }
            }
        }
    }

    private void DrawLevelData()
    {
        using (var tgs = new EditorGUILayout.ToggleGroupScope("是否可编辑", isEdit))
        {
            isEdit = tgs.enabled;

            GUILayout.Space(10);

            using (var hscope = new EditorGUILayout.HorizontalScope())
            {
                GUI.Box(hscope.rect, "");
                // 全选复选框
                bool tempSelect = isAllSelect;
                tempSelect = GUILayout.Toggle(isAllSelect, new GUIContent("全选"), GUILayout.Width(100));
                if (isAllSelect != tempSelect)
                {
                    //
                    isAllSelect = tempSelect;

                    for (int i = 0; i < idList.Count; i++)
                    {
                        mapToggleRows[idList[i]] = isAllSelect;
                    }
                }

                Delete();

                Save();

                Reload();
            }

            GUILayout.Space(10);


            SirenixEditorGUI.BeginHorizontalToolbar();
            ScrollViewContentOffset = EditorGUILayout.BeginScrollView(ScrollViewContentOffset, GUILayout.Width(750), GUILayout.Height(250));

            SirenixEditorGUI.BeginHorizontalToolbar();
            GUILayout.Space(100);
            for (int i = 0; i < levelFieldInfoArray.Length; ++i)
            {
                GUIStyle style = new GUIStyle();
                style.alignment = TextAnchor.MiddleCenter;
                style.fontStyle = FontStyle.Normal;
                style.normal.textColor = Color.white;
                EditorGUILayout.LabelField(levelFieldInfoArray[i].Name, style, GUILayout.Width(100));
            }

            // 增加添加按钮
            if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                var level = new Level();
                level.ID = ++maxID;
                level.Stage = new List<Stage>();
                lMgr.Levels.Add(level);
                mapToggleRows[level.ID] = false;
                idList.Add(level.ID);
            }

            SirenixEditorGUI.EndHorizontalToolbar();


            for (int i = 0; i < lMgr.Levels.Count; i++)
            {
                SirenixEditorGUI.BeginHorizontalToolbar();

                var id = lMgr.Levels[i].ID;
                mapToggleRows[id] = EditorGUILayout.Toggle(mapToggleRows[id], GUILayout.Width(100));
                if (mapToggleRows[id])
                {
                    // 选中的行记录下来 可以批量删除
                    if (!allSelectRow.Contains(i))
                    {
                        allSelectRow.Add(i);
                    }
                }
                else
                {
                    allSelectRow.Remove(i);
                }


                for (int j = 0; j < levelFieldInfoArray.Length; j++)
                {
                    string name = levelFieldInfoArray[j].Name;
                    if (name == "Stage")
                    {

                        if (toggleStageValues.Count < i + 1)
                        {
                            toggleStageValues.Add(false);
                        }

                        GUIStyle style = EditorStyles.miniPullDown;
                        style.alignment = TextAnchor.MiddleCenter;

                        if (toggleStageValues[i] = GUILayout.Toggle(toggleStageValues[i], new GUIContent("stage"), style, GUILayout.Width(100)))
                        //if (EditorGUILayout.DropdownButton(new GUIContent("stage+"), FocusType.Passive, GUILayout.Width(100)))
                        {
                            using (var vscope = new EditorGUILayout.VerticalScope())
                            {
                                GUI.Box(vscope.rect, new GUIContent());
                                GUILayout.Space(26);
                                DrawStageData(lMgr.Levels[i].ID, ref lMgr.Levels[i].Stage);
                            }

                            //Debug.LogFormat("x={0}, y={1}", Event.current.mousePosition.x, Event.current.mousePosition.y);
                            //EditorWindow wTemp = GetWindow<EditorStageWindow>("编辑阶段");//EditorWindow.GetWindowWithRect(typeof(EditorStageWindow), new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 600, 400), true, "编辑阶段");
                            //EditorStageWindow esw = wTemp as EditorStageWindow;

                            //esw.stages = lMgr.Levels[i].Stage;

                            //wTemp.Show();
                            //wTemp.ShowAsDropDown(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 100, 20), new Vector2(320, 160));
                        }
                    }
                    else
                    {
                        if (levelFieldInfoArray[j].Name == "Test")
                        {
                            lMgr.Levels[i].Test = EditorGUILayout.TextField(lMgr.Levels[i].Test, GUILayout.Width(100));
                        }
                        else if (levelFieldInfoArray[j].Name == "ID")
                        {
                            lMgr.Levels[i].ID = EditorGUILayout.IntField(lMgr.Levels[i].ID, GUILayout.Width(100));
                        }

                    }
                }

                // 增加删除按钮
                if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
                {
                    lMgr.Levels.RemoveAt(i);
                    mapToggleRows.Remove(id);
                    allSelectRow.Remove(i);
                    idList.Remove(id);

                    return;
                }


                SirenixEditorGUI.EndHorizontalToolbar();
            }

            EditorGUILayout.EndScrollView();
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }

    private void LoadLevelData()
    {
        levelFieldInfoArray = typeof(Level).GetFields();
        stageFieldInfoArray = typeof(Stage).GetFields();

        bool isExist = FileHelper.IsDirectoryExist(AppConst.jsonFilePath);
        if (!isExist)
        {
            FileHelper.Mkdir(AppConst.jsonFilePath);
        }

        string fileName = AppConst.jsonFilePath + "/level.json";
        string jsonStr = "";


        bool isExistFile = FileHelper.IsFileExist(fileName);
        if (!isExistFile)
        {
            lMgr = ScriptableObject.CreateInstance<LevelMgr>();
            lMgr.Levels = new List<Level>();

            jsonStr = JsonMapper.ToJson(lMgr);

            //Debug.LogFormat("jsonstr111[{0}]", jsonStr);

            FileHelper.WriteString(fileName, jsonStr);
        }

        string strContent = FileHelper.ReadString(fileName);

        //Debug.LogFormat("jsonstrRead [{0}]", strContent);


        lMgr = JsonMapper.ToObject<LevelMgr>(strContent);

        for (int i = 0; i < lMgr.Levels.Count; i++)
        {
            int id = lMgr.Levels[i].ID;

            if (maxID < id)
            {
                maxID = id;
            }

            mapToggleRows[id] = false;
            idList.Add(id);
        }

        //Debug.LogFormat("jsonstr[{0}]", strContent);
    }


    private void OnDisable()
    {
        SaveData();
    }

    private void Delete()
    {
        if (GUILayout.Button("删除选中的内容", GUILayout.Width(100)))
        {
            if (allSelectRow.Count <= 0)
            {

                EditorUtility.DisplayDialog("警告", "没有要删除的数据", "确认");
            }
            else
            {
                EditorUtility.DisplayDialog("警告", "数据删除后无法恢复", "确认", "取消");
            }

            for (int i = 0; i < allSelectRow.Count; i++)
            {
                idList.Remove(lMgr.Levels[allSelectRow[i]].ID);
                mapToggleRows.Remove(lMgr.Levels[allSelectRow[i]].ID);
                lMgr.Levels.RemoveAt(allSelectRow[i]);
            }
            allSelectRow.Clear();

        }
    }

    private void Save()
    {
        if (GUILayout.Button("保存", GUILayout.Width(100)))
        {
            SaveData();


        }
    }

    private void SaveData()
    {
        string fileName = AppConst.jsonFilePath + "/level.json";
        string jsonStr;

        jsonStr = JsonMapper.ToJson(lMgr);

        FileHelper.WriteString(fileName, jsonStr);
    }


    private void Reload()
    {
        if (GUILayout.Button("重新加载文件", GUILayout.Width(100)))
        {
            bool ret = EditorUtility.DisplayDialog("警告", "如果有内容修改会被覆盖掉？", "确定", "取消");
            if (ret)
            {
                // 先清理
                allSelectRow.Clear();
                mapToggleRows.Clear();
                mapTogglePlanes.Clear();
                toggleStageValues.Clear();
                isAllSelect = false;

                LoadLevelData();
            }

            return;
        }
    }
}

