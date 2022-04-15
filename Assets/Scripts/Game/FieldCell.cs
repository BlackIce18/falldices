using UnityEngine;
using UnityEngine.UI;

public class FieldCell : MonoBehaviour
{
    [SerializeField] private GameObject _tileObject;
    [SerializeField] private PlayerAnimationDirection _animationDirection;
    [SerializeField] private bool _isAvailableToBuild = true;
    [SerializeField] private GameObject _UIToShow;
    public PlayerAnimationDirection Direction => _animationDirection;
    public bool IsAvailableToBuild => _isAvailableToBuild;
    public GameObject UIToShow => _UIToShow;
    public GameObject TileObject => _tileObject;
}
