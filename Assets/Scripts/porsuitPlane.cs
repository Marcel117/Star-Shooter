using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class porsuitPlane : MonoBehaviour
{
    private GameManager _gameManager;
    private PlayerController _playerController;
    private int vidaExtra;
    public float speed;
    public GameObject explosionPreFab;
    public float HP;
    public int earnedPoints;

    [Header("ShotSettings")]   
    public float shotSpeed;
    public Transform enemyGun1;
    public Transform enemyGun2;
    public Transform enemyGun3;
    public Transform enemyGun4;
    public GameObject shotPreFab;
    public int shotChance; // Evita que o inimigo atire sem parar
    public float delayShot; // Evita que o inimigo atire sem parar
    private float tempTimeShot; // Temporizador do tiro
    void Start()
    {
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;

        vidaExtra = 2;
    }
    void Update()
    {
        tempTimeShot += Time.deltaTime;
        Movimentar();
    }
    private void OnBecameVisible()
    {
        StartCoroutine("controleTiro");
    }

    void Movimentar()
    {
            transform.up = _playerController.transform.position - transform.position;
            transform.Translate(Vector3.up * speed * Time.deltaTime);
       
    }
    IEnumerator controleTiro()
    {
        yield return new WaitForSeconds(delayShot);
        Shot();
        StartCoroutine("controleTiro");
    }
    void Shot()
    {   if(tempTimeShot >= delayShot) // temporizador alcançou o valor do delay entre os tiros
        {
            tempTimeShot = 0;
            int rand = Random.Range(0,100);
            if(rand <= shotChance) // Decidindo se vai atirar de novo
            {
            GameObject temp = Instantiate(shotPreFab, enemyGun1.position, transform.localRotation);
            GameObject temp1 = Instantiate(shotPreFab, enemyGun2.position, transform.localRotation);
            GameObject temp2 = Instantiate(shotPreFab, enemyGun3.position, transform.localRotation);
            GameObject temp3 = Instantiate(shotPreFab, enemyGun4.position, transform.localRotation);
            temp.GetComponent<Rigidbody2D>().velocity = enemyGun1.up * shotSpeed;       
            temp1.GetComponent<Rigidbody2D>().velocity = enemyGun2.up * shotSpeed;  
            temp2.GetComponent<Rigidbody2D>().velocity = enemyGun3.up * shotSpeed;
            temp3.GetComponent<Rigidbody2D>().velocity = enemyGun4.up * shotSpeed;
            }
            
        }           
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "PlayerShot":
                vidaExtra -= 1;
                if (vidaExtra == 0)
                {
                    GameObject temp = Instantiate(explosionPreFab, transform.position, transform.localRotation);
                    //temp.transform.parent = _gameController.cenario;
                    _gameManager.points += earnedPoints;
                    Destroy(this.gameObject);
                    Destroy(col.gameObject);
                   // _gameController.miniBoss += 1;
                }
                else
                {
                    GameObject temp = Instantiate(explosionPreFab, transform.position, transform.localRotation);
                }               
                break;
        }
    }
}
