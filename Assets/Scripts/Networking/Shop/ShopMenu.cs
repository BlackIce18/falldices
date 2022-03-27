using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gems;
    [SerializeField] private GameObject[] _shopItems;
    [SerializeField] private FirestoreDataBase _db;

    private void Awake()
    {
        _db.StartCoroutine(_db.GetUserGemsFromDB());
                Debug.Log(_db);
        Debug.Log(_db.UserData);
        Debug.Log(_db.UserData.Money);
        _gems.text = _db.UserData.Money.ToString();
        
    }
}
