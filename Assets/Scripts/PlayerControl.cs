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

    float xMin;
    float xMax;
    Coroutine firingCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        //Uses default buttons for axis, makes frame independent by using .deltaTime
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        //clamps positions
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        transform.position = new Vector2(newXPos, yHeight);
    }

    private void SetUpMoveBoundaries()
    {
        //converts from the cameras relative values to game scene actual values
        padding = padding * transform.localScale.x;
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
    }

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
            GameObject laser = Instantiate(
                projectile,
                transform.position,
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            while (laser)
            {
                yield return null;
            }
        }
    }
}
