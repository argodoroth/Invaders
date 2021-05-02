using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    [SerializeField] float minTimeBetweenSpawns;
    [SerializeField] float maxTimeBetweenSpawns;
    [SerializeField] GameObject[] powerUps;
    [SerializeField] float spawnHeight;

    private SpriteRenderer playerRender;
    private PlayerControl player;
    private Color initalPlayerColor;
    private float spawnCounter;
    private float initialPlayerMoveSpeed;
    private float initialPlayerFireSpeed;

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        playerRender = player.GetComponent<SpriteRenderer>();
        spawnCounter = UnityEngine.Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        initalPlayerColor = playerRender.color;
        initialPlayerMoveSpeed = player.moveSpeed;
        initialPlayerFireSpeed = player.projectileSpeed;
    }



    void Update()
    {
        CountDownAndSpawn();
    }

    private void CountDownAndSpawn()
    {
        spawnCounter -= Time.deltaTime;  //reduces countdown by time it took to execute frame
        if (spawnCounter <= 0f)
        {
            SpawnRandomPowerUp();
            spawnCounter = UnityEngine.Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        }
    }
    private void SpawnRandomPowerUp()
    {
        int rand = UnityEngine.Random.Range(0, powerUps.Length);

        Camera gameCamera = Camera.main;
        float xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + 1;
        float xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 1;
        float randXPos = UnityEngine.Random.Range(xMin, xMax);
        Vector3 spawnPos = new Vector3(randXPos, spawnHeight, 0);
        GameObject powerUp = Instantiate(
                powerUps[rand],
                spawnPos,
                Quaternion.identity) as GameObject;
    }


    //Various Coroutines for activating different powerup abilities
    public void activateSpeedUp(float inc)
    {
        StartCoroutine(SpeedUp(inc));
    }

    public void activateSpeedStop()
    {
        StartCoroutine(SpeedStop());
    }

    public void activateFireUp(float inc)
    {
        StartCoroutine(FireUp(inc));
    }

    public void activateLivesUp()
    {
        StartCoroutine(LivesUp());
    }

    private IEnumerator SpeedUp(float inc)
    {
        player.moveSpeed += inc;
        playerRender.color = new Color(0, 1f, 0);
        yield return new WaitForSeconds(5f);
        player.moveSpeed = initialPlayerMoveSpeed;
        playerRender.color = initalPlayerColor;
    }

    private IEnumerator FireUp(float inc)
    {
        player.projectileSpeed += inc;
        playerRender.color = new Color(1f, 1f, 0);
        yield return new WaitForSeconds(5f);
        player.projectileSpeed = initialPlayerFireSpeed;
        playerRender.color = initalPlayerColor;
    }

    private IEnumerator LivesUp()
    {
        LivesController lives = FindObjectOfType<LivesController>();
        lives.IncreaseLives(1);
        playerRender.color = new Color(1f, 0, 1f);
        yield return new WaitForSeconds(1.5f);
        playerRender.color = initalPlayerColor;
    }

    private IEnumerator SpeedStop()
    {
        player.moveSpeed = 0;
        playerRender.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(4f);
        player.moveSpeed = initialPlayerMoveSpeed;
        playerRender.color = initalPlayerColor;
    }
}
