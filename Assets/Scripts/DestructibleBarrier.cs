using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBarrier : MonoBehaviour
{
    [SerializeField] Sprite[] damagedSprites;
    private int damageLevel = 0;
    public void Damage()
    {
        damageLevel += 1;
        if (damageLevel == damagedSprites.Length)
        {
            gameObject.GetComponent<DestructionComponent>().Die();
        } else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = damagedSprites[damageLevel];
        }
    }
}
