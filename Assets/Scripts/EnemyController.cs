using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float padding;
    [SerializeField] float moveSpeedIncrease;
    
    public enum Direction { LEFT, RIGHT}
    private Direction dir;
    private float xMin;
    private float xMax;
    public bool atEdge;
    // Start is called before the first frame update
    void Start()
    {
        dir = Direction.LEFT;
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    { 
    }

    public void Move()
    {
        if (dir == Direction.LEFT)
        {
            transform.position -= new Vector3(moveSpeed*Time.deltaTime, 0, 0);
        } else if (dir == Direction.RIGHT)
        {
            transform.position += new Vector3(moveSpeed*Time.deltaTime, 0, 0);
        }
        if (transform.position.x <= xMin || transform.position.x >= xMax)
        {
            atEdge = true;
        }
    }

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
        atEdge = false;
    }


    public Direction GetDirection()
    {
        return dir;
    }
}
