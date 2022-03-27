using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
using TMPro;

public class FirestoreDataBase : MonoBehaviour
{
    private FirebaseFirestore _db;
    private FirebaseUser _activeUser;
    private UserFirebaseDataConstruct _userData;
    private DependencyStatus _dependencyStatus;

    public UserFirebaseDataConstruct UserData { get => _userData; set => _userData = value; }

    [Header("Firebase Collections")]
    private const string USERSCOLLECTION = "Users";
    private const string PURCHASEDITEMSCOLLECTION = "PurchasedItems";

    public static string GetNameUserCollection => USERSCOLLECTION;
    public static string GetNamePurchasedCollection => PURCHASEDITEMSCOLLECTION;

    [SerializeField] private AuthManager _authManager;

    private void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            _dependencyStatus = task.Result;
            if (_dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + _dependencyStatus);
            }
        });
        DontDestroy();
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up FirestoreDataBase");

        //Set the authentication instance object
        _db = FirebaseFirestore.DefaultInstance;
        _authManager.AuthSetter = FirebaseAuth.DefaultInstance;

        Debug.Log(_db);
        Debug.Log(FirebaseAuth.DefaultInstance);
    }
    private void DontDestroy()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("FirebaseDB");

        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void SetActiveUser(FirebaseUser user) 
    {
        _activeUser = user;
    }

    public void AddToCollection(UserFirebaseDataConstruct user)
    {
        DocumentReference usersDocRef = _db.Collection(FirestoreDataBase.GetNameUserCollection).Document(_activeUser.UserId);
        usersDocRef.SetAsync(user).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the alovelace document in the users collection.");
        });

        DocumentReference purchasedItemsDocRef = _db.Collection(FirestoreDataBase.GetNamePurchasedCollection).Document(_activeUser.UserId);
        Dictionary<string, object> purchasedItem = new Dictionary<string, object> { };
        purchasedItemsDocRef.SetAsync(purchasedItem).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the alovelace document in the users collection.");
        });
    }

    public IEnumerator GetUserGemsFromDB()
    {
        Debug.Log(_db);
        DocumentReference docRef = _db.Collection(USERSCOLLECTION).Document(_activeUser.UserId);
        yield return docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
                _userData = snapshot.ConvertTo<UserFirebaseDataConstruct>();
                UserData = _userData;
                Debug.Log(UserData);
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }
}
