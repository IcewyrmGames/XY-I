using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class SpriteOverrideEnumerator : MonoBehaviour
{
	[System.Serializable]
	public struct OverrideSlot
	{
		public SpriteSlot slot;
		public SpriteMeshInstance spriteMeshInstance;
	}

	[SerializeField] List<OverrideSlot> _overrideSlots;

	public void ApplyOverride( SpriteSlot slot, Sprite sprite )
	{
		for( int i = 0; i < _overrideSlots.Count; ++i )
		{
			if( _overrideSlots[i].slot == slot )
			{
				_overrideSlots[i].spriteMeshInstance.spriteOverride = sprite;
			}
		}
	}
}
