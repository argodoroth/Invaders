using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionComponent : MonoBehaviour
{
    private LivesController lives = null;

    private void Start()
    {
        if (gameObject.GetComponent<PlayerControl>())
        {
            lives = FindObjectOfType<LivesController>();
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
