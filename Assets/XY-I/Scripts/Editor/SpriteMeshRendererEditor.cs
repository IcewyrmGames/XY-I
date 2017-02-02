using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Anima2D;

[CustomEditor( typeof( SpriteMeshRenderer ) )]
public class SpriteMeshRendererEditor : Editor
{
	SerializedProperty sortingOrder;
	SerializedProperty sortingLayerID;
	SerializedProperty spriteMesh;
	SerializedProperty color;

	ReorderableList boneList;

	SpriteMeshData _spriteMeshData;

	void OnEnable()
	{
		sortingOrder = serializedObject.FindProperty( "_sortingOrder" );
		sortingLayerID = serializedObject.FindProperty( "_sortingLayerID" );
		spriteMesh = serializedObject.FindProperty( "_spriteMesh" );
		color = serializedObject.FindProperty( "_color" );

		boneList = new ReorderableList(
			serializedObject,
			serializedObject.FindProperty( "_bones" ),
			false, true, false, false
		);

		boneList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused ) => {
			SerializedProperty boneTransform = boneList.serializedProperty.GetArrayElementAtIndex( index );
			rect.y += 2f;
			rect.height = EditorGUIUtility.singleLineHeight;

			if( _spriteMeshData && index < _spriteMeshData.bindPoses.GetLength(0) )
			{
				EditorGUI.PropertyField( rect, boneTransform, new GUIContent( _spriteMeshData.bindPoses[index].name ) );
			}
			else
			{
				EditorGUI.PropertyField( rect, boneTransform, new GUIContent( string.Format( "bone {0}", index ) ) );
			}
		};

		boneList.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField( rect, "Bones" );
		};

		if( spriteMesh.objectReferenceValue != null )
		{
			UpdateSpriteMeshData();
			UpdateBoneCount();
			serializedObject.ApplyModifiedProperties();
		}
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUI.BeginChangeCheck();

		EditorGUILayout.PropertyField( spriteMesh );

		if( EditorGUI.EndChangeCheck() )
		{
			if( spriteMesh.objectReferenceValue != null )
			{
				UpdateSpriteMeshData();
				UpdateBoneCount();
			}
		}

		EditorGUILayout.PropertyField( color );

		boneList.DoLayoutList();

		EditorGUIExtra.SortingLayerField(
			new GUIContent("Sorting Layer"),
			sortingLayerID,
			EditorStyles.popup,
			EditorStyles.label
		);
		EditorGUILayout.PropertyField( sortingOrder, new GUIContent( "Order in Layer" ) );

		serializedObject.ApplyModifiedProperties();
	}

	void UpdateSpriteMeshData()
	{
		_spriteMeshData = SpriteMeshUtils.LoadSpriteMeshData( spriteMesh.objectReferenceValue as SpriteMesh );
	}

	void UpdateBoneCount()
	{
		boneList.serializedProperty.arraySize = _spriteMeshData.bindPoses.GetLength(0);
	}
}
