using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum direcaoMovimento
{
    Down, Right, Left
}
public class Enemy02 : MonoBehaviour
{
    private GameManager _gameManager;
    public direcaoMovimento direcaoMovimento;
    public float enemySpeed;
    public SpriteRenderer naveSr;

    private float pontoInicialCurva;
    private bool isCurva;
    public float grausCurva;
    public float incrementar;
    private float incrementado;
    private float rotacaoZ;
    public GameObject explosionPreFab;
    public int earnedPoints;
    public int HP;

[Header("ShotSettings")]   
    public float shotSpeed;
    public Transform enemyGun;
    public GameObject shotPreFab;
    public int shotChance; // Evita que o inimigo atire sem parar
    public float delayShot; // Evita que o inimigo atire sem parar
    private float tempTimeShot; // Temporizador do tiro
    void Start()
    {
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        rotacaoZ = transform.eulerAngles.z;
        naveSr = gameObject.GetComponent<SpriteRenderer>();

        pontoInicialCurva = Random.Range(-0.2f, 3.3f);
    }
    void Update()
    {
        if (transform.position.y <= pontoInicialCurva && isCurva == false)
        {
            isCurva = true;
        }
        if (isCurva == true && incrementado < grausCurva)
        {
            if(transform.position.x >= 0 )
            {
                rotacaoZ -= incrementar;
            }
            else
            {
                rotacaoZ += incrementar;
            }           
            transform.rotation = Quaternion.Euler(0, 0, rotacaoZ);
            if (incrementar < 0)
            {
                incrementado += (incrementar * -1);
            }
            else
            {
                incrementado += incrementar;
            }
        }
            movimentar();
    }
    void atirar()
    {
        GameObject temp = Instantiate(shotPreFab, enemyGun.position, transform.localRotation);

        temp.GetComponent<Rigidbody2D>().velocity = transform.up * -1 * shotSpeed;
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
    private void OnBecameVisible()
    {
        gameObject.SetActive(true);
        StartCoroutine("controleTiro");
    }
    IEnumerator controleTiro()
    {
        yield return new WaitForSeconds(delayShot);
        atirar();
        StartCoroutine("controleTiro");
    }

    void movimentar()
    {
        switch (direcaoMovimento)
        {
            case direcaoMovimento.Down:
                transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);
                break;
            /*case direcaoMovimento.cima:
            transform.Translate(Vector3.up * velocidadeMovimento * Time.deltaTime);
            GetComponent<Rigidbody2D>().velocity = transform.up * velocidadeMovimento * -1;
            naveSr.flipY = true;
            break;*/
            case direcaoMovimento.Left:
                transform.Translate(Vector3.left * enemySpeed * Time.deltaTime);
                break;
            case direcaoMovimento.Right:
                transform.Translate(Vector3.right * enemySpeed * Time.deltaTime);
                break;
        }
    }
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    void OnTriggerEnter2D(Collider2D col) 
    {
        switch(col.gameObject.tag) 
        {
            case "PlayerShot":
                damage(1);
                Destroy(col.gameObject);
                //SpawnLoot();
                break;
        }
    }
}
