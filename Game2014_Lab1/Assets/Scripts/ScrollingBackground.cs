using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    [SerializeField] private float _speed = 3.0f;
    private Vector3 _direction = Vector3.down;
    [SerializeField] private Boundry _boundry;
    [SerializeField] private Vector3 _spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime; 
        if(transform.position.y < _boundry.min)
        {
            transform.position = _spawnPosition;
        }
    }
}
