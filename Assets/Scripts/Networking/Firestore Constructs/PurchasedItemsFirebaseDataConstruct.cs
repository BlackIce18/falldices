using Firebase.Firestore;

[FirestoreData]
public class PurchasedItemsFirebaseDataConstruct
{
    [FirestoreProperty]
    public int Item { get; set; }
}
