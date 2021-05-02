using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovement : PowerUp
{
    public override void Activate()
    {
        //Calls from controller class so coroutine continues to run after death
        PowerUpController powers = GetPowerUpController();
        powers.activateSpeedStop();
    }
}
