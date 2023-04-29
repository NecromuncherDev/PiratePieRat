#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PPR.Util
{
    public class MakeDataTool : EditorWindow
    {
        private string scoreTag = "MyScore";
        private int firstCost = 50;
        private AnimationCurve valueGraph = new();
        private float valueScale = 2.5f;
        private int firstPower = 2;
        private float powerScale = 2.3f;
        private AnimationCurve powerGraph = new();
        private int topLevel = 20;

        private int currentValue;
        private int currentPower;
        private string jsonData;
        private Vector2 scrollPos;

        [MenuItem("Tools/Make Upgrade Data")]
        static void Init()
        {
            MakeDataTool window = (MakeDataTool)EditorWindow.GetWindow(typeof(MakeDataTool));
            window.minSize = new Vector2(600, 800);
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("Make Data", EditorStyles.boldLabel);

            GUILayout.Label("", GUILayout.Height(20));
            GUILayout.Label("Upgrade cost scaling");
            firstCost = EditorGUILayout.IntField("1st upgrade cost", firstCost);
            valueGraph = EditorGUILayout.CurveField("Cost graph", valueGraph);
            scoreTag = EditorGUILayout.TextField("Score tag", scoreTag);

            GUILayout.Label("", GUILayout.Height(20));
            GUILayout.Label("Upgrade power scaling");
            firstPower = EditorGUILayout.IntField("1st upgrade power", firstPower);
            powerGraph = EditorGUILayout.CurveField("Power graph", powerGraph);

            GUILayout.Label("", GUILayout.Height(20));
            GUILayout.Label("Upgrade levels");
            topLevel = EditorGUILayout.IntField("Levels", topLevel);

            currentValue = firstCost;
            currentPower = firstPower;

            if (GUILayout.Button("Generate"))
            {
                jsonData = $"{{\"Level\": 1,\"CurrencyCost\": 0,\"CurrencyTag\": \"{scoreTag}\", \"Power\": 1}}";
                for (int i = 0; i < topLevel; i++)
                {
                    jsonData = $"{jsonData},\n{{\"Level\": {i + 2},\"CurrencyCost\": {currentValue},\"CurrencyTag\": \"{scoreTag}\", \"Power\": {currentPower}}}";
                    currentValue = (int)(currentValue * (1 + valueGraph.Evaluate((1f / topLevel) * i)));
                    currentPower = (int)(currentPower * (1 + powerGraph.Evaluate((1f / topLevel) * i)));
                }
            }

            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(600), GUILayout.Height(300));
            GUILayout.Label(jsonData);
            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            if (GUILayout.Button("Copy to Clipboard"))
            {
                jsonData.CopyToClipboard();
            }
        }
    }

    public static class EditorExtentions
    {
        public static void CopyToClipboard(this string s)
        {
            TextEditor te = new TextEditor();
            te.text = s;
            te.SelectAll();
            te.Copy();
        }
    }
}
#endif