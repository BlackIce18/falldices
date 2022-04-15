using System;
using UnityEngine;

[Serializable]
public struct Models 
{
    public int Id;
    public Sprite Sprite;
    public GameObject Prefab;
}
public class ModelsAssociation : MonoBehaviour
{
    [SerializeField] private Models[] _models;

    public Models GetModelById(int id)
    {
        for(int i = 0; i < _models.Length; i++)
        {
            if (_models[i].Id == id) 
                return _models[i];
        }
        return _models[0];
    }
}
