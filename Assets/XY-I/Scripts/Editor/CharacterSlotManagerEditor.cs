using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor( typeof( CharacterSlotManager ) )]
public class CharacterSlotManagerEditor : Editor
{
	ReorderableList partsArray;
	ReorderableList decalsArray;

	void OnEnable()
	{
		partsArray = new ReorderableList(
			serializedObject,
			serializedObject.FindProperty( "_parts" ),
			true, true, true, true
		);
		decalsArray = new ReorderableList(
			serializedObject,
			serializedObject.FindProperty( "_decals" ),
			true, true, true, true
		);

		partsArray.elementHeight = EditorGUIUtility.singleLineHeight*4f + EditorGUIUtility.standardVerticalSpacing*5f;
		decalsArray.elementHeight = EditorGUIUtility.singleLineHeight*4f + EditorGUIUtility.standardVerticalSpacing*5f;

		partsArray.drawElementCallback = ( Rect rect, int index, bool isActive, bool isFocused ) =>
		{
			SerializedProperty property = partsArray.serializedProperty.GetArrayElementAtIndex( index );
			EditorGUI.PropertyField( rect, property, GUIContent.none );
		};
		decalsArray.drawElementCallback = ( Rect rect, int index, bool isActive, bool isFocused ) =>
		{
			SerializedProperty property = decalsArray.serializedProperty.GetArrayElementAtIndex( index );
			EditorGUI.PropertyField( rect, property, GUIContent.none );
		};

		partsArray.drawHeaderCallback = (Rect rect) =>
		{
			EditorGUI.LabelField( rect, "Body Parts" );
		};
		decalsArray.drawHeaderCallback = (Rect rect) =>
		{
			EditorGUI.LabelField( rect, "Decals" );
		};
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		using( var scope = new EditorGUI.DisabledGroupScope( EditorApplication.isPlayingOrWillChangePlaymode ) )
		{
			EditorGUILayout.Separator();
			partsArray.DoLayoutList();

			EditorGUILayout.Separator();
			decalsArray.DoLayoutList();
		}

		serializedObject.ApplyModifiedProperties();
	}
}
