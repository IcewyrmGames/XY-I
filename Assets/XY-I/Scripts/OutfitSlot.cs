using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class OutfitSlot : ScriptableObject
{
	[SerializeField] int _applicationOrder;
	public int ApplicationOrder {get {return _applicationOrder;}}
}
