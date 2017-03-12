using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Outfit : ScriptableObject
{
	public OutfitSlot slot;

	public BodyData[] bodyOverrides = new BodyData[0];
	public DecalData[] decalOverrides = new DecalData[0];
}
