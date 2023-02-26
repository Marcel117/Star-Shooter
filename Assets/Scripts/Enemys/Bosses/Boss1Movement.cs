using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Movement : MonoBehaviour
{
    //private inimigoController _inimigoController;
    public Transform naveInimiga;
    public Transform[] checkPoints;
    public float bossSpeed;
    public float delayParado;
    private int idCheckPoint;
    private bool movimentar;
   
    void Start()
    {
        //_inimigoController = FindObjectOfType(typeof(inimigoController)) as inimigoController;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 1f, 0), bossSpeed * Time.deltaTime);
        StartCoroutine("iniciarMovimento");
        movimentar = true;       
    }

    // Update is called once per frame
    void Update()
    {
       // if(movimentar == true && _inimigoController.isBossAlive == true)
        //{
            if(movimentar == true)
            {
                naveInimiga.transform.position = Vector3.MoveTowards(naveInimiga.position, checkPoints[idCheckPoint].position, bossSpeed * Time.deltaTime);
            if(naveInimiga.position == checkPoints[idCheckPoint].position)
            {
                movimentar = false;
                StartCoroutine("iniciarMovimento");
            }
            }            
       // }
    }
    IEnumerator iniciarMovimento()
    {        
        idCheckPoint++;
        if(idCheckPoint >= checkPoints.Length)
        {
            idCheckPoint = 0;
        }
        yield return new WaitForSeconds(delayParado);
        movimentar = true;                                           
    }
}
