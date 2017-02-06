using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ColorMask
{
	public Color r;
	public Color g;
	public Color b;
	public Color a;

	public static readonly ColorMask white = new ColorMask( Color.white, Color.white, Color.white, Color.white );

	public ColorMask( Color r, Color g, Color b, Color a )
	{
		this.r = r;
		this.g = g;
		this.b = b;
		this.a = a;
	}
}

static public class ColorMaskExtensions
{
	static int _idColorR = Shader.PropertyToID( "_ColorR" );
	static int _idColorG = Shader.PropertyToID( "_ColorG" );
	static int _idColorB = Shader.PropertyToID( "_ColorB" );
	static int _idColorA = Shader.PropertyToID( "_ColorA" );

	static public void SetColorMask( this MaterialPropertyBlock block, ColorMask mask )
	{
		block.SetColor( _idColorR, mask.r );
		block.SetColor( _idColorG, mask.g );
		block.SetColor( _idColorB, mask.b );
		block.SetColor( _idColorA, mask.a );
	}
}
