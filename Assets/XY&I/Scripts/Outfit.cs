using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Outfit : ScriptableObject
{
	[System.Serializable]
	public struct SpriteOverride
	{
		public SpriteSlot spriteSlot;
		public Sprite sprite;
	}

	public OutfitSlot outfitSlot;

	public List<SpriteOverride> spriteOverrides;
}
