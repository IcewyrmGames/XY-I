using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Outfit : ScriptableObject
{
	public OutfitSlot slot;

	public BodySlotData[] bodyOverrides = new BodySlotData[0];
	public DecalSlotData[] decalOverrides = new DecalSlotData[0];
}
