using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System;

public class ReferenceTypeCreatorWindow : EditorWindow
{
	readonly string templateFolderPath = "Assets/Reference Variables/Editor/Templates/";
	readonly string referenceVariableFolderPath = "Assets/Reference Variables/";
	static string typeInput;
	static string variableNameInput;
	static string referenceNameInput;
	static string folderName;


	[MenuItem("Variable References/Create New", false, 0)]
	static void Init()
	{
		ReferenceTypeCreatorWindow window = CreateInstance<ReferenceTypeCreatorWindow>();
		window.position = new Rect(Screen.width / 2, Screen.height / 2, 500, 250);
		window.ShowUtility();
	}

	private void OnGUI()
	{
		GUILayout.Space(10);
		EditorGUILayout.LabelField("WARNING: Types that do not have += operator compatability will be broken upon creation, but can be manually edited.", EditorStyles.wordWrappedLabel);
		GUILayout.Space(30);


		typeInput = EditorGUILayout.TextField("Variable Type", typeInput);
		GUILayout.Space(10);
		variableNameInput = EditorGUILayout.TextField("Variable Script Name", variableNameInput);
		GUILayout.Space(10);
		referenceNameInput = EditorGUILayout.TextField("Reference Script Name", referenceNameInput);
		GUILayout.Space(20);

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Create From Templates"))
		{
			folderName = UppercaseFirst(typeInput) + "/";
			CreateVariableScript();
			CreateReferenceScript();
			CreateReferenceDrawer();
			Close();
		}

		if (GUILayout.Button("Cancel")) Close();
		EditorGUILayout.EndHorizontal();
	}

	private void CreateVariableScript()
	{
		string scriptText = File.ReadAllText(templateFolderPath + "VariableTemplate.txt");
		string partialPath = referenceVariableFolderPath + folderName + variableNameInput + ".cs";

		scriptText = scriptText.Replace("#VARIABLE_NAME#", variableNameInput);
		scriptText = scriptText.Replace("#REFERENCE_NAME#", referenceNameInput);
		scriptText = scriptText.Replace("#TYPE#", typeInput);
		scriptText = scriptText.Replace("#TYPE_UPPER#", UppercaseFirst(typeInput));

		WriteToNewFile(scriptText, Path.GetFullPath(partialPath));

		AssetDatabase.ImportAsset(partialPath);
		ProjectWindowUtil.ShowCreatedAsset(AssetDatabase.LoadAssetAtPath(partialPath, typeof(UnityEngine.Object)));
	}

	private void CreateReferenceScript()
	{
		string scriptText = File.ReadAllText(templateFolderPath + "ReferenceTemplate.txt");
		string partialPath = referenceVariableFolderPath + folderName + referenceNameInput + ".cs";

		scriptText = scriptText.Replace("#VARIABLE_NAME#", variableNameInput);
		scriptText = scriptText.Replace("#REFERENCE_NAME#", referenceNameInput);
		scriptText = scriptText.Replace("#TYPE#", typeInput);

		WriteToNewFile(scriptText, Path.GetFullPath(partialPath));

		AssetDatabase.ImportAsset(partialPath);
		//ProjectWindowUtil.ShowCreatedAsset(AssetDatabase.LoadAssetAtPath(partialPath, typeof(UnityEngine.Object)));
	}

	private void CreateReferenceDrawer()
	{
		string scriptText = File.ReadAllText(templateFolderPath + "EditorTemplate.txt");
		string partialPath = referenceVariableFolderPath + folderName + "Editor/" + referenceNameInput + "Drawer.cs";

		scriptText = scriptText.Replace("#VARIABLE_NAME#", variableNameInput);
		scriptText = scriptText.Replace("#REFERENCE_NAME#", referenceNameInput);
		scriptText = scriptText.Replace("#TYPE#", typeInput);

		WriteToNewFile(scriptText, Path.GetFullPath(partialPath));

		AssetDatabase.ImportAsset(partialPath);
		//ProjectWindowUtil.ShowCreatedAsset(AssetDatabase.LoadAssetAtPath(partialPath, typeof(UnityEngine.Object)));
	}

	private void WriteToNewFile(string textToWrite, string fullPath)
	{
		try
		{
			//creates the directory if needed
			Directory.CreateDirectory(Path.GetDirectoryName(fullPath));


			using(FileStream stream = new FileStream(fullPath, FileMode.Create))
			{
				using(StreamWriter writer = new StreamWriter(stream))
				{
					writer.Write(textToWrite);
				}
				stream.Close();
			}
		}
		catch (Exception e)
		{
			Debug.LogError(e);
		}
	}

	//Utilities
	string UppercaseFirst(string s)
	{
		if (string.IsNullOrEmpty(s))
		{
			return string.Empty;
		}
		return char.ToUpper(s[0]) + s.Substring(1);
	}
}
