using System;
using System.Collections.Generic;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gems;
    [SerializeField] private GameObject _shopItemPrefab;
    [SerializeField] private GameObject _contentScrollView;
    [SerializeField] private FirestoreDataBase _db;
    [SerializeField] private ModelsAssociation _modelsAssociation;

    private Dictionary<string, ShopItemFirebaseDataConstruct> _purchasedItems = new Dictionary<string, ShopItemFirebaseDataConstruct>();
    private Dictionary<string, ShopItemFirebaseDataConstruct> _shopItems = new Dictionary<string, ShopItemFirebaseDataConstruct>();
    public Dictionary<string, ShopItemFirebaseDataConstruct> ShopItems => _shopItems;
    public Dictionary<string, ShopItemFirebaseDataConstruct> PurchasedItems => _purchasedItems;
    private void Start()
    {
        DocumentReference docRef = _db.Db.Collection(FirestoreDataBase.NameUserCollection).Document(_db.GetActiveUserId());
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
                _db.userFirebaseDataConstruct = snapshot.ConvertTo<UserFirebaseDataConstruct>();
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        }).ContinueWithOnMainThread(task =>
        {
            _gems.text = _db.userFirebaseDataConstruct.Money.ToString();
            GetShopItems();
        });
    }


    private void OnEnable()
    {
        
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
            shopItem.ChangeSprite(_modelsAssociation.GetModelById(item.Value.Id).Sprite);
        }
    }
}
