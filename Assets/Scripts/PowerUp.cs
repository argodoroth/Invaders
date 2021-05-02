using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public abstract void Activate();

    protected void Drop()
    {
        transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    protected PowerUpController GetPowerUpController()
    {
        return FindObjectOfType<PowerUpController>();
    }
}
