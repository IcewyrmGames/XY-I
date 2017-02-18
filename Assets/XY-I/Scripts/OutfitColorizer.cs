using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitColorizer : MonoBehaviour
{
	public CharacterOutfitManager outfitManager;
	public OutfitSlot slot;
	public ColorMask colors;

	public Color red {
		get {return colors.r;}
		set {
			colors.r = value;
			outfitManager.SetOutfitColor( slot, colors );
			outfitManager.Refresh();
		}
	}

	public Color green {
		get {return colors.g;}
		set {
			colors.g = value;
			outfitManager.SetOutfitColor( slot, colors );
			outfitManager.Refresh();
		}
	}

	public Color blue {
		get {return colors.b;}
		set {
			colors.b = value;
			outfitManager.SetOutfitColor( slot, colors );
			outfitManager.Refresh();
		}
	}
}
