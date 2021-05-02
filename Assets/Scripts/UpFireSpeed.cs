using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpFireSpeed : PowerUp
{
    [SerializeField] float fireSpeedIncrease = 10f;
    public override void Activate()
    {
        //Calls from controller class so coroutine continues to run after death
        PowerUpController powers = GetPowerUpController();
        powers.activateFireUp(fireSpeedIncrease);
    }
}
