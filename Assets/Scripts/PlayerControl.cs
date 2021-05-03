using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] float padding = 0.6f;
    [SerializeField] float yHeight = -4f;

    [SerializeField] GameObject projectile;
    [SerializeField] public float projectileSpeed = 10;

    private bool canFire = true;
    float xMin;
    float xMax;
    Coroutine firingCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    private void OnEnable()
    {
        Projectile.OnProjectileDeath += ChangeFire;
    }
    private void OnDisable()
    {
        Projectile.OnProjectileDeath += ChangeFire;
    }

    private void ChangeFire(bool isPlayer)
    {
        if (isPlayer)
        {
            canFire = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    //Moves the player
    private void Move()
    {
        //Uses default buttons for axis, makes frame independent by using .deltaTime
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        //clamps positions
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        transform.position = new Vector2(newXPos, yHeight);
    }

    //Prevents the player from moving offscreen
    private void SetUpMoveBoundaries()
    {
        //converts from the cameras relative values to game scene actual values
        padding = padding * transform.localScale.x;
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
    }

    //Will repeatedly fire while space button is held down, and stop when lifted
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(RepeatFire());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator RepeatFire()
    {
        while (true)
        {
            if (canFire)
            {
                GameObject laser = Instantiate(
                    projectile,
                    transform.position,
                    Quaternion.identity) as GameObject;
                laser.GetComponent<Projectile>().PlayerFired();
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                canFire = false;
            }
            while (!canFire)
            {
                yield return null;
            }
        }
    }
}
