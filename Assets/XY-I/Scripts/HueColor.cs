using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions.ColorPicker;

public class HueColor : MonoBehaviour {
	[SerializeField, Range( 0f, 1f )] float _hue;

	public ColorChangedEvent onColorChanged = new ColorChangedEvent();

	void OnValidate()
	{
		SendChangedEvent();
	}

	void SendChangedEvent()
	{
		Color c = HSVUtil.ConvertHsvToRgb( _hue * 360f, 1, 1, 1 );
		onColorChanged.Invoke( c );
	}

	public void SetHue( float hue )
	{
		_hue = hue;
		SendChangedEvent();
	}
}
