using ColaFramework.Foundation;
using LitJson;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ColaFramework.ToolKit
{
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

        //[LabelText("触发的敌人")]
        //public List<EnemyPlane> Planes;

        [LabelText("敌人机群id")]
        public List<int> WaveIDs;
    }

    class EnemyPlane
    {
        [LabelText("敌机表里面的id")]
        public int ID;

        public WaveType Type;

        public int Num;
    }


    class EditorLevelWindow : BaseTableEditorWindow
    {
        EditorLevelWindow()
        {
            winName = "关卡编辑";
            fileName = "level.json";
        }

        LevelMgr lMgr;


        // 阵型列表
        WaveFMgr wMgr;
        List<string> listWaves = new List<string>();


        FieldInfo[] levelFieldInfoArray;
        FieldInfo[] stageFieldInfoArray;


        Dictionary<int, bool> mapTogglePlanes = new Dictionary<int, bool>();
        Dictionary<int, bool> mapToggleWaves = new Dictionary<int, bool>();

        List<bool> toggleStageValues = new List<bool>();//单选框信息

        // id的最大值
        int maxID = 0;
        List<int> idList = new List<int>();


        [MenuItem("CustomEditor/Plane/Level")]
        public static void GetWindow()
        {
            var window = GetWindow<EditorLevelWindow>(winName);
            window.Show();
        }

        protected override void OnLoad()
        {
            //SetFileName("level.json");
            // 加载wave.json
            wMgr = LoadJsonData<WaveFMgr>("wave.json");
            listWaves.Clear();
            for (int i = 0; i < wMgr.Waves.Count; i++)
            {
                string str = wMgr.Waves[i].ID + "|" + wMgr.Waves[i].Name;
                listWaves.Add(str);
            }


            levelFieldInfoArray = typeof(Level).GetFields();
            stageFieldInfoArray = typeof(Stage).GetFields();

            string fName = AppConst.jsonFilePath + "/" + fileName;
            string jsonStr;

            bool isExistFile = FileHelper.IsFileExist(fName);
            if (!isExistFile)
            {
                lMgr = ScriptableObject.CreateInstance<LevelMgr>();
                lMgr.Levels = new List<Level>();

                jsonStr = JsonMapper.ToJson(lMgr);

                //Debug.LogFormat("jsonstr111[{0}]", jsonStr);

                FileHelper.WriteString(fName, jsonStr);
            }

            string strContent = FileHelper.ReadString(fName);

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
        }

        private void OnGUI()
        {
            DrawLevelData();
        }

        private void DrawStageData(int id, ref List<Stage> stages)
        {
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
                stage.WaveIDs = new List<int>();

                stages.Add(stage);

                mapToggleWaves[stages.Count - 1] = false;
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
                        /*  if (!mapTogglePlanes.ContainsKey(i))
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
                                                GUI.Box(vscope1.rect, new GUIContent());

                                                if (tempPlanesFieldInfo[m].Name == "ID")
                                                {
                                                    string key = "stage|" + i.ToString() + "|" + l.ToString();

                                                    if (GUILayout.Button("选择", GUILayout.Width(50)))
                                                    {
                                                        // 弹出一个editorwindow
                                                        EditorTextWindow window = GetWindow<EditorTextWindow>("文本列表");
                                                        
                                                        window.SetTextList(listEnemys);
                                                        window.Show();
                                                        window.ParentWin = this;
                                                        keyValuePairs[key] = string.Empty;
                                                        window.key = key;

                                                        if (!editorWindows.Contains(window))
                                                        {
                                                            editorWindows.Add(window);
                                                        }
                                                    }

                                                    if (keyValuePairs.ContainsKey(key))
                                                    {
                                                        if(!string.IsNullOrEmpty(keyValuePairs[key]))
                                                        {
                                                            string[] sep = new string[] { "|" };
                                                            string[] result = keyValuePairs[key].Split(sep, StringSplitOptions.None);
                                                            int idt;
                                                            bool ret = int.TryParse(result[0], out idt);
                                                            if (ret)
                                                            {
                                                                if (stages[i].Planes[l].ID != idt)
                                                                {
                                                                    stages[i].Planes[l].ID = idt;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    stages[i].Planes[l].ID = EditorGUILayout.IntField(stages[i].Planes[l].ID, GUILayout.Width(50));
                                                }
                                                else if (tempPlanesFieldInfo[m].Name == "Type")
                                                {
                                                    stages[i].Planes[l].Type = (WaveType)EditorGUILayout.EnumPopup(stages[i].Planes[l].Type, GUILayout.Width(100));
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
                            }*/
                        }
                        else
                        {
                            if (stageFieldInfoArray[j].Name == "Time")
                            {
                                stages[i].Time = EditorGUILayout.IntField(stages[i].Time, GUILayout.Width(100));
                            }
                            else if (stageFieldInfoArray[j].Name == "WaveIDs")
                            {
                                if (!mapToggleWaves.ContainsKey(i))
                                {
                                    mapToggleWaves[i] = false;
                                }

                                string str = "显示";
                                if (mapToggleWaves[i])
                                {
                                    str = "隐藏";
                                }

                                GUIStyle style = EditorStyles.miniPullDown;
                                style.alignment = TextAnchor.MiddleCenter;
                                style.normal.textColor = Color.white;
                                if (mapToggleWaves[i] = GUILayout.Toggle(mapToggleWaves[i], str, style, GUILayout.Width(100)))
                                {
                                    using (var vscope = new EditorGUILayout.VerticalScope())
                                    {

                                        GUI.Box(vscope.rect, new GUIContent());
                                        GUILayout.Space(26);
                                        using (var planeScope = new EditorGUILayout.HorizontalScope())
                                        {
                                            EditorGUILayout.LabelField("阵型ID", GUILayout.Width(100));

                                            // 增加添加按钮
                                            if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
                                            {
                                                stages[i].WaveIDs.Add(0);
                                                return;
                                            }
                                        }

                                        // 所有的飞机数据
                                        for (int l = 0; l < stages[i].WaveIDs.Count; l++)
                                        {
                                            using (var vscope1 = new EditorGUILayout.HorizontalScope())
                                            {
                                                GUI.Box(vscope1.rect, new GUIContent());

                                                string key = "wave|" + i.ToString() + "|" + l.ToString();

                                                if (GUILayout.Button("选择", GUILayout.Width(50)))
                                                {
                                                    // 弹出一个editorwindow
                                                    EditorTextWindow window = GetWindow<EditorTextWindow>("文本列表");

                                                    window.SetTextList(listWaves);
                                                    window.Show();
                                                    window.ParentWin = this;
                                                    keyValuePairs[key] = string.Empty;
                                                    window.key = key;

                                                    if (!editorWindows.Contains(window))
                                                    {
                                                        editorWindows.Add(window);
                                                    }
                                                }

                                                if (keyValuePairs.ContainsKey(key))
                                                {
                                                    if (!string.IsNullOrEmpty(keyValuePairs[key]))
                                                    {
                                                        string[] sep = new string[] { "|" };
                                                        string[] result = keyValuePairs[key].Split(sep, StringSplitOptions.None);
                                                        int idt;
                                                        bool ret = int.TryParse(result[0], out idt);
                                                        if (ret)
                                                        {
                                                            if (stages[i].WaveIDs[l] != idt)
                                                            {
                                                                stages[i].WaveIDs[l] = idt;
                                                            }
                                                        }
                                                    }
                                                }

                                                stages[i].WaveIDs[l] = EditorGUILayout.IntField(stages[i].WaveIDs[l], GUILayout.Width(50));

                                                // 增加删除按钮
                                                if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
                                                {
                                                    stages[i].WaveIDs.RemoveAt(l);
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        } 
                    }

                    // 增加阶段删除按钮
                    if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
                    {
                        int nCount = stages.Count;
                        stages.RemoveAt(i);
                        mapToggleWaves.Remove(i);

                        // 删除之前 0 1 2 3 4
                        for (int l = i + 1; l < nCount; l++)
                        {
                            bool bOld = mapToggleWaves[l];
                            mapToggleWaves[l - 1] = bOld;
                            mapToggleWaves.Remove(l);
                        }

                        return;
                    }
                }
            }
        }

        private void DrawLevelData()
        {
            DrawHead();

            //EditorGUILayout.BeginToggleGroup();

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
                        {
                            using (var vscope = new EditorGUILayout.VerticalScope())
                            {
                                GUI.Box(vscope.rect, new GUIContent());
                                GUILayout.Space(26);
                                DrawStageData(lMgr.Levels[i].ID, ref lMgr.Levels[i].Stage);
                            }
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

        private void OnDisable()
        {
            SaveData();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < editorWindows.Count; i++)
            {
                if (editorWindows[i] != null)
                {
                    editorWindows[i].Close();
                }
            }

            editorWindows.Clear();
        }

        protected override void OnDelete()
        {
            for (int i = 0; i < allSelectRow.Count; i++)
            {
                idList.Remove(lMgr.Levels[allSelectRow[i]].ID);
                mapToggleRows.Remove(lMgr.Levels[allSelectRow[i]].ID);
                lMgr.Levels.RemoveAt(allSelectRow[i]);
            }
            allSelectRow.Clear();
        }

        private void SaveData()
        {
            string fName = AppConst.jsonFilePath + "/" + fileName;
            string jsonStr;

            jsonStr = ToJson<LevelMgr>(lMgr);

            //jsonStr = JsonMapper.ToJson(lMgr);

            FileHelper.WriteString(fName, jsonStr);
        }

        protected override void OnSave()
        {
            SaveData();
        }

        protected override void OnReload()
        {
            // 先清理
            mapToggleWaves.Clear();
            toggleStageValues.Clear();

            OnLoad();
        }

        protected override void OnSelectAll()
        {
            for (int i = 0; i < idList.Count; i++)
            {
                mapToggleRows[idList[i]] = isAllSelect;
            }
        }
    }

}