using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls all enemies in the scene
public class EnemyGroupController : MonoBehaviour
{
    [SerializeField] float downMoveSpeed;
    [SerializeField] float downDistance;
    [SerializeField] float minTimeBetweenShots;
    [SerializeField] float maxTimeBetweenShots;

    private List<EnemyController> enemies;
    private bool movingDown = false;
    private float downPos = 0;
    private float shotCounter;
    private int nextShooter;
    private GameSession session;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<EnemyController>(GetComponentsInChildren<EnemyController>());
        session = FindObjectOfType<GameSession>();
        //sets time between shots and next shooter
        AdjustFireSpeed();
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        nextShooter = UnityEngine.Random.Range(0, enemies.Count -1);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0)
        {
            MoveGroup();
            CountDownAndShoot();
        } else
        {
            //when all enemies defeated will advance scenes
            session.LoadMenu(MenuState.CONTINUELEVEL);
        }
    }

    //Moves all of the enemies as a group
    private void MoveGroup()
    {
        if (!movingDown) {
            //Checks if any enemies have reached the edge of the screen before trying to move the group
            for(int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i])
                {
                    enemies.RemoveAt(i);
                }
                if (enemies[i].atEdge)
                {
                    movingDown = true;
                    downPos = transform.position.y - downDistance;
                    return;
                }
            }
            //Moves each enemy
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Move();
            }
        } else
        {
            //Will keep moving the parent object downwards until reaches a point
            if (transform.position.y > downPos)
            {
                MoveDown(downPos);
            } else
            {
                Debug.Log("Finished Moving down");
                movingDown = false;
                //when all units moved downwards, will change direction
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (!enemies[i])
                    {
                        enemies.RemoveAt(i);
                    }
                    enemies[i].ChangeDirection();
                }
            }
        }
    }

    //Will move towards specified down point
    private void MoveDown(float finalY)
    {
        Vector3 finalVector = new Vector3(this.transform.position.x, finalY, this.transform.position.z);
        transform.position = Vector3.MoveTowards(this.transform.position, finalVector, downMoveSpeed * Time.deltaTime);
    }

    //Waits for a random amount of time before telling next shooter to shoot and resetting
    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;  //reduces countdown by time it took to execute frame
        if (shotCounter <= 0f)
        {
            //checks to see if next shooter has been killed
            if (!enemies[nextShooter])
            {
                nextShooter = UnityEngine.Random.Range(0, enemies.Count - 1);
                enemies[nextShooter].Fire();
            } else
            {
                enemies[nextShooter].Fire();
            }
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            nextShooter = UnityEngine.Random.Range(0, enemies.Count - 1);
        }
    }

    //increases fire speed each level
    private void AdjustFireSpeed()
    {
        int x = session.GetLevel();
        minTimeBetweenShots = Mathf.Max(minTimeBetweenShots - (x * 0.05f), 0.1f);
        maxTimeBetweenShots = Mathf.Max(maxTimeBetweenShots - (x * 0.05f), 0.3f);
    }
}
