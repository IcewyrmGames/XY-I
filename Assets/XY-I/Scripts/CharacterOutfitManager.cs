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

	void OnValidate()
	{
		if( _slotManager )
		{
			Refresh();
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
		for( int i = 0; i < _outfits.Length; ++i )
		{
			if( _outfits[i].slot == outfit.slot )
			{
				_outfits[i].outfit = outfit;
				return;
			}
		}
	}

	public void UnequipOutfit( OutfitSlot slot )
	{
		for( int i = 0; i < _outfits.Length; ++i )
		{
			if( _outfits[i].slot == slot )
			{
				_outfits[i].outfit = null;
				return;
			}
		}
	}
}
