using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

[CreateAssetMenu()]
public class BodySlot : ScriptableObject
{
}

[System.Serializable]
public struct BodySlotData
{
	public BodySlot slot;
	public SpriteMesh sprite;
	public Color color;
}
