using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ReferenceTypeCreatorWindow : EditorWindow
{
    static string typeInput;
    static string variableNameInput;
    static string referenceNameInput;


    [MenuItem("Variable References/Create New", false, 0)]
    static void Init()
    {
        ReferenceTypeCreatorWindow window = CreateInstance<ReferenceTypeCreatorWindow>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 500, 300);
        window.ShowUtility();
    }

	private void OnGUI()
	{
        GUILayout.Space(10);
        EditorGUILayout.LabelField("WARNING: NOT YET FULLY IMPLEMENTED", EditorStyles.largeLabel);
        GUILayout.Space(60);


		typeInput = EditorGUILayout.TextField("Variable Type", typeInput);
        GUILayout.Space(10);
		variableNameInput = EditorGUILayout.TextField("Variable Script Name", variableNameInput);
        GUILayout.Space(10);
		referenceNameInput = EditorGUILayout.TextField("Reference Script Name", referenceNameInput);
        GUILayout.Space(20);

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Create From Templates"))
        {
            //do the template creation
            Close();
        }

        if (GUILayout.Button("Cancel")) Close();
        EditorGUILayout.EndHorizontal();
	}
}
