using UnityEngine;

public class FieldCell : MonoBehaviour
{
    [SerializeField] private GameObject _tileObject;
    [SerializeField] private PlayerAnimationDirection _animationDirection;

    public PlayerAnimationDirection Direction { get { return _animationDirection; } }
}
