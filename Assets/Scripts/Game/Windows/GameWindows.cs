using System;
using System.Collections.Generic;
using UnityEngine;
public interface IShowWindow
{
    void Show();
}
public abstract class Window : MonoBehaviour, IShowWindow
{
    public abstract void Show();
}
[Serializable]
public enum WindowsEnum
{
    NewCircle = 0,
    BankRobbery = 1,
    Chance = 2
}
[Serializable]
public struct WindowsNameStruct {
    public WindowsEnum name;
    public Window window;
}
public class GameWindows : MonoBehaviour
{
    [SerializeField] private List<WindowsNameStruct> _gameWindows;
    public void ShowWindow(WindowsEnum windowsEnum)
    {
        foreach(WindowsNameStruct windowsStruct in _gameWindows)
        {
            if(windowsStruct.name.Equals(windowsEnum))
            {
                windowsStruct.window.gameObject.SetActive(true);
                windowsStruct.window.Show();
                break;
            }
        }
    }
}

