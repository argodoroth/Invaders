using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpMoveSpeed : PowerUp
{
    [SerializeField] float moveSpeedIncrease = 20f;
    public override void Activate()
    {
        //Calls from controller class so coroutine continues to run after death
        PowerUpController powers = GetPowerUpController();
        powers.activateSpeedUp(moveSpeedIncrease);
    }

    private void Update()
    {
        Drop();
    }
}
