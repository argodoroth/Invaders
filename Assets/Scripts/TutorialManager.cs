using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialState { START, HASMOVED, HASFIRED, FIREUP,LIFEUP,SPEEDUP,SPEEDSTOP, ENEMYSPAWN, END}
public class TutorialManager : MonoBehaviour
{
    [SerializeField] Canvas[] stages;
    [SerializeField] GameObject[] powerUps;
    [SerializeField] Vector3 powerUpSpawnPoint;

    private DestructibleBarrier[] barriers;
    private EnemyController[] enemies;
    private EnemyGroupController enemyGroup;
    private PlayerControl player;
    private LivesController lives;
    private SceneLoader sceneLoader;

    private TutorialState curStage;
    private int currentCanvas = 0;
    private int totalBarriers;
    private int totalEnemies;
    private float initialSpeed;
    private float initialProjectileSpeed;
    private float initialLives;
    private bool usedPowerup = true;
    // Start is called before the first frame update
    void Start()
    {
        curStage = TutorialState.START;
        enemies = FindObjectsOfType<EnemyController>();
        barriers = FindObjectsOfType<DestructibleBarrier>();
        enemyGroup = FindObjectOfType<EnemyGroupController>();
        player = FindObjectOfType<PlayerControl>();
        lives = FindObjectOfType<LivesController>();
        sceneLoader = FindObjectOfType<SceneLoader>();


        initialLives = lives.GetLives();
        initialSpeed = player.moveSpeed;
        initialProjectileSpeed = player.projectileSpeed;
        totalEnemies = enemies.Length;
        totalBarriers = barriers.Length;

        DeactivateObjects();
        DeactivateStages();
    }
    // Update is called once per frame
    void Update()
    {
        AdvanceState();
    }

    private void DeactivateObjects()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < barriers.Length; i++)
        {
            barriers[i].gameObject.SetActive(false);
        }
        enemyGroup.gameObject.SetActive(false);
    }

    private void DeactivateStages()
    {
        for (int i = 1; i < stages.Length; i++)
        {
            stages[i].gameObject.SetActive(false);
        }
    }

    private void ActivateObjects(Component[] newActive)
    {
        for (int i = 0; i < newActive.Length; i++)
        {
            Debug.Log("activate:" + i);
            newActive[i].gameObject.SetActive(true);
        }
    }

    private void AdvanceState()
    {
        switch (curStage)
        {
            case TutorialState.START:
                SwitchAfterMovement();
                break;
            case TutorialState.HASMOVED:
                SwitchAfterFire();
                break;
            case TutorialState.HASFIRED:
                SwitchAfterBarrierDestroyed();
                break;
            case TutorialState.FIREUP:
                CreateFireUp();
                break;
            case TutorialState.LIFEUP:
                CreateLifeUp();
                break;
            case TutorialState.SPEEDUP:
                CreateSpeedUp();
                break;
            case TutorialState.SPEEDSTOP:
                CreateSpeedStop();
                break;
            case TutorialState.ENEMYSPAWN:
                WaitForEnemyKilled();
                break;
            case TutorialState.END:
                sceneLoader.LoadMenu(MenuState.MAINMENU);
                break;
        }
    }

    private void SwitchAfterMovement()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            curStage = TutorialState.HASMOVED;
            StartCoroutine(SwapStages(1));
        }
    }

    private void SwitchAfterFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            curStage = TutorialState.HASFIRED;
            StartCoroutine(SwapStages(1));
            StartCoroutine(WaitActivate(barriers, 1.5f));
        }
    }

    private void SwitchAfterBarrierDestroyed()
    {
        for (int i = 0; i < barriers.Length; i++)
        {
            if (!barriers[i])
            {
                curStage = TutorialState.FIREUP;
                usedPowerup = true;
                StartCoroutine(SwapStages(1f));
            }
        }
    }

    private void CreateFireUp()
    {
        if (!FindObjectOfType<PowerUp>() && !usedPowerup)
        {
            SpawnPowerup(0);
        }
        if (player.projectileSpeed > initialProjectileSpeed)
        {
            curStage = TutorialState.LIFEUP;
            usedPowerup = true;
            StartCoroutine(SwapStages(4f));
        }
    }

    private void CreateLifeUp()
    {
        if (!FindObjectOfType<PowerUp>() && !usedPowerup)
        {
            SpawnPowerup(1);
        }
        if (lives.GetLives() > initialLives)
        {
            usedPowerup = true;
            curStage = TutorialState.SPEEDUP;
            StartCoroutine(SwapStages(4f));
        }
    }

    private void CreateSpeedUp()
    {
        if (!FindObjectOfType<PowerUp>() && !usedPowerup)
        {
            SpawnPowerup(2);
        }
        if (player.moveSpeed > initialSpeed)
        {
            usedPowerup = true;
            curStage = TutorialState.SPEEDSTOP;
            StartCoroutine(SwapStages(4f));
        }
    }

    private void CreateSpeedStop()
    {
        if (!FindObjectOfType<PowerUp>() && !usedPowerup)
        {
            SpawnPowerup(3);
        }
        if (player.moveSpeed < initialSpeed)
        {
            usedPowerup = true;
            curStage = TutorialState.ENEMYSPAWN;
            StartCoroutine(SwapStages(4f));
            StartCoroutine(WaitActivateEnemies(4f));
        }
    }

    private void SpawnPowerup(int index)
    {
        GameObject powerUp = Instantiate(
               powerUps[index],
               powerUpSpawnPoint,
               Quaternion.identity) as GameObject;
    }

    private void WaitForEnemyKilled()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i])
            {
                curStage = TutorialState.END;
                StartCoroutine(SwapStages(1f));
            }
        }
    }
    private IEnumerator SwapStages(float wait)
    {
        yield return new WaitForSeconds(wait);
        stages[currentCanvas].gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        stages[currentCanvas+1].gameObject.SetActive(true);
        currentCanvas += 1;
        usedPowerup = false;
    }

    private IEnumerator WaitActivate(Component[] comps, float wait)
    {
        yield return new WaitForSeconds(wait);
        ActivateObjects(comps);
    }

    private IEnumerator WaitActivateEnemies(float wait)
    {
        Debug.Log("Activate");
        yield return new WaitForSeconds(wait);
        enemyGroup.gameObject.SetActive(true);
        ActivateObjects(enemies);
    }
}
