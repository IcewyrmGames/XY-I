using UnityEngine;
using Anima2D;

[ExecuteInEditMode]
[RequireComponent( typeof( SkinnedMeshRenderer ) )]
public class SpriteMeshRenderer : MonoBehaviour
{
	[SerializeField, HideInInspector] SkinnedMeshRenderer _meshRenderer;
	public SkinnedMeshRenderer meshRenderer {
		get {
			if( !_meshRenderer )
			{
				_meshRenderer = GetComponent<SkinnedMeshRenderer>();
			}
			return _meshRenderer;
		}
	}

	[SerializeField] SpriteMesh _spriteMesh;
	public SpriteMesh spriteMesh {
		get {return _spriteMesh;}
		set {_spriteMesh = value;}
	}

	[SerializeField] Transform[] _bones;

	[SerializeField] Color color = Color.white;
	[SerializeField] int sortingLayerID = 0;
	[SerializeField] int sortingOrder = 0;

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
			color = oldInstance.color;

			_bones = new Transform[oldInstance.bones.Count];
			for( int i = 0; i < oldInstance.bones.Count; ++i )
			{
				_bones[i] = oldInstance.bones[i].transform;
			}

			sortingLayerID = oldInstance.sortingLayerID;
			sortingOrder = oldInstance.sortingOrder;

			DestroyImmediate( oldInstance );
		}
	}

	void OnWillRenderObject()
	{
		if( spriteMesh && meshRenderer && materialProperties != null )
		{
			meshRenderer.sortingLayerID = sortingLayerID;
			meshRenderer.sortingOrder = sortingOrder;

			materialProperties.SetColor( "_Color", color );
			meshRenderer.SetPropertyBlock( materialProperties );
		}
	}

	void OnValidate()
	{
		if( spriteMesh && meshRenderer )
		{
			meshRenderer.sharedMesh = spriteMesh.sharedMesh;
			meshRenderer.sharedMaterials = spriteMesh.sharedMaterials;

			if( _bones.GetLength(0) != spriteMesh.sharedMesh.bindposes.GetLength(0) )
			{
				_bones = new Transform[spriteMesh.sharedMesh.bindposes.GetLength(0)];
			}

			meshRenderer.bones = _bones;
		}
	}
}
