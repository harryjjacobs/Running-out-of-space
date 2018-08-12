using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GlobalFont : EditorWindow {

    [SerializeField]
    private Font font;

    private void Awake()
    {
        Text[] texts = (Text[]) Resources.FindObjectsOfTypeAll(typeof(Text));
        if (texts.Length > 0)
        {
            Font newFont = texts[0].font;
            font = newFont;
        }
    }

    [MenuItem("Window/Set Global Font")]
    public static void ChangeFont()
    {
        // Get existing open window or if none, make a new one:
        GlobalFont window = (GlobalFont) GetWindow(typeof(GlobalFont));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        font = (Font) EditorGUILayout.ObjectField("Font", font, typeof(Font), true);
        if (GUILayout.Button("Update"))
        {
            SetFonts();
        }
    }

    private void SetFonts()
    {
        Text[] texts = (Text[]) Resources.FindObjectsOfTypeAll(typeof(Text));
        foreach (Text text in texts)
        {
            text.font = font;
        }
    }

}
