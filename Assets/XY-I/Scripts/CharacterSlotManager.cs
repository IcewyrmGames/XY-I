using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlotManager : MonoBehaviour
{
	[System.Serializable]
	public struct Body
	{
		public BodySlot slot;
		public SpriteMeshRenderer renderer;
	}

	[System.Serializable]
	public struct Decal
	{
		public DecalSlot slot;
		public SpriteRenderer renderer;
	}

	[SerializeField] Body[] _parts = new Body[0];
	[SerializeField] Decal[] _decals = new Decal[0];

	[SerializeField] BodySlotData[] _defaultBodyData = new BodySlotData[0];
	[SerializeField] DecalSlotData[] _defaultDecalData = new DecalSlotData[0];

	Dictionary<BodySlot, SpriteMeshRenderer> _partsDict;
	protected Dictionary<BodySlot, SpriteMeshRenderer> partsDict {
		get {
			if( _partsDict == null )
			{
				_partsDict = new Dictionary<BodySlot, SpriteMeshRenderer>();
				for( int i = 0; i < _parts.Length; ++i )
				{
					Body part = _parts[i];
					if( part.slot && part.renderer && !_partsDict.ContainsKey( part.slot ) )
					{
						_partsDict.Add( part.slot, part.renderer );
					}
				}
			}

			return _partsDict;
		}
	}

	Dictionary<DecalSlot, SpriteRenderer> _decalsDict;
	protected Dictionary<DecalSlot, SpriteRenderer> decalsDict {
		get {
			if( _decalsDict == null )
			{
				_decalsDict = new Dictionary<DecalSlot, SpriteRenderer>();
				for( int i = 0; i < _decals.Length; ++i )
				{
					Decal decal = _decals[i];
					if( decal.slot && decal.renderer && !_decalsDict.ContainsKey( decal.slot ) )
					{
						_decalsDict.Add( decal.slot, decal.renderer );
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
		foreach( BodySlotData data in _defaultBodyData )
		{
			ApplyBodyData( data );
		}
		foreach( DecalSlotData data in _defaultDecalData )
		{
			ApplyDecalData( data );
		}
	}

	public void ApplyBodyData( BodySlotData data )
	{
		if( !data.slot ) return;

		SpriteMeshRenderer renderer = null;
		if( partsDict.TryGetValue( data.slot, out renderer ) && renderer )
		{
			renderer.enabled = (data.sprite == null);
			renderer.spriteMesh = data.sprite;
			renderer.color = data.color;
			renderer.RefreshRenderer();
		}
		else
		{
			Debug.LogError( "Character does not have part for slot: " + data.slot, this );
		}
	}

	public void ApplyDecalData( DecalSlotData data )
	{
		if( !data.slot ) return;

		SpriteRenderer renderer = null;
		if( decalsDict.TryGetValue( data.slot, out renderer ) && renderer )
		{
			renderer.enabled = (data.sprite == null);
			renderer.sprite = data.sprite;
			renderer.color = data.color;
		}
		else
		{
			Debug.LogError( "Character does not have decal for slot: " + data.slot, this );
		}
	}
}
