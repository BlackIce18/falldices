using Firebase.Firestore;

[FirestoreData]

public class ShopItemFirebaseDataConstruct
{
    [FirestoreProperty("id")]
    public int Id { get; set; }
    [FirestoreProperty("name")]
    public string Name { get; set; }
    [FirestoreProperty("price")]
    public int Price { get; set; }
}
