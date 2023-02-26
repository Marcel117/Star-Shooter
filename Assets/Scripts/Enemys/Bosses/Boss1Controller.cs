using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1Controller : MonoBehaviour
{
   private PlayerController _playerController;
   private GameManager _gameManager;
   public int HP;
   public GameObject explosionPreFab;
   public int earnedPoints;
   public float enemySpeed;
   private bool isBossAlive;

   [Header("ShotSettings")]   
    public float shotSpeed;
    public Transform enemyGun1;
    public Transform enemyGun2;
    public Transform enemyGun3;
    public Transform enemyGun4;
    public Transform mainGun;
    public GameObject shotPreFab;
    public GameObject mainShotPreFab;
    public int shotChance; // Evita que o inimigo atire sem parar
    public float delayShot; // Evita que o inimigo atire sem parar
    private float tempTimeShot; // Temporizador do tiro
    private float mainTempTimeShot; // Temporizador do tiro Principal
    private float delaydeath;

    void Start()
    {
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
       // StartCoroutine("atirar");

        isBossAlive = true;
    }

    void Update()
    {
        mainTempTimeShot += Time.deltaTime;
        tempTimeShot += Time.deltaTime;
        shot();
        mainShot();
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
    void shot()
    {
      if(tempTimeShot >= delayShot) // temporizador alcançou o valor do delay entre os tiros
        {
            tempTimeShot = 0;          
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
    void mainShot()
    {
        if(mainTempTimeShot >= 3) 
        {
            mainTempTimeShot = 0;
            GameObject temp = Instantiate(mainShotPreFab, mainGun.position, transform.localRotation);
            temp.GetComponent<Rigidbody2D>().velocity = mainGun.up * shotSpeed;
        }
    }
    void OnTriggerEnter2D(Collider2D col) 
    {
        switch(col.gameObject.tag) 
        {            
            case "PlayerShot":
            if(HP > 0)
            {
                GameObject temp = Instantiate(explosionPreFab, transform.position, transform.rotation);                
            }
            else
            {
                SceneManager.LoadScene("CenaPlay");
            damage(1);
            Destroy(col.gameObject);         
            //SpawnLoot();
            }
            
            break;
        }
    }
}
