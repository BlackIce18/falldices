using Firebase.Firestore;

[FirestoreData]
public class UserFirebaseDataConstruct
{
    [FirestoreProperty]
    public int Money { get; set; }
    [FirestoreProperty]
    public string Nickname { get; set; }
}
