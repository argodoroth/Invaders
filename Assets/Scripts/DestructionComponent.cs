using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        DestructionComponent enemy = other.gameObject.GetComponent<DestructionComponent>();
        if (!enemy) { return; }
        enemy.ProcessHit();
    }

    private void OnDestroy()
    {
        Debug.Log("Destroyed");
    }

    public void ProcessHit()
    {
        Destroy(gameObject);
    }
}
