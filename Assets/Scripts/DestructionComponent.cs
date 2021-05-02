using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionComponent : MonoBehaviour
{
    private LivesController lives = null;
    private DestructibleBarrier barrier = null;
    private PowerUp powerUp = null;
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        DestructionComponent enemy = other.gameObject.GetComponent<DestructionComponent>();
        if (!enemy) { return; }
        enemy.ProcessHit();
    }

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

    public void Die()
    {
        Destroy(gameObject);
    }

}
