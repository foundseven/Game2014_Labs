using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    Rigidbody2D _RB;

    [SerializeField]
    float _speed = 10;
    void Start()
    {
        _RB = GetComponent<Rigidbody2D>();
        Vector3 directionToTarget = (FindObjectOfType<PlayerBehaviour>().transform.position - transform.position).normalized;
        _RB.AddForce(directionToTarget * _speed, ForceMode2D.Impulse);
    }

}
