using UnityEngine;
using System;

[Serializable]
public struct MeshMaterial
{
    public Material material;
    public MeshRenderer meshRenderer;

    public void SetMaterial(Material newMaterial)
    {
        material = newMaterial;
    }

    public void SetMeshMaterial(Material newMaterial)
    {
        meshRenderer.material = newMaterial;
    }
}
public class PlayerColoring : MonoBehaviour
{
    [SerializeField] private MeshMaterial[] _meshMaterials;

    public void SetMaterials(Material material)
    {
        for(int i = 0; i < _meshMaterials.Length; i++)
        {
            _meshMaterials[i].SetMeshMaterial(material);
        }
    }
}
