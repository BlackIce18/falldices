using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;

public class AuthManager : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject _authorization;

    [Header("Firebase")]
    [SerializeField] private FirebaseAuth _auth;
    public FirebaseAuth AuthSetter { set => _auth = value; }
    [SerializeField] private FirebaseUser _user;
    public FirebaseUser GetAuthentificatedUser => _user;


    [Header("Login")]
    [SerializeField] private GameObject _gameMenu;
    [SerializeField] private GameObject _loginMenu;
    [SerializeField] private TMP_InputField _emailLogin;
    [SerializeField] private TMP_InputField _passwordLogin;
    [SerializeField] private TextMeshProUGUI _warningTextLogin;

    [Header("Register")]
    [SerializeField] private GameObject _registerMenu;
    [SerializeField] private TMP_InputField _emailRegister;
    [SerializeField] private TMP_InputField _passwordRegister;
    [SerializeField] private TMP_InputField _usernameRegister;
    [SerializeField] private TextMeshProUGUI _warningTextRegister;

    [SerializeField] private FirestoreDataBase _db;
    public string GetUserId() { 
        return _user.UserId; 
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(_emailLogin.text, _passwordLogin.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(_emailRegister.text, _passwordRegister.text, _usernameRegister.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        if (_email == "")
        {
            //If the username field is blank show a warning
            _warningTextLogin.text = "�������� email";
            Debug.Log("Email is empty");
        }
        else if (_password == "")
        {
            //If the username field is blank show a warning
            _warningTextLogin.text = "�������� ������";
            Debug.Log("Password is empty");
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var LoginTask = _auth.SignInWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

            if (LoginTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
                FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Login Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "�������� Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "�������� ������";
                        break;
                    case AuthError.WrongPassword:
                        message = "�� ������ ������";
                        break;
                    case AuthError.InvalidEmail:
                        message = "�� ������ Email";
                        break;
                    case AuthError.UserNotFound:
                        message = "������� �� ����������";
                        break;
                }
                _warningTextLogin.text = message;
            }
          /*  else if (LoginTask.Result.IsEmailVerified == false)
            {
                Debug.Log("The user has an unconfirmed email");
                _warningTextLogin.text = "�� �������������� email. ������ � ���������� ������� �� �����.";
                ConfirmEmail();
                _auth.SignOut();
            }*/
            else
            {
                //User is now logged in
                //Now get the result
                _user = LoginTask.Result;
                _db.OnUserLogin(_user);
                Debug.LogFormat("User signed in successfully: {0} ({1})", _user.DisplayName, _user.Email);
                _warningTextLogin.text = "";
                Debug.Log(_user.UserId);
                _gameMenu.SetActive(true);
                _authorization.SetActive(false);
                //confirmLoginText.text = "Logged In";
            }
        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_email == "")
        {
            //If the username field is blank show a warning
            _warningTextRegister.text = "�������� email";
        }
        if (_username == "")
        {
            //If the username field is blank show a warning
            _warningTextRegister.text = "��������� ������� ���";
        }
        else if (_password == "")
        {
            //If the username field is blank show a warning
            _warningTextRegister.text = "�������� ������";
        }
        else
        {
            Debug.Log(_auth);
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = _auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "����������� �� �������";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "�������� Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "�������� ������";
                        break;
                    case AuthError.WeakPassword:
                        message = "������ ������";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email ��� ������������";
                        break;
                }
                _warningTextRegister.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                _user = RegisterTask.Result;
                ConfirmEmail();

                if (_user != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };
                    
                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = _user.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        _warningTextRegister.text = "Username �� ����������!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        _db.AddToCollection(InitUser());

                        _loginMenu.SetActive(true);
                        _registerMenu.SetActive(false);
                        _warningTextRegister.text = "";
                    }
                }
            }
        }
    }

    private UserFirebaseDataConstruct InitUser()
    {
        UserFirebaseDataConstruct user = new UserFirebaseDataConstruct();
        user.Money = 0;
        user.Nickname = _usernameRegister.text;
        return user;
    }

    private void ConfirmEmail()
    {/*
        if (_user != null)
        {
            _user.SendEmailVerificationAsync().ContinueWith(t =>
            {
                if (t.IsCanceled)
                {
                    Debug.LogError("SendEmailVerificationAsync was canceled.");
                }
                if (t.IsFaulted)
                {
                    Debug.LogError("SendEmailVerificationAsync encountered an error: " + t.Exception);
                }

                Debug.Log("Email sent successfully.");
            });
        }*/
    }
}
