using UnityEngine;

public class FieldCell : MonoBehaviour
{
    [SerializeField] private GameObject _tileObject;
    [SerializeField] private PlayerAnimationDirection _animationDirection;
    [SerializeField] private bool _isAvailableToBuild = true;
    [SerializeField] private GameObject _UIToShow;
    public Player owner;
    public Enterprise enterprise;
    public PlayerAnimationDirection Direction => _animationDirection;
    public bool IsAvailableToBuild
    {
        get { return _isAvailableToBuild; }
        set { _isAvailableToBuild = value; }
    }
    public GameObject UIToShow => _UIToShow;
    public GameObject TileObject => _tileObject;
}
