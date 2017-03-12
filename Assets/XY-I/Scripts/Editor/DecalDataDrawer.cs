using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer( typeof( DecalData ) )]
public class DecalDataDrawer : PropertyDrawer
{
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return ( EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing ) * 2f;
	}

	public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
	{
		SerializedProperty slot = property.FindPropertyRelative( "slot" );
		SerializedProperty sprite = property.FindPropertyRelative( "sprite" );

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

		EditorGUI.PropertyField( pos0, slot, GUIContent.none );
		EditorGUI.PropertyField( pos1, sprite );

		EditorGUI.indentLevel = previousIndent;
	}
}
