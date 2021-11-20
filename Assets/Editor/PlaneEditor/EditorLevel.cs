using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using ColaFramework.Foundation;
using LitJson;
using Sirenix.Utilities.Editor;
using System.Reflection;
using Sirenix.OdinInspector.Editor;

class LevelMgr: SerializedScriptableObject
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

    public int Type;

    public int Num;
}

class EditorStageWindow: OdinEditorWindow
{
    public List<Stage> stages;
    FieldInfo[] stageFieldInfoArray;
    List<bool> toggleValues = new List<bool>();//单选框信息
    List<bool> togglePlanesValues = new List<bool>();//单选框信息
    int selectedIndex = -1;//当前选择的某行数据下标

    protected override void OnEnable()
    {
        base.OnEnable();
        stageFieldInfoArray = typeof(Stage).GetFields();
    }

    protected override void OnGUI()
    {
        DrawStageData();
    }

    void ChangeSelect(int index)
    {
        if (selectedIndex != index)
        {
            if (selectedIndex != -1)
            {
                toggleValues[selectedIndex] = false;
            }
            toggleValues[index] = true;
        }
        selectedIndex = index;
    }

    private void DrawStageData()
    {
        SirenixEditorGUI.BeginHorizontalToolbar();
        GUILayout.Space(100);

        for (int i = 0; i < stageFieldInfoArray.Length; ++i)
        {
            EditorGUILayout.LabelField(stageFieldInfoArray[i].Name, GUILayout.Width(100));
        }
        SirenixEditorGUI.EndHorizontalToolbar();


        for (int i = 0; i < stages.Count; i++)
        {
            using (var stageScope = new EditorGUILayout.HorizontalScope())
            {
                GUI.Box(stageScope.rect, new GUIContent());

                toggleValues.Add(false);
                if (toggleValues[i] = EditorGUILayout.Toggle(toggleValues[i], GUILayout.Width(100)))
                {
                    if (selectedIndex != i)
                    {
                        ChangeSelect(i);
                    }
                }

                for (int j = 0; j < stageFieldInfoArray.Length; j++)
                {
                    bool isArray = stageFieldInfoArray[j].FieldType.IsArray;
                    string name = stageFieldInfoArray[j].Name;
                    if (name == "Planes")
                    {
                        if (togglePlanesValues.Count < i+1)
                        {
                            togglePlanesValues.Add(false);
                        }

                        string str = "显示";
                        if (togglePlanesValues[i])
                        {
                            str = "隐藏";
                        }

                        if (togglePlanesValues[i] = GUILayout.Toggle(togglePlanesValues[i], str, GUILayout.Width(100)))
                        //if (GUILayout.Button("+", GUILayout.Width(100)))
                        {
                            using (var vscope = new EditorGUILayout.VerticalScope())
                            {
                                
                                GUI.Box(vscope.rect, new GUIContent());
                                FieldInfo[] tempPlanesFieldInfo = typeof(EnemyPlane).GetFields();

                                GUILayout.Space(30);
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
                                                stages[i].Planes[l].Type = EditorGUILayout.IntField(stages[i].Planes[l].Type, GUILayout.Width(100));
                                            }
                                            else if (tempPlanesFieldInfo[m].Name == "Num")
                                            {
                                                stages[i].Planes[l].Num = EditorGUILayout.IntField(stages[i].Planes[l].Num, GUILayout.Width(100));
                                            }

                                            //EditorGUILayout.TextArea(tempPlanesFieldInfo[m].GetValue(stages[i].Planes[l])?.ToString(), GUILayout.Width(100));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        EditorGUILayout.LabelField(stageFieldInfoArray[j].GetValue(stages[i])?.ToString(), GUILayout.Width(100));
                    }
                }
            }  
        }
        
    }
}


class EditorLevelWindow: EditorWindow
{
    LevelMgr lMgr;

    Vector2 ScrollViewContentOffset = Vector2.zero;//纪录ScrollView滚动的位置
    FieldInfo[] levelFieldInfoArray;
    FieldInfo[] stageFieldInfoArray;


    Dictionary<int, bool> mapToggleValues = new Dictionary<int, bool>();//单选框信息
    Dictionary<int, bool> mapTogglePlanes = new Dictionary<int, bool>();

    List<bool> toggleStageValues = new List<bool>();//单选框信息

    int selectedIndex = -1;//当前选择的某行数据下标

    // 全选
    bool isAllSelect = false;
    // 是否可编辑
    bool isEdit = false;
    // 记录所有被选中的行的索引
    List<int> allSelectRow = new List<int>();



    [MenuItem("CustomEditor/Plane/Level")]
    public static void GetWindow()
    {
        Rect rect = new Rect(0, 0, 800, 500);
        var window = GetWindow<EditorLevelWindow>("关卡编辑");
        
        //var window = EditorWindow.GetWindowWithRect(typeof(EditorLevelWindow), rect, false, "关卡编辑");
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
        Debug.LogFormat("DrawStageData");
        SirenixEditorGUI.BeginHorizontalToolbar();
        
        
        for (int i = 0; i < stageFieldInfoArray.Length; ++i)
        {
            EditorGUILayout.LabelField(stageFieldInfoArray[i].Name, GUILayout.Width(100));
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
                        if (!mapTogglePlanes.ContainsKey(id))
                        {
                            mapTogglePlanes[id] = false;
                        }


                        string str = "显示";
                        if (mapTogglePlanes[id])
                        {
                            str = "隐藏";
                        }

                        GUIStyle style = EditorStyles.miniPullDown;
                        style.alignment = TextAnchor.MiddleCenter;
                        style.normal.textColor = Color.white;
                        if (mapTogglePlanes[id] = GUILayout.Toggle(mapTogglePlanes[id], str, style,GUILayout.Width(100)))
                        //if (GUILayout.Button("+", GUILayout.Width(100)))
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
                                                stages[i].Planes[l].Type = EditorGUILayout.IntField(stages[i].Planes[l].Type, GUILayout.Width(100));
                                            }
                                            else if (tempPlanesFieldInfo[m].Name == "Num")
                                            {
                                                stages[i].Planes[l].Num = EditorGUILayout.IntField(stages[i].Planes[l].Num, GUILayout.Width(100));
                                            }

                                            //EditorGUILayout.TextArea(tempPlanesFieldInfo[m].GetValue(stages[i].Planes[l])?.ToString(), GUILayout.Width(100));
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

                    for (int i = 0; i < lMgr.Levels.Count; i++)
                    {
                        mapToggleValues[i] = isAllSelect;
                    }
                }

                Delete();

                Save();

                for (int j = 0; j < 11; j++)
                {
                    EditorUtility.DisplayProgressBar("Save", "--------", j * 1f / 10);
                }
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
            SirenixEditorGUI.EndHorizontalToolbar();


            for (int i = 0; i < lMgr.Levels.Count; i++)
            {
                SirenixEditorGUI.BeginHorizontalToolbar();

                mapToggleValues[i] = EditorGUILayout.Toggle(mapToggleValues[i], GUILayout.Width(100));
                if (mapToggleValues[i])
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
            mapToggleValues[i] = false;
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
}

