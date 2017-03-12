using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer( typeof( OutfitData ) )]
public class OutfitDataDrawer : PropertyDrawer
{
	const float SLOT_WIDTH = 100f;
	const float SPACING = 4f;

	public override float GetPropertyHeight( SerializedProperty property, GUIContent label )
	{
		return ( EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing ) * 3f;
	}

	public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
	{
		SerializedProperty slot = property.FindPropertyRelative( "slot" );
		SerializedProperty outfit = property.FindPropertyRelative( "outfit" );
		SerializedProperty colors = property.FindPropertyRelative( "colors" );

		position = EditorGUI.IndentedRect( position );
		int previousIndent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		EditorGUIUtility.labelWidth *= 2f/3f;

		position.y += 2f;
		position.height = EditorGUIUtility.singleLineHeight;

		Rect pos0 = new Rect(
			position.x,
			position.y,
			position.width,
			position.height
		);
		Rect pos1 = new Rect(
			position.x,
			pos0.y + pos0.height + EditorGUIUtility.standardVerticalSpacing,
			position.width,
			position.height
		);
		Rect pos2 = new Rect(
			position.x,
			pos1.y + pos1.height + EditorGUIUtility.standardVerticalSpacing,
			position.width,
			position.height
		);

		EditorGUI.PropertyField( pos0, slot, GUIContent.none );
		EditorGUI.PropertyField( pos1, outfit );
		EditorGUI.PropertyField( pos2, colors );

		EditorGUI.indentLevel = previousIndent;
	}
}
