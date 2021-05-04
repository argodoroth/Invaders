using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tells player if a fired projectile has been destroyed
public class Projectile : MonoBehaviour
{
    private bool isPlayerFired = false;
    public static event Action<bool> OnProjectileDeath;

    //Alerts player when projectile has been destroyed
    private void OnDestroy()
    {
        OnProjectileDeath?.Invoke(isPlayerFired);
    }

    public void PlayerFired()
    {
        isPlayerFired = true;
    }
}
