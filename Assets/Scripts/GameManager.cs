using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Boss1Movement _boss1Movement;
    public Text score;
    public int points;
    private IAEnemy _IAEnemy;
    public PlayerController _playerController;

    [Header("Limites de Movimento")]
    public Transform limiteSuperior;
    public Transform limiteInferior;
    public Transform limiteDireito;
    public Transform limiteEsquerdo;
    
     [Header("Player Life Settings")]
    public int extraLifes;
    public GameObject lifeIcon;
    public Transform extraLifeicon;
    public GameObject[] extras;
    public Transform spawnPlayer;
    public GameObject playerSpawn;

    [Header("SpawnEnemy01")]
    public GameObject[] enemyPreFab;
    public float delaySpawn;
    public Transform leftLimit;
    public Transform rightLimit;
    private float minX;
    private float maxX;
    private float tempTime; // Temporizador

    void Start()
    {
        _IAEnemy = FindObjectOfType(typeof(IAEnemy)) as IAEnemy; 
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        _boss1Movement = FindObjectOfType(typeof(Boss1Movement)) as Boss1Movement;
        
        Lifes();

        minX = leftLimit.position.x;
        maxX = rightLimit.position.x;
    }

    void Update()
    {             
        tempTime += Time.deltaTime;
        if(tempTime >= delaySpawn) 
        {
            tempTime = 0;
            spawnEnemys();
        }
        score.text = points.ToString();
    }
    void Lifes()
    {        
        GameObject tempPlayer;
        
        GameObject tempLife;
        float posXicon; // Posição X do ícone de vida extra

        foreach(GameObject Life in extras)
        {
            if(Life != null)
            {
                Destroy(Life);
            }
        }
        for(int i = 0; i < extraLifes; i++)
        {
            posXicon = extraLifeicon.position.x + (0.5f * i); //A conta é para dstanciar os ícones de vidas extras (0.5 de distância entre os ícones).
            tempLife = Instantiate(lifeIcon) as GameObject;
            extras[i] = tempLife;
            tempLife.transform.position = new Vector3(posXicon, extraLifeicon.position.y, extraLifeicon.position.z);
        }

        tempPlayer = Instantiate(playerSpawn);
        tempPlayer.transform.position = spawnPlayer.position;
        
    }    
    public void Dead()
    {
        if(extraLifes > 0) 
        {
            Lifes();
        }
        else
        {
            mudarCena("GameOver");
            //Application.LoadLevel("GameOver");
        }
        extraLifes -= 1;       
    }
    void spawnEnemys()
    {
        if(points <= 70)
        {
        int x = Random.Range(0,5);
        float posX = Random.Range(minX, maxX);
        GameObject temp = Instantiate(enemyPreFab[x]) as GameObject;
        temp.transform.position = new Vector3(posX, 7, 0);
        }   
        else
        {
            SceneManager.LoadScene("Boss"); 
        }                        
    }  
    public void mudarCena(string GameOver)
    {
        SceneManager.LoadScene("GameOver");       
    }  
    
}
