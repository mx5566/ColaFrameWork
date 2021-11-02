using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;

// https://blog.csdn.net/wangjiangrong/article/details/100743781
// https://www.sohu.com/a/378884846_485902
namespace ColaFramework.ToolKit
{
    public class Test1 : JsonDataWriter
    {
        [LabelText("测试变量")]
        public bool TestVar;

        public void Export()
        {
            WriteBoolean("TestVar", TestVar);

            Debug.LogFormat("Hello {0}", GetDataDump());
        }
    }

    public class Test1Editor
    {
        [MenuItem("CustomEditor/TestOdinJson")]
        public static void TestOdinJson()
        {
                        

        }
    }

    public class Test2 : MonoBehaviour
    {
        [LabelText("测试变量")]
        public bool TestVar;
        

    }

    [CustomEditor(typeof(Test2))]
    public class Test2Editor : Editor
    {
        private Test2 t2;
        private void OnEnable()
        {
            t2 = target as Test2;
        }

        int circleSize = 5;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("ClickTest2"))
            {
                t2.TestVar = !t2.TestVar;
            }
        }

        private void OnSceneGUI()
        {

            
            Handles.color = Color.green;

            //Handles.CircleCap(0, t2.transform.position + new Vector3(10, 0, 0), t2.transform.rotation, circleSize); //画圈的方法

            Handles.color = Color.red;
            //Handles.CircleCap(0, t2.transform.position + new Vector3(0, 10, 0), t2.transform.rotation, circleSize);
            Handles.CircleHandleCap(0, transform.position + new Vector3(0f, 0f, 3f),
                                transform.rotation * Quaternion.LookRotation(Vector3.forward),
                                size,
                                EventType.Repaint
                );

        }
    }



    [CreateAssetMenu(fileName = "BuildRules.asset", menuName = "ColaFramework/BuildRules", order = 3)]
    public class BuildRules : SerializedScriptableObject
    {
        [LabelText("按照文件夹标记")]
        [SerializeField]
        internal List<string> MarkWithDirList;

        [LabelText("按照文件标记")]
        [SerializeField]
        internal List<string> MarkWithFileList;

        [LabelText("标记为一个Bundle")]
        [SerializeField]
        internal List<string> MarkWithOneBundleList;

        [LabelText("手动处理,程序不会自动处理的列表")]
        [SerializeField]
        internal List<string> ManualList;

        public bool IsInBuildRules(string path)
        {
            if (null != ManualList)
            {
                foreach (var item in ManualList)
                {
                    if (path.StartsWith(item))
                    {
                        return true;
                    }
                }
            }

            if (null != MarkWithDirList)
            {
                foreach (var item in MarkWithDirList)
                {
                    if (path.StartsWith(item))
                    {
                        return true;
                    }
                }
            }

            if (null != MarkWithFileList)
            {
                foreach (var item in MarkWithFileList)
                {
                    if (path.StartsWith(item))
                    {
                        return true;
                    }
                }
            }

            if (null != MarkWithOneBundleList)
            {
                foreach (var item in MarkWithOneBundleList)
                {
                    if (path.StartsWith(item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
