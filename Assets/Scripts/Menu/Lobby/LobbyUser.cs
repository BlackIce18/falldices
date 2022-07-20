using UnityEngine;

public class LobbyUser
{
    private string _nickname;
    private Color32 _color;
    private int _modelId;
    private Models _model;

    public string Nickname { get { return _nickname; } }
    public Color32 Color { get { return _color; } }
    public int ModelId { get { return _modelId; } }
    public Models Model { get { return _model; } }

    public LobbyUser(string nickname, Color32 color, int modelId = 0)
    {
        _nickname = nickname;
        _color = color;
        _modelId = modelId;
        _model = ModelsAssociation.ModelsAssociationSingleton.GetModelById(_modelId);
    }
}
