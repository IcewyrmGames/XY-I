using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatString : MonoBehaviour {
	[SerializeField, Range( 0f, 1f )] float _f;
	[SerializeField] float _mult;
	[SerializeField] bool _wholeNumber;

	public StringEvent onStringChanged = new StringEvent();

	void OnValidate()
	{
		SendChangedEvent();
	}

	void SendChangedEvent()
	{
		if( _wholeNumber )
		{
			onStringChanged.Invoke( ( (int)( _f * _mult ) ).ToString() );
		}
		else
		{
			onStringChanged.Invoke( ( _f * _mult ).ToString() );
		}
	}

	public void SetFloat( float f )
	{
		_f = f;
	}
}
