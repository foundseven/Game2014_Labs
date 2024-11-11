using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlane : MonoBehaviour
{
    Vector3 _spawnPOS = new Vector3(-1.54f, 0.47f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.transform.position = _spawnPOS;
        }
    }

    public void UpdateSpawnPosition(Vector3 checkPoint)
    {
        _spawnPOS = checkPoint;
    }
}
