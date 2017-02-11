using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

[ExecuteInEditMode]
public class CharacterSlotManager : MonoBehaviour
{
	[System.Serializable]
	public struct Body
	{
		public BodySlot slot;
		public SpriteMeshRenderer renderer;

		public SpriteMesh defaultSprite;
		public ColorMask defaultColor;
	}

	[System.Serializable]
	public struct Decal
	{
		public DecalSlot slot;
		public SpriteRenderer renderer;

		public Sprite defaultSprite;
		public Color defaultColor;
	}

	[SerializeField] Body[] _parts = new Body[0];
	[SerializeField] Decal[] _decals = new Decal[0];

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
			Body part = _parts[i];
			if( part.slot && part.renderer && !_bodyRendererDict.ContainsKey( part.slot ) )
			{
				_bodyRendererDict.Add( part.slot, part.renderer );
			}
		}
		_decalRendererDict.Clear();
		for( int i = 0; i < _decals.Length; ++i )
		{
			Decal decal = _decals[i];
			if( decal.slot && decal.renderer && !_decalRendererDict.ContainsKey( decal.slot ) )
			{
				_decalRendererDict.Add( decal.slot, decal.renderer );
			}
		}
	}

	public void ApplyDefaultData()
	{
		foreach( Body body in _parts )
		{
			if( body.renderer )
			{
				body.renderer.SetProperties(
					body.defaultSprite,
					body.defaultColor
				);
			}
		}
		foreach( Decal decal in _decals )
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
