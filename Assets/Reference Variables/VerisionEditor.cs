using UnityEngine;
using UnityEditor;
using System.IO;

//example of doing an editor window with input
public class VersionEditor : EditorWindow
{
	const string FILENAME = "Tester.cs";
	static string versionFilePath;
	static string version;

	//[MenuItem("MyGame/Edit " + FILENAME, false, 100)]
	static void Init()
	{
		versionFilePath = Application.dataPath + "/" + FILENAME;
		version = File.ReadAllText(versionFilePath);

		VersionEditor window = CreateInstance<VersionEditor>();
		window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
		window.ShowUtility();
	}


	void OnGUI()
	{
		GUILayout.Space(10);
		EditorGUILayout.LabelField("Change the version and click Update.", EditorStyles.wordWrappedLabel);
		GUILayout.Space(10);
		version = EditorGUILayout.TextField("Version: ", version);
		GUILayout.Space(60);
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Update"))
		{
			File.WriteAllText(versionFilePath, version);
			AssetDatabase.ImportAsset("Assets/" + FILENAME, ImportAssetOptions.ForceUpdate);
			Close();
		}
		if (GUILayout.Button("Cancel")) Close();
		EditorGUILayout.EndHorizontal();
	}
}