using System.Collections.Generic;
using UnityEngine;

struct Ownership
{
    public Enterprise enterprise;
    public int upgradeLvl;
}
public class PlayerOwnership : MonoBehaviour
{
    private List<Ownership> _enterprises = new List<Ownership>();

    public void AddToOwn(Enterprise enterprise)
    {
        Ownership ownership = new Ownership();
        ownership.enterprise = enterprise;
        ownership.upgradeLvl = 0;
        _enterprises.Add(ownership);
    }
}
