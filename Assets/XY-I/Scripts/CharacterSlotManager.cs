using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class CharacterSlotManager : MonoBehaviour
{
	[System.Serializable]
	public struct Body
	{
		public BodySlot slot;
		public SpriteMeshRenderer renderer;

		public SpriteMesh defaultSprite;
		public Color defaultColor;
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

	Dictionary<BodySlot, Body> _partsDict;
	protected Dictionary<BodySlot, Body> partsDict {
		get {
			if( _partsDict == null )
			{
				_partsDict = new Dictionary<BodySlot, Body>();
				for( int i = 0; i < _parts.Length; ++i )
				{
					Body part = _parts[i];
					if( part.slot && part.renderer && !_partsDict.ContainsKey( part.slot ) )
					{
						_partsDict.Add( part.slot, part );
					}
				}
			}

			return _partsDict;
		}
	}

	Dictionary<DecalSlot, Decal> _decalsDict;
	protected Dictionary<DecalSlot, Decal> decalsDict {
		get {
			if( _decalsDict == null )
			{
				_decalsDict = new Dictionary<DecalSlot, Decal>();
				for( int i = 0; i < _decals.Length; ++i )
				{
					Decal decal = _decals[i];
					if( decal.slot && decal.renderer && !_decalsDict.ContainsKey( decal.slot ) )
					{
						_decalsDict.Add( decal.slot, decal );
					}
				}
			}

			return _decalsDict;
		}
	}

	void OnValidate()
	{
		_partsDict = null;
		_decalsDict = null;
	}

	public void ApplyDefaultData()
	{
		foreach( Body body in _parts )
		{
			ApplyBodyData( body.renderer, body.defaultSprite, body.defaultColor );
		}
		foreach( Decal decal in _decals )
		{
			ApplyDecalData( decal.renderer, decal.defaultSprite, decal.defaultColor );
		}
	}

	public void ApplyBodyData( BodySlotData data )
	{
		if( !data.slot ) return;

		Body body;
		if( partsDict.TryGetValue( data.slot, out body ) )
		{
			ApplyBodyData( body.renderer, data.sprite, data.color );
		}
		else
		{
			Debug.LogError( "Character does not have part for slot: " + data.slot, this );
		}
	}

	public void ApplyBodyData( SpriteMeshRenderer renderer, SpriteMesh sprite, Color color )
	{
		if( !renderer ) return;

		renderer.enabled = (sprite != null);
		renderer.spriteMesh = sprite;
		renderer.color = color;
		renderer.RefreshRenderer();
	}

	public void ApplyDecalData( DecalSlotData data )
	{
		if( !data.slot ) return;

		Decal decal;
		if( decalsDict.TryGetValue( data.slot, out decal ) )
		{
			ApplyDecalData( decal.renderer, data.sprite, data.color );
		}
		else
		{
			Debug.LogError( "Character does not have decal for slot: " + data.slot, this );
		}
	}

	public void ApplyDecalData( SpriteRenderer renderer, Sprite sprite, Color color )
	{
		if( !renderer ) return;

		renderer.enabled = (sprite != null);
		renderer.sprite = sprite;
		renderer.color = color;
	}
}
