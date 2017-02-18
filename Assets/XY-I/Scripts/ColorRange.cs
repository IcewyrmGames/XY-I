using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRange : MonoBehaviour {
	public enum Channel
	{
		Red,
		Green,
		Blue,
	};

	[SerializeField] Color _color;
	[SerializeField] Channel _channel;

	public ColorEvent onMinChanged = new ColorEvent();
	public ColorEvent onMaxChanged = new ColorEvent();

	public void SetColor( Color color )
	{
		_color = color;
		switch( _channel )
		{
			case Channel.Red:
			onMinChanged.Invoke( new Color( 0, _color.g, _color.b ) );
			onMaxChanged.Invoke( new Color( 1, _color.g, _color.b ) );
			break;

			case Channel.Green:
			onMinChanged.Invoke( new Color( _color.r, 0, _color.b ) );
			onMaxChanged.Invoke( new Color( _color.r, 1, _color.b ) );
			break;

			case Channel.Blue:
			onMinChanged.Invoke( new Color( _color.r, _color.g, 0 ) );
			onMaxChanged.Invoke( new Color( _color.r, _color.g, 1 ) );
			break;
		}
	}
}
