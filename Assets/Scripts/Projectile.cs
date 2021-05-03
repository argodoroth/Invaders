using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool isPlayerFired = false;
    public static event Action<bool> OnProjectileDeath;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnDestroy()
    {
        OnProjectileDeath?.Invoke(true);
    }

    public void PlayerFired()
    {
        isPlayerFired = true;
    }
}
