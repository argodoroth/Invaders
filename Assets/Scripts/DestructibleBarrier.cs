using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to show damage levels of barriers
public class DestructibleBarrier : MonoBehaviour
{
    [SerializeField] Sprite[] damagedSprites;
    private int damageLevel = 0;
    //change sprite based on damage levels
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
