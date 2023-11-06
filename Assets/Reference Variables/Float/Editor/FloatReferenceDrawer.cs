using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatReference))]
public class FloatReferenceDrawer : PropertyDrawer
{
    /// <summary>
    /// Options to display in the popup to select whether to use constant or scriptable object
    /// </summary>
    private readonly string[] popupOptions =
        { "Use Constant", "Use Scriptable Object" };

    /// <summary>
    /// Cached style to tell the editor how to draw the popup button
    /// </summary>
    private GUIStyle popupStyle;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		if(popupStyle == null)
        {
            popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
            popupStyle.imagePosition = ImagePosition.ImageOnly;
        }

        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        EditorGUI.BeginChangeCheck();

        //Get properties
        SerializedProperty useConstant = property.FindPropertyRelative("UseConstant");
        SerializedProperty constantValue = property.FindPropertyRelative("ConstantValue");
        SerializedProperty variable = property.FindPropertyRelative("Variable");

        //Calculate rect for config button
        Rect buttonRect = new Rect(position);
        buttonRect.yMin += popupStyle.margin.top;
        buttonRect.xMin = position.xMax - popupStyle.fixedWidth;
        buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
        buttonRect.height = position.height;
        position.xMax = buttonRect.xMin;

        //Store old indent level and set it to 0 the PrefixLabel takes care of it
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;


        int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);
        useConstant.boolValue = result == 0;

        EditorGUI.PropertyField(position, 
            useConstant.boolValue ? constantValue : variable, 
            GUIContent.none);

        if (EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
	}
}
