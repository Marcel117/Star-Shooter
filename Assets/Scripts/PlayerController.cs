using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager _gameManager;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    public float playerSpeed;
    private int direction;
    public GameObject[] PowerUp;
    public int powers;

    [Header("Life Settings")]
 
    public float HPMax;
    public float HP;
    public Transform HPBar;  
    public GameObject explosionPreFab;
    private Vector3 theScale;
    private float percLife;

    void Start()
    {
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager; 

        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        HPBar = GameObject.Find("barraHP").transform;
        HPBar.localScale = new Vector3(0.5f,0.02f,1f);

        HP = HPMax;
        percLife = HP / HPMax;

        PowerUp[powers].SetActive(true);
    }
    void Update()
    {        
        limitarMovimentoPlayer(); 
    }
        void LateUpdate() 
    {
        /*if(_gameManager.points >= 15)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, -4, 0), 1 * Time.deltaTime);            
        }*/
    }
    void FixedUpdate() 
    {
        float horizontal = Input.GetAxis("Horizontal");
        if(horizontal < 0) 
        {
            direction = -1;
        }
        else if(horizontal > 0) 
        {
            direction = 1;
        }
        else
        {
            direction = 0;
        }
        float vertical = Input.GetAxis("Vertical");

        

        playerRb.velocity = new Vector2(horizontal * playerSpeed, vertical * playerSpeed);

        playerAnimator.SetInteger("direcao", direction);
    }
    void damage(int damageTake)
    {
        HP -= damageTake;
        percLife = (HP / HPMax) / 2;
        theScale = HPBar.localScale;
        theScale.x = percLife;
        HPBar.localScale = theScale;
        if(HP <= 0) 
        {
            explosion();                     
        }
    }
    void explosion()
    {
        GameObject temp = Instantiate(explosionPreFab, transform.position, transform.rotation);  
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, playerSpeed);
        
        _gameManager.Dead();
        Destroy(this.gameObject);
    }
    void powerUp()
    {
        powers ++;
        if(powers <= PowerUp.Length - 1) // o "-1" está aí pq o array começa em zero. Ñ fosse isso, o valor de powers poderia ir a 3. Oq não queremos.
        {           
            PowerUp[powers].SetActive(true);
        }
        else
        {
            _gameManager.points += 3;
        }      
    }
    void limitarMovimentoPlayer()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;
        
        if (posY > _gameManager.limiteSuperior.position.y)
        {
            transform.position = new Vector3(posX, _gameManager.limiteSuperior.position.y, 0);
        }
        else if (posY < _gameManager.limiteInferior.position.y)
        {
            transform.position = new Vector3(posX, _gameManager.limiteInferior.position.y, 0);
        }
        if (posX > _gameManager.limiteDireito.position.x)
        {
            transform.position = new Vector3(_gameManager.limiteDireito.position.x, posY, 0);
        }
        else if (posX < _gameManager.limiteEsquerdo.position.x)
        {
            transform.position = new Vector3(_gameManager.limiteEsquerdo.position.x, posY, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D col) 
    {
        switch(col.gameObject.tag)
        {
            case "Enemy01Shot":
                damage(1);
                Destroy(col.gameObject);
                    break;  

            case "PowerUp":
                powerUp();
                Destroy(col.gameObject);
                break;
        }
    }   
    void OnCollisionEnter2D(Collision2D other) 
    {
        switch(other.gameObject.tag)
        {
            case "Enemy01":
                damage(3);
                break; 
        }  
    } 
}
