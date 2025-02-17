using UnityEngine;
using UnityEditor;

public static class ReadmeEditorStyles
{
    public static GUIStyle TitleStyle
    {
        get
        {
            var style = new GUIStyle(EditorStyles.largeLabel)
            {
                fontSize = 26,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                wordWrap = true
            };
            return style;
        }
    }

    public static GUIStyle HeadingStyle
    {
        get
        {
            var style = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                wordWrap = true
            };
            return style;
        }
    }

    public static GUIStyle BodyStyle
    {
        get
        {
            var style = new GUIStyle(EditorStyles.label)
            {
                fontSize = 14,
                wordWrap = true,
                richText = true
            };
            return style;
        }
    }

    public static bool LinkLabel(GUIContent label)
    {
        var position = GUILayoutUtility.GetRect(label, EditorStyles.linkLabel);
        Handles.BeginGUI();
        Handles.color = EditorStyles.linkLabel.normal.textColor;
        Handles.DrawLine(new Vector3(position.xMin, position.yMax), new Vector3(position.xMax, position.yMax));
        Handles.color = Color.white;
        Handles.EndGUI();

        EditorGUIUtility.AddCursorRect(position, MouseCursor.Link);

        return GUI.Button(position, label, EditorStyles.linkLabel);
    }
}