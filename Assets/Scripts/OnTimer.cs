using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTimer : MonoBehaviour
{
    public float lifeTime;
    public float tempTime;
    void Start()
    {
        
    }

    void Update()
    {
        tempTime += Time.deltaTime;
        if(tempTime >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
