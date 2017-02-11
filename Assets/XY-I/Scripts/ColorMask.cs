using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ColorMask
{
	public Color r;
	public Color g;
	public Color b;

	public static readonly ColorMask white = new ColorMask( Color.white, Color.white, Color.white );

	public ColorMask( Color r, Color g, Color b)
	{
		this.r = r;
		this.g = g;
		this.b = b;
	}
}

static public class ColorMaskExtensions
{
	static int _idColor0 = Shader.PropertyToID( "_Color" ); //omit index to allow this to work seamlessly with legacy shaders
	static int _idColor1 = Shader.PropertyToID( "_Color1" );
	static int _idColor2 = Shader.PropertyToID( "_Color2" );

	static public void SetColorMask( this MaterialPropertyBlock block, ColorMask mask )
	{
		block.SetColor( _idColor0, mask.r );
		block.SetColor( _idColor1, mask.g );
		block.SetColor( _idColor2, mask.b );
	}
}
