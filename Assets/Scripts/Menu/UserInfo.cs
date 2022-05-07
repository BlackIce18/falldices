using UnityEngine;

public class UserInfo
{
    public Color32 color;
    public Models model;
    public Sprite icon;
    public string nickname;

    public UserInfo(Color32 _color, Models _model, Sprite _icon, string _nickname)
    {
        color = _color;
        model = _model;
        icon = _icon;
        nickname = _nickname;
    }
}
