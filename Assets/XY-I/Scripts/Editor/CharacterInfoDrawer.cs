using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer( typeof( CharacterSlotManager.Body ) )]
[CustomPropertyDrawer( typeof( CharacterSlotManager.Decal ) )]
public class CharacterInfoDrawer : PropertyDrawer
{
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return ( EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing ) * 2f;
	}

	public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
	{
		SerializedProperty slot = property.FindPropertyRelative( "slot" );
		SerializedProperty renderer = property.FindPropertyRelative( "renderer" );
		SerializedProperty defaultSprite = property.FindPropertyRelative( "defaultSprite" );
		SerializedProperty defaultColor = property.FindPropertyRelative( "defaultColor" );

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
		Rect pos3 = new Rect(
			position.x,
			pos2.y + pos2.height + EditorGUIUtility.standardVerticalSpacing,
			position.width,
			position.height
		);

		EditorGUI.PropertyField( pos0, slot, GUIContent.none );

		EditorGUI.BeginChangeCheck();
		EditorGUI.PropertyField( pos1, renderer );
		if( EditorGUI.EndChangeCheck() )
		{
			SpriteMeshRenderer spriteMeshRenderer = renderer.objectReferenceValue as SpriteMeshRenderer;
			SpriteRenderer spriteRenderer = renderer.objectReferenceValue as SpriteRenderer;
			if( spriteMeshRenderer != null )
			{
				defaultSprite.objectReferenceValue = spriteMeshRenderer.spriteMesh;
				defaultColor.FindPropertyRelative( "r" ).colorValue = spriteMeshRenderer.colors.r;
				defaultColor.FindPropertyRelative( "g" ).colorValue = spriteMeshRenderer.colors.g;
				defaultColor.FindPropertyRelative( "b" ).colorValue = spriteMeshRenderer.colors.b;
			}
			else if ( spriteRenderer != null )
			{
				defaultSprite.objectReferenceValue = spriteRenderer.sprite;
				defaultColor.colorValue = spriteRenderer.color;
			}
			else
			{
				defaultSprite.objectReferenceValue = null;
				defaultColor.colorValue = Color.white;
			}
		}

		EditorGUI.PropertyField( pos2, defaultSprite );
		EditorGUI.PropertyField( pos3, defaultColor );

		EditorGUI.indentLevel = previousIndent;
	}
}
