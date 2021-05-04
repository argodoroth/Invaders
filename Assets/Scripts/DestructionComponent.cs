using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages all objects that can be destroyed
public class DestructionComponent : MonoBehaviour
{
    //used to establish object type
    private LivesController lives = null;
    private DestructibleBarrier barrier = null;
    private PowerUp powerUp = null;

    //object type established
    private void Start()
    {
        if (gameObject.GetComponent<PlayerControl>())
        {
            lives = FindObjectOfType<LivesController>();
        } else if (gameObject.GetComponent<DestructibleBarrier>())
        {
            barrier = gameObject.GetComponent<DestructibleBarrier>();
        } else if (gameObject.GetComponent<PowerUp>())
        {
            powerUp = gameObject.GetComponent<PowerUp>();
        }
    }

    //destroy other entity when collided
    private void OnTriggerEnter2D(Collider2D other)
    {
        DestructionComponent enemy = other.gameObject.GetComponent<DestructionComponent>();
        if (!enemy) { return; }
        enemy.ProcessHit();
    }

    //Reacts differently to different objects
    public void ProcessHit()
    {
        if (gameObject.GetComponent<PlayerControl>())
        {
            lives.DecreaseLives(1);
        } else if (gameObject.GetComponent<DestructibleBarrier>())
        {
            barrier.Damage();
        } else if (gameObject.GetComponent<PowerUp>())
        {
            powerUp.Activate();
            Die();
        } else
        {
            Die();
        }
    }

    //Destroys game object
    public void Die()
    {
        Destroy(gameObject);
    }

}
