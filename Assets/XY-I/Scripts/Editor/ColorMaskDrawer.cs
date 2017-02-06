using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer( typeof( ColorMask ) )]
public class ColorMaskDrawer : PropertyDrawer
{
	const float SPACING = 5f;
	const float LABEL_WIDTH = 15f;

	static GUIContent colorR = new GUIContent( "R", "Red-channel masked color" );
	static GUIContent colorG = new GUIContent( "G", "Green-channel masked color" );
	static GUIContent colorB = new GUIContent( "B", "Blue-channel masked color" );
	static GUIContent colorA = new GUIContent( "A", "Alpha-channel masked color" );

	public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
	{
		position = EditorGUI.PrefixLabel( position, label );

		position.y += 2f; position.height = EditorGUIUtility.singleLineHeight;
		float width = position.width / 4f;

		Rect rectColorR = new Rect(
			position.x + width * 0f, position.y,
			width - SPACING, position.height
		);
		Rect rectColorG = new Rect(
			position.x + width * 1f, position.y,
			width - SPACING, position.height
		);
		Rect rectColorB = new Rect(
			position.x + width * 2f, position.y,
			width - SPACING, position.height
		);
		Rect rectColorA = new Rect(
			position.x + width * 3f, position.y,
			width - SPACING, position.height
		);

		EditorGUIUtility.labelWidth = LABEL_WIDTH;
		ColorField( rectColorR, property.FindPropertyRelative( "r" ), colorR );
		ColorField( rectColorG, property.FindPropertyRelative( "g" ), colorG );
		ColorField( rectColorB, property.FindPropertyRelative( "b" ), colorB );
		ColorField( rectColorA, property.FindPropertyRelative( "a" ), colorA );
	}

	static void ColorField( Rect rect, SerializedProperty property, GUIContent label )
	{
		property.colorValue = EditorGUI.ColorField(
			rect,
			label,
			property.colorValue,
			false,
			true,
			false,
			null
		);
	}
}
