using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float movespeed;
    [SerializeField] float padding;
    
    private enum Direction { LEFT, RIGHT , DOWN}
    private Direction dir;
    private float xMin;
    private float xMax;
    // Start is called before the first frame update
    void Start()
    {
        dir = Direction.LEFT;
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (dir == Direction.LEFT)
        {
            transform.position -= new Vector3(movespeed, 0, 0);
        } else if (dir == Direction.RIGHT)
        {
            transform.position += new Vector3(movespeed, 0, 0);
        }
        if (transform.position.x <= xMin || transform.position.x >= xMax)
        {
            ChangeDirection();
            Debug.Log("Change Dir");
        }
    }

    private void SetUpMoveBoundaries()
    {
        //converts from the cameras relative values to game scene actual values
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
    }

    private void ChangeDirection()
    {
        transform.position -= new Vector3(0, 0.5f, 0);
        movespeed += 0.0005f;
        if (dir == Direction.LEFT)
        {
            dir = Direction.RIGHT;
        } else
        {
            dir = Direction.LEFT;
        }
    }

}
