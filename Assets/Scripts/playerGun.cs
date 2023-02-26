using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGun : MonoBehaviour
{
    public GameObject blueShotPrefab;
    public float shotSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            shot();
        }
    }
    void shot()
    {
        GameObject temp = Instantiate(blueShotPrefab, transform.position, transform.rotation);
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, shotSpeed);
    }
    
}
