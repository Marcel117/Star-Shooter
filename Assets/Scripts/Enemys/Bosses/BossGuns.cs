using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGuns : MonoBehaviour
{
    private PlayerController _playerController;
    public float speed;
    void Start()
    {
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;

    }

    // Update is called once per frame
    void Update()
    {
        transform.up = _playerController.transform.position - transform.position;
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
