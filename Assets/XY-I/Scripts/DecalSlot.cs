using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class DecalSlot : ScriptableObject
{
}

[System.Serializable]
public struct DecalSlotData
{
	public DecalSlot slot;
	public Sprite sprite;
}
