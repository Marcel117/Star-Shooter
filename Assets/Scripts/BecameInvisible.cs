using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecameInvisible : MonoBehaviour
{
void OnBecameInvisible() 
{
    Destroy(this.gameObject);
}

}
