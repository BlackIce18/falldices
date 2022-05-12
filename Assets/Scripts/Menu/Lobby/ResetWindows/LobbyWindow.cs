using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICanResetObjects
{
    void ResetObjects();
}

public abstract class LobbyWindow : MonoBehaviour, ICanResetObjects
{
    public virtual void ResetObjects()
    {
        throw new System.NotImplementedException();
    }
}
