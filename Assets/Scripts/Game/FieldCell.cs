using UnityEngine;

public class FieldCell : MonoBehaviour
{
    [SerializeField] private GameObject _tileObject;
    [SerializeField] private PlayerAnimationDirection _animationDirection;
    [SerializeField] private bool _isAvailableToBuild = true;
    [SerializeField] private GameObject _UIToShow;
    public int position;
    public Player owner;
    public Enterprise enterprise;
    public PlayerAnimationDirection Direction => _animationDirection;
    public bool IsAvailableToBuild
    {
        get { return _isAvailableToBuild; }
    }
    public GameObject UIToShow => _UIToShow;
    public GameObject TileObject => _tileObject;

    private void BlockAccessToBuild()
    {
        _isAvailableToBuild = false;
    }

    public void ChangeOwner(Player newOwner)
    {
        owner = newOwner;
    }

    public void BuildEnterprise(Player owner, Enterprise enterpriseToBuild) 
    {
        BlockAccessToBuild();
        ChangeOwner(owner);

    }
}
