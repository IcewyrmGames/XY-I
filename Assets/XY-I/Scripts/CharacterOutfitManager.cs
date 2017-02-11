using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOutfitManager : MonoBehaviour {
	[System.Serializable]
	public class OutfitData
	{
		public OutfitSlot slot;
		public Outfit outfit;
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
		foreach( OutfitData data in _outfits )
		{
			if( data.outfit )
			{
				foreach( BodySlotData bodyData in data.outfit.bodyOverrides )
				{
					_slotManager.ApplyBodyData( bodyData );
				}
				foreach( DecalSlotData decalData in data.outfit.decalOverrides )
				{
					_slotManager.ApplyDecalData( decalData );
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
}
