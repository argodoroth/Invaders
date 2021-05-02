﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float padding;
    [SerializeField] float moveSpeedIncrease;

    [Header("Enemy Projectiles")]
    [SerializeField] GameObject projectile;
    [SerializeField] int projectileSpeed = -10;

    [Header("Other")]
    [SerializeField] int score = 50;

    public enum Direction { LEFT, RIGHT}
    private Direction dir;
    private float xMin;
    private float xMax;
    public bool atEdge;
    private ScoreController scoreControl;
    // Start is called before the first frame update
    void Start()
    {
        dir = Direction.LEFT;
        SetUpMoveBoundaries();
        scoreControl = FindObjectOfType<ScoreController>();
    }

    //Will
    public void Move()
    {
        if (dir == Direction.LEFT)
        {
            transform.position -= new Vector3(moveSpeed*Time.deltaTime, 0, 0);
        } else if (dir == Direction.RIGHT)
        {
            transform.position += new Vector3(moveSpeed*Time.deltaTime, 0, 0);
        }
        if (transform.position.x <= xMin || transform.position.x >= xMax && !atEdge)
        {
            atEdge = true;
        }
    }

    //Restricts enemy movement to within the screens boundaries
    private void SetUpMoveBoundaries()
    {
        //converts from the cameras relative values to game scene actual values
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
    }

    public void ChangeDirection()
    {
        if (dir == Direction.LEFT)
        {
            dir = Direction.RIGHT;
        } else
        {
            dir = Direction.LEFT;
        }
        moveSpeed += moveSpeedIncrease;
        //moves away from edge and sets at edge to false
        Move();
        atEdge = false;
    }


    public Direction GetDirection()
    {
        return dir;
    }

    public void Fire()
    {
        GameObject laser = Instantiate(
                projectile,
                transform.position,
                Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
    }

    public void OnDestroy()
    {
        scoreControl.IncreaseScore(score);
    }
}
