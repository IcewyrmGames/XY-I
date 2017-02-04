using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor( typeof( CharacterSlotManager ) )]
public class CharacterSlotManagerEditor : Editor
{
	static float FoldedElementHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2f;
	static float UnfoldedElementHeight = EditorGUIUtility.singleLineHeight*4f + EditorGUIUtility.standardVerticalSpacing*5f;

	static bool partsArrayUnfolded = false;
	static bool decalsArrayUnfolded = false;

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

		partsArray.elementHeightCallback = (int index) =>
		{
			return partsArrayUnfolded ? UnfoldedElementHeight : FoldedElementHeight;
		};
		decalsArray.elementHeightCallback = (int index) =>
		{
			return decalsArrayUnfolded ? UnfoldedElementHeight : FoldedElementHeight;
		};

		partsArray.drawElementCallback = ( Rect rect, int index, bool isActive, bool isFocused ) =>
		{
			SerializedProperty property = partsArray.serializedProperty.GetArrayElementAtIndex( index );
			if( !partsArrayUnfolded )
			{
				rect.y += 2f; rect.height = EditorGUIUtility.singleLineHeight;
				EditorGUI.PropertyField( rect, property.FindPropertyRelative( "slot" ), GUIContent.none );
			}
			else
			{
				EditorGUI.PropertyField( rect, property, GUIContent.none );
			}
		};
		decalsArray.drawElementCallback = ( Rect rect, int index, bool isActive, bool isFocused ) =>
		{
			SerializedProperty property = decalsArray.serializedProperty.GetArrayElementAtIndex( index );
			if( !partsArrayUnfolded )
			{
				rect.y += 2f; rect.height = EditorGUIUtility.singleLineHeight;
				EditorGUI.PropertyField( rect, property.FindPropertyRelative( "slot" ), GUIContent.none );
			}
			else
			{
				EditorGUI.PropertyField( rect, property, GUIContent.none );
			}
		};

		partsArray.drawHeaderCallback = (Rect rect) =>
		{
			rect.x += 10f;
			partsArrayUnfolded = EditorGUI.Foldout( rect, partsArrayUnfolded, "Body Parts", true );
		};
		decalsArray.drawHeaderCallback = (Rect rect) =>
		{
			rect.x += 10f;
			decalsArrayUnfolded = EditorGUI.Foldout( rect, decalsArrayUnfolded, "Decals", true );
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
