using UnityEngine;
using UnityEngine.UI;

public class LobbyChooseUserColor : LobbyWindow, ICanResetObjects
{
    [SerializeField] private Image _previewImage;

    public override void ResetObjects()
    {
        //_previewImage.color = new Color32(139, 139, 139, 110);
    }
}
