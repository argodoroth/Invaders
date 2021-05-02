using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    public void activateSpeedUp(float inc)
    {
        StartCoroutine(SpeedUp(inc));
    }
    public IEnumerator SpeedUp(float inc)
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        SpriteRenderer playerRender = player.GetComponent<SpriteRenderer>();
        float initialMovespeed = player.moveSpeed;
        Color initalColor = playerRender.color;
        player.moveSpeed = inc;
        playerRender.color = new Color(0, 0.5f, 0);
        yield return new WaitForSeconds(5f);
        player.moveSpeed = initialMovespeed;
        playerRender.color = initalColor;
    }
}
