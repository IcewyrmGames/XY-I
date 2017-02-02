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
					if( !_partsDict.ContainsKey( _parts[i].slot ) )
					{
						_partsDict.Add( _parts[i].slot, _parts[i].renderer );
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
					if( !_decalsDict.ContainsKey( _decals[i].slot ) )
					{
						_decalsDict.Add( _decals[i].slot, _decals[i].renderer );
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
		SpriteMeshRenderer renderer = partsDict[data.slot];
		if( renderer )
		{
			renderer.enabled = (data.mesh == null);
			renderer.spriteMesh = data.mesh;
			renderer.color = data.color;
			renderer.RefreshRenderer();
		}
		else
		{
			Debug.LogError( "Character does not have part for slot: " + data.slot.name, this );
		}
	}

	public void ApplyDecalData( DecalSlotData data )
	{
		SpriteRenderer renderer = decalsDict[data.slot];
		if( renderer )
		{
			renderer.enabled = (data.sprite == null);
			renderer.sprite = data.sprite;
			renderer.color = data.color;
		}
		else
		{
			Debug.LogError( "Character does not have decal for slot: " + data.slot.name, this );
		}
	}
}
