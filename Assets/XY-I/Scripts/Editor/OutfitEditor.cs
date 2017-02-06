using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor( typeof( Outfit ) )]
public class OutfitEditor : Editor
{
	static float FoldedElementHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2f;
	static float UnfoldedElementHeight = EditorGUIUtility.singleLineHeight*3f + EditorGUIUtility.standardVerticalSpacing*4f;

	static bool bodyOverrideArrayUnfolded = false;
	static bool decalOverrideArrayUnfolded = false;

	ReorderableList bodyOverrideArray;
	ReorderableList decalOverrideArray;

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

		bodyOverrideArray.elementHeightCallback = (int index) =>
		{
			return bodyOverrideArrayUnfolded ? UnfoldedElementHeight : FoldedElementHeight;
		};
		decalOverrideArray.elementHeightCallback = (int index) =>
		{
			return decalOverrideArrayUnfolded ? UnfoldedElementHeight : FoldedElementHeight;
		};

		bodyOverrideArray.drawElementCallback = ( Rect rect, int index, bool isActive, bool isFocused ) =>
		{
			SerializedProperty property = bodyOverrideArray.serializedProperty.GetArrayElementAtIndex( index );
			if( !bodyOverrideArrayUnfolded )
			{
				rect.y += 2f; rect.height = EditorGUIUtility.singleLineHeight;
				EditorGUI.PropertyField( rect, property.FindPropertyRelative( "slot" ), GUIContent.none );
			}
			else
			{
				EditorGUI.PropertyField( rect, property, GUIContent.none );
			}
		};
		decalOverrideArray.drawElementCallback = ( Rect rect, int index, bool isActive, bool isFocused ) =>
		{
			SerializedProperty property = decalOverrideArray.serializedProperty.GetArrayElementAtIndex( index );
			if( !decalOverrideArrayUnfolded )
			{
				rect.y += 2f; rect.height = EditorGUIUtility.singleLineHeight;
				EditorGUI.PropertyField( rect, property.FindPropertyRelative( "slot" ), GUIContent.none );
			}
			else
			{
				EditorGUI.PropertyField( rect, property, GUIContent.none );
			}
		};

		bodyOverrideArray.drawHeaderCallback = (Rect rect) =>
		{
			rect.x += 10f;
			bodyOverrideArrayUnfolded = EditorGUI.Foldout( rect, bodyOverrideArrayUnfolded, "Body Part Overrides", true );
		};
		decalOverrideArray.drawHeaderCallback = (Rect rect) =>
		{
			rect.x += 10f;
			decalOverrideArrayUnfolded = EditorGUI.Foldout( rect, decalOverrideArrayUnfolded, "Decal Overrides", true );
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
