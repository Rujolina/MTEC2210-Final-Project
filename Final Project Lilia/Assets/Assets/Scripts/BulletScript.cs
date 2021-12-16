using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public bool isPlayerBullet;
    public float speed;
    private GameManager gameManager;
    private float mod;
    private EnemyFormation enemyFormation;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyFormation = GameObject.Find("EnemyFormation").GetComponent<EnemyFormation>();
        if (isPlayerBullet)
        {
            mod = 1;
        }
        else
        {
            mod = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(0, speed * Time.deltaTime * mod, 0);
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var enemy = collision.gameObject.GetComponent<EnemyScript>();
            gameManager.IncreaseScore(enemy.scoreValue);
            enemyFormation.PlayEnemyDeathAudio();
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            enemyFormation.PlayPlayerDeathClip();
            gameManager.RestartGame();

        }

        Destroy(gameObject);
    }
}
