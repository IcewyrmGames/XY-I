using UnityEngine;
using Anima2D;

[ExecuteInEditMode]
[RequireComponent( typeof( SkinnedMeshRenderer ) )]
public class SpriteMeshRenderer : MonoBehaviour
{
	int _MainTex;

	[SerializeField] int _sortingLayerID = 0;
	[SerializeField] int _sortingOrder = 0;

	[SerializeField] SpriteMesh _spriteMesh;
	public SpriteMesh spriteMesh {
		get {return _spriteMesh;}
		set {
			_spriteMesh = value;
			if( _spriteMesh )
			{
				enabled = true;
				meshRenderer.enabled = true;

				meshRenderer.sharedMesh = _spriteMesh.sharedMesh;
				materialProperties.SetTexture( _MainTex, _spriteMesh.sprite.texture );
			}
			else
			{
				enabled = false;
				meshRenderer.enabled = false;

				meshRenderer.sharedMesh = null;
				materialProperties.SetTexture( _MainTex, Texture2D.whiteTexture );
			}
			meshRenderer.SetPropertyBlock( materialProperties );
		}
	}

	[SerializeField] ColorMask _color = ColorMask.white;
	public ColorMask color {
		get {return _color;}
		set {
			_color = value;
			materialProperties.SetColorMask( _color );
			meshRenderer.SetPropertyBlock( materialProperties );
		}
	}

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
				_MainTex = Shader.PropertyToID( "_MainTex" );
			}
			return _materialProperties;
		}
	}

	void Awake()
	{
		SpriteMeshInstance oldInstance = GetComponent<SpriteMeshInstance>();
		if( oldInstance )
		{
			spriteMesh = oldInstance.spriteMesh;

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

		spriteMesh = _spriteMesh;
		color = _color;
	}

	void OnValidate()
	{
		meshRenderer.bones = _bones;

		Start();
	}

	[ContextMenu("ResetColorMask")]
	void ResetColorMaskValue()
	{
		_color = ColorMask.white;
	}
}
