using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract class for other powerups to inherit from
public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    //Each child will have different activate function
    public abstract void Activate();

    //Moves powerup down the screen
    protected void Drop()
    {
        transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    protected PowerUpController GetPowerUpController()
    {
        return FindObjectOfType<PowerUpController>();
    }

    protected void Update()
    {
        Drop();
    }
}
