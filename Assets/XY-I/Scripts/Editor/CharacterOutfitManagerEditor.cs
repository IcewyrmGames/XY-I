using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor( typeof( CharacterOutfitManager ) )]
public class CharacterOutfitManagerEditor : Editor
{
	ReorderableList outfitDataArray;

	void OnEnable()
	{
		outfitDataArray = new ReorderableList(
			serializedObject,
			serializedObject.FindProperty( "_outfits" ),
			true, true, true, true
		);

		outfitDataArray.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused ) => {
			SerializedProperty outfitData = outfitDataArray.serializedProperty.GetArrayElementAtIndex( index );

			EditorGUI.BeginChangeCheck();
			EditorGUI.PropertyField( rect, outfitData, GUIContent.none );
			if( EditorGUI.EndChangeCheck() && !EditorApplication.isPlayingOrWillChangePlaymode )
			{
				SerializedProperty slotProperty = outfitData.FindPropertyRelative( "slot" );
				SerializedProperty outfitProperty = outfitData.FindPropertyRelative( "outfit" );

				OutfitSlot slot = (OutfitSlot)slotProperty.objectReferenceValue;
				Outfit outfitObj = (Outfit)outfitProperty.objectReferenceValue;

				if( outfitObj && outfitObj.slot != slot )
				{
					outfitProperty.objectReferenceValue = null;
				}
			}
		};

		outfitDataArray.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField( rect, "Outfit Data" );
		};
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		outfitDataArray.DoLayoutList();

		serializedObject.ApplyModifiedProperties();
	}
}
