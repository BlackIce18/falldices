using System;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gems;
    [SerializeField] private GameObject _shopItemPrefab;
    [SerializeField] private GameObject _contentScrollView;
    [SerializeField] private FirestoreDataBase _db;

    private Dictionary<string, ShopItemFirebaseDataConstruct> _purchasedItems = new Dictionary<string, ShopItemFirebaseDataConstruct>();
    private Dictionary<string, ShopItemFirebaseDataConstruct> _shopItems = new Dictionary<string, ShopItemFirebaseDataConstruct>();
    public Dictionary<string, ShopItemFirebaseDataConstruct> ShopItems => _shopItems;
    public Dictionary<string, ShopItemFirebaseDataConstruct> PurchasedItems => _purchasedItems;

    private void Awake()
    {
        GetShopItems();
    }
    private void OnEnable()
    {
        _gems.text = _db.UserData.Money.ToString();
        
    }

    private void GetShopItems()
    {
        Query allItemsQuery = _db.Db.Collection(FirestoreDataBase.NameItemsCollection);
        allItemsQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allItemsQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allItemsQuerySnapshot.Documents)
            {
                Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
                ShopItemFirebaseDataConstruct item = documentSnapshot.ConvertTo<ShopItemFirebaseDataConstruct>();
                _shopItems.Add(documentSnapshot.Id, item);
                /*
                 Dictionary<string, object> item = documentSnapshot.ToDictionary();
                 foreach (KeyValuePair<string, object> pair in item)
                 {
                     Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                 }
                */
            }
        }).ContinueWithOnMainThread(task=> {
            GetPurchasedItems();
        });
    }

    private void GetPurchasedItems()
    {
        Query allItemsQuery = _db.Db.Collection(FirestoreDataBase.NamePurchasedCollection).Document(_db.GetActiveUserId()).Collection(FirestoreDataBase.NamePurchasedCollection);
        allItemsQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allItemsQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allItemsQuerySnapshot.Documents)
            {
                Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
                _purchasedItems.Add(documentSnapshot.Id, _shopItems[documentSnapshot.Id]);
                /*documentSnapshot.ConvertTo<ShopItemFirebaseDataConstruct>();
                Debug.Log(documentSnapshot);

                Dictionary<string, object> item = documentSnapshot.ToDictionary();
                _purchasedItems.Add(item);
                foreach (KeyValuePair<string, object> pair in item)
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }*/
            }
        }).ContinueWithOnMainThread(task => {
            CreateItemsInUI();
        }); ;
    }

    private void CreateItemsInUI()
    {
        foreach(KeyValuePair<string, ShopItemFirebaseDataConstruct> item in _shopItems)
        {
            GameObject itemGameObject = Instantiate(_shopItemPrefab, _contentScrollView.transform);
            ShopItem shopItem = itemGameObject.GetComponent<ShopItem>();
            shopItem.SetPrice(item.Value.Price.ToString());
        }
    }
}
