﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer( typeof( CharacterOutfitManager.OutfitData ) )]
public class OutfitDataDrawer : PropertyDrawer
{
	const float SLOT_WIDTH = 100f;
	const float SPACING = 4f;

	public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
	{
		position = EditorGUI.IndentedRect( position );
		int previousIndent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		position.y += 2f;
		position.height = EditorGUIUtility.singleLineHeight;

		Rect slotPosition = new Rect(
			position.x,
			position.y,
			SLOT_WIDTH,
			position.height
		);
		Rect outfPosition = new Rect(
			slotPosition.x + slotPosition.width + SPACING,
			position.y,
			position.width - SLOT_WIDTH - SPACING,
			position.height
		);

		EditorGUI.PropertyField( slotPosition, property.FindPropertyRelative( "slot" ), GUIContent.none );
		EditorGUI.PropertyField( outfPosition, property.FindPropertyRelative( "outfit" ), GUIContent.none );

		EditorGUI.indentLevel = previousIndent;
	}
}
