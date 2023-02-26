using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEnemy : MonoBehaviour
{    
    public GameObject loot;
    private GameManager _gameManager;
    private Rigidbody2D enemyRb;
    private Animator enemyAnimator;
    private int direction;
    private int horizontal;
    private int vertical;
    public int HP;
    public GameObject explosionPreFab;
    public int earnedPoints;

    [Header("ShotSettings")]   
    public float shotSpeed;
    public Transform enemyGun;
    public GameObject redShotPrefab;
    public int shotChance; // Evita que o inimigo atire sem parar
    public float delayShot; // Evita que o inimigo atire sem parar
    private float tempTimeShot; // Temporizador do tiro

    [Header("Movement Settings")]
    public float enemySpeed;
    public float curveTime; // Quando o temporizador alcança esse valor, o rand é calculado. Aqui, vai se decidir se a nave mantém a trajetória ou faz uma curva;
    public int changeChance; // chance de guinar para a esquerda ou direita depois que a nave decidiu fazer uma curva
    private float tempTime; // temporizador
    private int rand;

    
    void Start()
    {
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager; 

        enemyRb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();

        vertical = 1;
    }

    void Update()
    {
        tempTime += Time.deltaTime; // Temporizador de Movimento começa a contar
        tempTimeShot += Time.deltaTime; //Temporizador de Tiro começa a contar
        if(tempTime >= curveTime)
        {
            tempTime = 0;
            rand = Random.Range(0,100); // 0-99
            if(rand <= changeChance) //Se o valor for menor que a chance de mudança, o objeto muda de direção
            {
                rand = Random.Range(0,100); // Outra rolagem randomica para saber se o inimigo vai guinar para e esquerda ou para a direita
                if(rand < 50) 
                {
                    horizontal = -1; // -1 pq o sprite está invertido (180 graus). 
                    direction = 1;
                }
                else
                {
                    horizontal = 1; // 1 pq o sprite está invertido (180 graus).
                    direction = -1;
                }
            }
            else 
            {
                horizontal = 0;
                direction = 0;
            }
        }

        if(tempTimeShot >= delayShot) // temporizador alcançou o valor do delay entre os tiros
        {
            tempTimeShot = 0;
            rand = Random.Range(0,100);
            if(rand <= shotChance) // Decidindo se vai atirar de novo
            {
                shot();
            }
            
        }
        enemyAnimator.SetInteger("direction", direction);
        enemyRb.velocity = new Vector2(horizontal * enemySpeed, vertical * -enemySpeed);
    }

    void shot()
    {
        GameObject temp = Instantiate(redShotPrefab, enemyGun.transform.position, enemyGun.rotation);
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -1 * shotSpeed);
    }

    void damage(int damageTake)
    {
        HP -= damageTake;
        if(HP <= 0) 
        {
            explosion();
            _gameManager.points += earnedPoints;                                
        }
    }
    void explosion()
    {
        GameObject temp = Instantiate(explosionPreFab, transform.position, transform.rotation);  
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * enemySpeed);
        Destroy(this.gameObject);
    }
    void SpawnLoot()
        {
            //int idItem = 0;
            int rand = Random.Range(0, 100);
            if(rand < 70)
            {
                /*rand = Random.Range(0, 100);
                if(rand > 85)
                {
                    idItem = 2;
                    
                }
                else if(rand > 50 )
                {
                    idItem = 1;
                }
                else
                {
                    idItem = 0;
                }*/
                    GameObject temp =Instantiate(loot, transform.position, transform.localRotation);
                    temp.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -0.5f * enemySpeed);
                    
            }            
        }
    void OnTriggerEnter2D(Collider2D col) 
    {
        switch(col.gameObject.tag) 
        {
            case "PlayerShot":
                damage(1);
                Destroy(col.gameObject);
                SpawnLoot();
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        switch(other.gameObject.tag)
        {
          case "Player":
            explosion();
            break;
        }        
    }
}
