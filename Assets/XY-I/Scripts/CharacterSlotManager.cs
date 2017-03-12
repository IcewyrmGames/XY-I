using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

[ExecuteInEditMode]
public class CharacterSlotManager : MonoBehaviour
{
	[SerializeField] BodyRendererData[] _parts = new BodyRendererData[0];
	[SerializeField] DecalRendererData[] _decals = new DecalRendererData[0];

	Dictionary<BodySlot, SpriteMeshRenderer> _bodyRendererDict = new Dictionary<BodySlot, SpriteMeshRenderer>();
	Dictionary<DecalSlot, SpriteRenderer> _decalRendererDict = new Dictionary<DecalSlot, SpriteRenderer>();

	void OnValidate()
	{
		RefreshDictionaries();
	}

	void OnEnable()
	{
		RefreshDictionaries();
	}

	void RefreshDictionaries()
	{
		_bodyRendererDict.Clear();
		for( int i = 0; i < _parts.Length; ++i )
		{
			BodyRendererData part = _parts[i];
			if( part.slot && part.renderer && !_bodyRendererDict.ContainsKey( part.slot ) )
			{
				_bodyRendererDict.Add( part.slot, part.renderer );
			}
		}
		_decalRendererDict.Clear();
		for( int i = 0; i < _decals.Length; ++i )
		{
			DecalRendererData decal = _decals[i];
			if( decal.slot && decal.renderer && !_decalRendererDict.ContainsKey( decal.slot ) )
			{
				_decalRendererDict.Add( decal.slot, decal.renderer );
			}
		}
	}

	public void ApplyDefaultData()
	{
		foreach( BodyRendererData body in _parts )
		{
			if( body.renderer )
			{
				body.renderer.SetProperties(
					body.defaultSprite,
					body.defaultColor
				);
			}
		}
		foreach( DecalRendererData decal in _decals )
		{
			if( decal.renderer )
			{
				decal.renderer.sprite = decal.defaultSprite;
				decal.renderer.color = decal.defaultColor;
			}
		}
	}

	public void ApplyBodyData( BodySlot slot, SpriteMesh spriteMesh, ColorMask colors )
	{
		if( !slot ) return;

		SpriteMeshRenderer renderer;
		if( _bodyRendererDict.TryGetValue( slot, out renderer ) && renderer )
		{
			renderer.SetProperties( spriteMesh, colors );
		}
		else
		{
			Debug.LogError( "Character does not have part for slot: " + slot, this );
		}
	}

	public void ApplyDecalData( DecalSlot slot, Sprite sprite, Color color )
	{
		if( !slot ) return;

		SpriteRenderer renderer;
		if( _decalRendererDict.TryGetValue( slot, out renderer ) && renderer )
		{
			renderer.sprite = sprite;
			renderer.color = color;
		}
		else
		{
			Debug.LogError( "Character does not have decal for slot: " + slot, this );
		}
	}
}
