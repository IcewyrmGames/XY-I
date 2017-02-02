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
}
