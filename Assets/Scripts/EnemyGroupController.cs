using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupController : MonoBehaviour
{
    [SerializeField] float downMoveSpeed;

    private List<EnemyController> enemies;
    private bool movingDown = false;
    private float downPos = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<EnemyController>(GetComponentsInChildren<EnemyController>());       
    }

    // Update is called once per frame
    void Update()
    {
        MoveGroup();
    }

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
                    downPos = transform.position.y - 1;
                    return;
                }
            }
            foreach(EnemyController enemy in enemies)
            {
                enemy.Move();
            }
        } else
        {
            if (transform.position.y > downPos)
            {
                MoveDown(downPos);
            } else
            {
                Debug.Log("Finished Moving down");
                movingDown = false;
                foreach(EnemyController enemy in enemies)
                {
                    enemy.ChangeDirection();
                }
            }
        }
    }
    private void MoveDown(float finalY)
    {
        Vector3 finalVector = new Vector3(this.transform.position.x, finalY, this.transform.position.z);
        transform.position = Vector3.MoveTowards(this.transform.position, finalVector, downMoveSpeed * Time.deltaTime);
    }
}
