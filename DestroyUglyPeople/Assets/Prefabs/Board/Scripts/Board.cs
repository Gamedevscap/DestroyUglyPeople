using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    SFXManager sfxManager;

    [Header("Board Stats")]
    [SerializeField] int boardWidth;
    [SerializeField] int boardHeight;
    [SerializeField] GameObject tilePrefab;
    private BackgroundTile[,] allTiles;
    [SerializeField] float moveLeftDelay = 10f;
    private bool readyToMove;

    [Header ("Board Shoot")]
    [SerializeField] GameObject enemyProjectile;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float shootDelay = 1f;
    private Transform[] spawnPoints;
    private int spawnPointIndex;
    private bool readyToShoot;


    // Use this for initialization
    void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();

        Random.InitState(System.DateTime.Now.Millisecond);
        allTiles = new BackgroundTile[boardWidth, boardHeight];
        SetUp();
        spawnPoints = GetComponentsInChildren<Transform>();
        readyToShoot = true;
        readyToMove = true;
    }

    private void Update()
    {
        if (readyToShoot)
        {
            spawnPoints = GetComponentsInChildren<Transform>();
            spawnPointIndex = Random.Range(1, spawnPoints.Length);
            StartCoroutine(Shoot());
        }

        else if (readyToMove)
        {
            StartCoroutine(MoveLeft());
        }

        else if(spawnPoints.Length == 1)
        {
            FindObjectOfType<GameSession>().PlayerWin();
        }
    }

    void SetUp()
    {
        for (int i = 0; i < boardWidth; i++)
        {
            for (int j = 0; j < boardHeight; j++)
            {
                Vector2 tempPos = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPos, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";
                FindObjectOfType<GameSession>().CountEnemies();
            }
        }
    }

    IEnumerator Shoot()
    {
        readyToShoot = false;
        var tempShootPos = spawnPoints[spawnPointIndex];
        if(spawnPointIndex == 0)
        {
            yield return null;
        }
        else
        {
            GameObject beam = Instantiate(enemyProjectile, tempShootPos.position, Quaternion.identity) as GameObject;
            beam.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed, 0);
            sfxManager.PlayEnemyShootSFX();
            yield return new WaitForSeconds(shootDelay);
            readyToShoot = true;
        }
    }

    IEnumerator MoveLeft()
    {
        readyToMove = false;
        yield return new WaitForSeconds(moveLeftDelay);
        float xIncrement = 1f;
        GetComponentInChildren<Transform>().transform.position = new Vector2(transform.position.x - xIncrement, transform.position.y);
        readyToMove = true;
    }
}