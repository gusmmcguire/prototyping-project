using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vector2 Variable", menuName = "Variables/Vector2")]
public class Vector2Variable : ScriptableObject
{
	public Vector2 Value;

	public void SetValue(Vector2 value)
	{
		Value = value;
	}

	public void SetValue(Vector2Variable value)
	{
		Value = value.Value;
	}

	public void ApplyChange(Vector2 amount)
	{
		Value += amount;
	}

	public void ApplyChange(Vector2Variable amount)
	{
		Value += amount.Value;
	}

#if UNITY_EDITOR
	[Multiline] 
	public string DeveloperDescription = "";
#endif
}