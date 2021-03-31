using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class BallController : MonoBehaviour
{
    [System.NonSerialized]
    public SpawnBalls spawnBalls;

    private Rigidbody2D _rb;

    private Vector2 _startVector,
                    _directionVector = Vector2.zero;

    private float _distance;

    public bool isMowing;
    public Vector3 vectorMoving;

    void Start()
    {
        isMowing = false;
        _rb = GetComponent<Rigidbody2D>();
    
        spawnBalls.gameController.SpriteLine.SetActive(false);
    }

    void Update()
    {
        if (spawnBalls.gameController.optionsGame == OptionsGame.BallInPlace)
        {
            _startVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (_startVector.y < transform.position.y + .5f && _startVector.y > -3.5f)
            {
                _startVector.y = transform.position.y + .5f;
                spawnBalls.gameController._isAiming = true;
            }
            if (_startVector.y < -3.5f || _startVector.y > 3.5f)
            {
                spawnBalls.gameController.SpriteLine.SetActive(false); 
                spawnBalls.gameController.SpriteLine.transform.position = transform.position;
                spawnBalls.gameController._isAiming = false;
            }

            if (Input.GetKey(KeyCode.Mouse0) && spawnBalls.gameController._isAiming)
            {
                spawnBalls.gameController.SpriteLine.SetActive(true);

                spawnBalls.gameController.SpriteLine.transform.position = transform.position;

                Vector2 difference = _startVector - (Vector2)spawnBalls.gameController.SpriteLine.transform.position;
                difference.Normalize();
                float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                spawnBalls.gameController.SpriteLine.transform.rotation = Quaternion.Euler(0, 0, rotation_z - 90);
            }

            if (Input.GetKeyUp(KeyCode.Mouse0) && spawnBalls.gameController._isAiming)
            {
                spawnBalls.gameController.SpriteLine.SetActive(false);

                spawnBalls.gameController.SpriteLine.transform.position = transform.position;

                StartCoroutine(spawnBalls.gameController.ThrowingBall());
                spawnBalls.gameController.optionsGame = OptionsGame.BallFlying;
            }
        }

    }

    private void FixedUpdate()
    {
        if(isMowing)
            Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "bottomBorder")
        {
            _rb.velocity = Vector2.zero;

            if (spawnBalls.gameController.tempListBall.Count == 0)
            {
                transform.position = new Vector2(transform.position.x, spawnBalls.gameController.startPositionBall.y);                
            }

            else
            {
                vectorMoving = spawnBalls.gameController.tempListBall[0].transform.position;
                isMowing = true;
            }

            spawnBalls.gameController.tempListBall.Add(gameObject);

        }
    }

    public void Send()
    {
        if (spawnBalls.gameController._isAiming)
        {
            spawnBalls.gameController.countBalls--;
            _directionVector = (_startVector - (Vector2)transform.position).normalized;
            _rb.AddForce(_directionVector * spawnBalls.gameController.speedBalls);
        }
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, vectorMoving, .3f);
        if(transform.position == vectorMoving)
        {
            isMowing = false;
        }
    }

    public void StopSend()
    {
        StopAllCoroutines();
    }
}
