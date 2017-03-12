using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor( typeof( Outfit ) )]
public class OutfitEditor : Editor
{
	ReorderableList bodyOverrideArray;
	ReorderableList decalOverrideArray;

	static GUIContent bodyOverrideArrayLabel = new GUIContent( "Body Part Overrides" );
	static GUIContent decalOverrideArrayLabel = new GUIContent( "Decal Overrides" );

	void OnEnable()
	{
		bodyOverrideArray = new ReorderableList(
			serializedObject,
			serializedObject.FindProperty( "bodyOverrides" ),
			true, true, true, true
		);
		decalOverrideArray = new ReorderableList(
			serializedObject,
			serializedObject.FindProperty( "decalOverrides" ),
			true, true, true, true
		);

		bodyOverrideArray.elementHeightCallback = ( int index ) =>
		{
			SerializedProperty property = bodyOverrideArray.serializedProperty.GetArrayElementAtIndex( index );
			return EditorGUI.GetPropertyHeight( property, GUIContent.none, false );
		};
		decalOverrideArray.elementHeightCallback = ( int index ) =>
		{
			SerializedProperty property = decalOverrideArray.serializedProperty.GetArrayElementAtIndex( index );
			return EditorGUI.GetPropertyHeight( property, GUIContent.none, false );
		};

		bodyOverrideArray.drawElementCallback = ( Rect rect, int index, bool isActive, bool isFocused ) =>
		{
			SerializedProperty property = bodyOverrideArray.serializedProperty.GetArrayElementAtIndex( index );
			EditorGUI.PropertyField( rect, property, GUIContent.none );
		};
		decalOverrideArray.drawElementCallback = ( Rect rect, int index, bool isActive, bool isFocused ) =>
		{
			SerializedProperty property = decalOverrideArray.serializedProperty.GetArrayElementAtIndex( index );
			EditorGUI.PropertyField( rect, property, GUIContent.none );
		};

		bodyOverrideArray.drawHeaderCallback = ( Rect rect ) =>
		{
			EditorGUI.LabelField( rect, bodyOverrideArrayLabel );
		};
		decalOverrideArray.drawHeaderCallback = ( Rect rect ) =>
		{
			EditorGUI.LabelField( rect, decalOverrideArrayLabel );
		};
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		using( var scope = new EditorGUI.DisabledGroupScope( EditorApplication.isPlayingOrWillChangePlaymode ) )
		{
			EditorGUILayout.Separator();
			bodyOverrideArray.DoLayoutList();

			EditorGUILayout.Separator();
			decalOverrideArray.DoLayoutList();
		}

		serializedObject.ApplyModifiedProperties();
	}
}
