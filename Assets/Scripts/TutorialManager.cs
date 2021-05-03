using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialState { START, HASMOVED, HASFIRED, BARRIERDESTROYED, FIREUP,LIFEUP,SPEEDUP,SPEEDSTOP, ENEMYDESTROYED, END}
public class TutorialManager : MonoBehaviour
{
    [SerializeField] Canvas[] stages;
    [SerializeField] GameObject[] powerUps;
    [SerializeField] Vector3 powerUpSpawnPoint;

    private DestructibleBarrier[] barriers;
    private EnemyController[] enemies;
    private EnemyGroupController enemyGroup;
    private PlayerControl player;
    
    private TutorialState curStage;
    private int currentCanvas = 0;
    private int totalBarriers;
    private int totalEnemies;
    private float initialSpeed;
    private float initialProjectileSpeed;
    // Start is called before the first frame update
    void Start()
    {
        curStage = TutorialState.START;
        enemies = FindObjectsOfType<EnemyController>();
        barriers = FindObjectsOfType<DestructibleBarrier>();
        enemyGroup = FindObjectOfType<EnemyGroupController>();
        player = FindObjectOfType<PlayerControl>();

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
            case TutorialState.BARRIERDESTROYED:
                SwithToPowerUps();
                break;
            case TutorialState.FIREUP:
                CreateFireUp();
                break;
            case TutorialState.LIFEUP:
                break;
            case TutorialState.SPEEDUP:
                break;
            case TutorialState.SPEEDSTOP:
                break;
            case TutorialState.ENEMYDESTROYED:
                break;
            case TutorialState.END:
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
                curStage = TutorialState.BARRIERDESTROYED;
                StartCoroutine(SwapStages(1));
            }
        }
    }

    private void SwithToPowerUps()
    {
        curStage = TutorialState.FIREUP;
        StartCoroutine(SwapStages(3));
    }

    private void CreateFireUp()
    {
        if (!FindObjectOfType<PowerUp>())
        {
            SpawnPowerup(0);
        }
        //curStage = TutorialState.SPEEDUP;
        //StartCoroutine(SwapStages(7));
    }

    private void SpawnPowerup(int index)
    {
        Debug.Log("spawning");
        GameObject powerUp = Instantiate(
               powerUps[index],
               powerUpSpawnPoint,
               Quaternion.identity) as GameObject;
    }
    private IEnumerator SwapStages(float wait)
    {
        yield return new WaitForSeconds(wait);
        stages[currentCanvas].gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        stages[currentCanvas+1].gameObject.SetActive(true);
        currentCanvas += 1;
    }

    private IEnumerator WaitActivate(Component[] comps, float wait)
    {
        yield return new WaitForSeconds(wait);
        ActivateObjects(comps);
    }
}
