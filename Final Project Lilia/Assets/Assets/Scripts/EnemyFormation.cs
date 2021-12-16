using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : MonoBehaviour
{
    public bool movingDown;
    public bool movingSide;
    public float speed = 2;
    public Vector3 destination;
    private float descendSpeed = 1;
    public GameObject bulletPrefab;
    private float timeTillFire;
    public float fireDelay;

    // Start is called before the first frame update
    void Start()
    {
        movingSide = true;
        timeTillFire = fireDelay;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingSide)
        {

            MoveHorizontal();

        }

        if (movingDown)
        {

            MoveDown();
        }

     
        if (timeTillFire > 0)
        {
            timeTillFire -= Time.deltaTime;
        }
        else
        {
            EnemyShoot();
            timeTillFire = fireDelay;
        }
    }
    public void EnemyShoot()
    {
        int numberofEnemies = GetComponentsInChildren<EnemyScript>().Length;
        int index = Random.Range(0, numberofEnemies);
        var enemyArray = GetComponentsInChildren<EnemyScript>();

        Vector3 bullPos = enemyArray[index].transform.position;
        Instantiate(bulletPrefab, bullPos, Quaternion.identity);
    }
    public void SetDestinationAndMoveDown()
    {
        destination = new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z);
        movingDown = true;
    }

    public void MoveDown()
    {

        transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * descendSpeed);

        if (transform.position == destination)
        {
            movingDown = false;
            speed *= -1;
            movingSide = true;
        }
    }

    public void MoveHorizontal()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}
