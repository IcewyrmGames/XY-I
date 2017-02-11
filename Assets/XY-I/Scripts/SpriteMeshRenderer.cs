using UnityEngine;
using Anima2D;

[ExecuteInEditMode]
[RequireComponent( typeof( SkinnedMeshRenderer ) )]
public class SpriteMeshRenderer : MonoBehaviour
{
	int _MainTexProperty;

	[SerializeField] int _sortingLayerID = 0;
	[SerializeField] int _sortingOrder = 0;

	[SerializeField] SpriteMesh _spriteMesh;
	public SpriteMesh spriteMesh {get {return _spriteMesh;}}

	[SerializeField] ColorMask _colors = ColorMask.white;
	public ColorMask colors {get {return _colors;}}

	[SerializeField] Transform[] _bones = new Transform[0];

	SkinnedMeshRenderer _meshRenderer;
	public SkinnedMeshRenderer meshRenderer {
		get {
			if( !_meshRenderer )
			{
				_meshRenderer = GetComponent<SkinnedMeshRenderer>();
			}
			return _meshRenderer;
		}
	}

	MaterialPropertyBlock _materialProperties;
	public MaterialPropertyBlock materialProperties {
		get {
			if( _materialProperties == null )
			{
				_materialProperties = new MaterialPropertyBlock();
				_MainTexProperty = Shader.PropertyToID( "_MainTex" );
			}
			return _materialProperties;
		}
	}

	void Awake()
	{
		SpriteMeshInstance oldInstance = GetComponent<SpriteMeshInstance>();
		if( oldInstance )
		{
			SetSpriteMesh( oldInstance.spriteMesh );

			_bones = new Transform[oldInstance.bones.Count];
			for( int i = 0; i < oldInstance.bones.Count; ++i )
			{
				_bones[i] = oldInstance.bones[i].transform;
			}

			_sortingLayerID = oldInstance.sortingLayerID;
			_sortingOrder = oldInstance.sortingOrder;

			DestroyImmediate( oldInstance );
		}
	}

	void Start()
	{
		meshRenderer.sortingLayerID = _sortingLayerID;
		meshRenderer.sortingOrder = _sortingOrder;

		SetProperties( _spriteMesh, _colors );
	}

	void OnValidate()
	{
		meshRenderer.bones = _bones;

		Start();
	}

	public void SetSpriteMesh( SpriteMesh newSpriteMesh )
	{
		_spriteMesh = newSpriteMesh;
		if( _spriteMesh )
		{
			enabled = true;
			meshRenderer.enabled = true;

			meshRenderer.sharedMesh = _spriteMesh.sharedMesh;
			materialProperties.SetTexture( _MainTexProperty, _spriteMesh.sprite.texture );
		}
		else
		{
			enabled = false;
			meshRenderer.enabled = false;

			meshRenderer.sharedMesh = null;
			materialProperties.SetTexture( _MainTexProperty, Texture2D.whiteTexture );
		}
	}

	public void SetColors( ColorMask newColors )
	{
		_colors = newColors;
		materialProperties.SetColorMask( _colors );
	}

	public void SetProperties( SpriteMesh newSpriteMesh, ColorMask newColors )
	{
		materialProperties.Clear();

		SetSpriteMesh( newSpriteMesh );
		SetColors( newColors );

		meshRenderer.SetPropertyBlock( materialProperties );
	}
}
