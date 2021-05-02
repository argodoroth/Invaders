using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpLives : PowerUp
{
    public override void Activate()
    {
        PowerUpController powers = GetPowerUpController();
        powers.activateLivesUp();
    }


}
