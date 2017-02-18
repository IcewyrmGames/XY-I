using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterOutfitManager : MonoBehaviour {
	[System.Serializable]
	public class OutfitData
	{
		public OutfitSlot slot;
		public Outfit outfit;
		public ColorMask colors = ColorMask.white;
	}

	[SerializeField] CharacterSlotManager _slotManager;

	[SerializeField] OutfitData[] _outfits = new OutfitData[0];

	Dictionary<OutfitSlot, OutfitData> _outfitDict = new Dictionary<OutfitSlot, OutfitData>();

	void OnValidate()
	{
		RefreshDictionary();
		if( _slotManager )
		{
			Refresh();
		}
	}

	void OnEnabled()
	{
		RefreshDictionary();
	}

	void RefreshDictionary()
	{
		_outfitDict.Clear();
		foreach( OutfitData data in _outfits )
		{
			if( data.slot )
			{
				_outfitDict.Add( data.slot, data );
			}
		}
	}

	public void Refresh()
	{
		_slotManager.ApplyDefaultData();
		foreach( OutfitData outfitData in _outfits )
		{
			if( outfitData.outfit )
			{
				foreach( BodySlotData bodyData in outfitData.outfit.bodyOverrides )
				{
					_slotManager.ApplyBodyData(
						bodyData.slot,
						bodyData.sprite,
						outfitData.colors
					);
				}
				foreach( DecalSlotData decalData in outfitData.outfit.decalOverrides )
				{
					_slotManager.ApplyDecalData(
						decalData.slot,
						decalData.sprite,
						outfitData.colors.r
					);
				}
			}
		}
	}

	public void EquipOutfit( Outfit outfit )
	{
		if( _outfitDict.ContainsKey( outfit.slot ) )
		{
			_outfitDict[outfit.slot].outfit = outfit;
		}
	}

	public void UnequipOutfit( OutfitSlot slot )
	{
		if( _outfitDict.ContainsKey( slot ) )
		{
			_outfitDict[slot].outfit = null;
		}
	}

	public void SetOutfitColor( OutfitSlot slot, ColorMask colors )
	{
		if( _outfitDict.ContainsKey( slot ) )
		{
			_outfitDict[slot].colors = colors;
		}
	}
}
