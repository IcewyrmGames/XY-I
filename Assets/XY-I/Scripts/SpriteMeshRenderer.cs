using UnityEngine;
using Anima2D;

[ExecuteInEditMode]
[RequireComponent( typeof( SkinnedMeshRenderer ) )]
public class SpriteMeshRenderer : MonoBehaviour
{
	[SerializeField] int _sortingLayerID = 0;
	[SerializeField] int _sortingOrder = 0;

	[SerializeField] SpriteMesh _spriteMesh;
	public SpriteMesh spriteMesh {
		get {return _spriteMesh;}
		set {
			_spriteMesh = value;
			if( _spriteMesh )
			{
				meshRenderer.sharedMesh = _spriteMesh.sharedMesh;
			}
		}
	}

	[SerializeField] Color _color = Color.white;
	public Color color {
		get {return _color;}
		set {_color = value;}
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
			_color = oldInstance.color;

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

	void OnEnable()
	{
		meshRenderer.enabled = true;
	}

	void OnDisable()
	{
		meshRenderer.enabled = false;
	}

	void OnWillRenderObject()
	{
		if( spriteMesh && materialProperties != null )
		{
			meshRenderer.sortingLayerID = _sortingLayerID;
			meshRenderer.sortingOrder = _sortingOrder;

			materialProperties.SetColor( "_Color", _color );
			materialProperties.SetTexture( "_MainTex", spriteMesh.sprite.texture );

			meshRenderer.SetPropertyBlock( materialProperties );
		}
	}

	void OnValidate()
	{
		if( spriteMesh )
		{
			meshRenderer.sharedMesh = spriteMesh.sharedMesh;
			meshRenderer.bones = _bones;
		}
	}
}
