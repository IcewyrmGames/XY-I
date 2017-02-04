using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer( typeof( BodySlotData ) )]
[CustomPropertyDrawer( typeof( DecalSlotData ) )]
public class SlotDataDrawer : PropertyDrawer
{
	const float SLOT_WIDTH = 100f;
	const float COLO_WIDTH = 50f;
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
		Rect spriPosition = new Rect(
			slotPosition.x + slotPosition.width + SPACING,
			position.y,
			position.width - SLOT_WIDTH - COLO_WIDTH - SPACING * 2f,
			position.height
		);
		Rect coloPosition = new Rect(
			spriPosition.x + spriPosition.width + SPACING,
			position.y,
			COLO_WIDTH,
			position.height
		);

		EditorGUI.PropertyField( slotPosition, property.FindPropertyRelative( "slot" ), GUIContent.none );
		EditorGUI.PropertyField( spriPosition, property.FindPropertyRelative( "sprite" ), GUIContent.none );
		EditorGUI.PropertyField( coloPosition, property.FindPropertyRelative( "color" ), GUIContent.none );

		EditorGUI.indentLevel = previousIndent;
	}
}
