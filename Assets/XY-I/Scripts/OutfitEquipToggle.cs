using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitEquipToggle : MonoBehaviour
{
	public Outfit outfit;
	public CharacterOutfitManager manager;

	public void OnToggle( bool isToggled )
	{
		if( isToggled )
		{
			manager.EquipOutfit( outfit );
		}
		else
		{
			manager.UnequipOutfit( outfit.slot );
		}
		manager.Refresh();
	}
}
