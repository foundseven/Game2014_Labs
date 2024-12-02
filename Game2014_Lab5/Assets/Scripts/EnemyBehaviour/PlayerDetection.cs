using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField]
    bool _isSensing;

    [SerializeField]
    bool _LOS;

    PlayerBehaviour _player;

    [SerializeField]
    LayerMask _layerMask;

    void Start()
    {
        _player = FindObjectOfType<PlayerBehaviour>();
    }

    void Update()
    {
        if(_isSensing)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, _player.transform.position, _layerMask);

            Vector2 playerDirection = _player.transform.position - transform.position;
            float playerDirectionValue = (playerDirection.x > 0) ? 1 : -1;
            float enemyLookingDirectionValue = (transform.parent.localScale.x > 0) ? -1 : 1;

            _LOS = (hit.collider.name == "Player") && playerDirectionValue == enemyLookingDirectionValue;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _isSensing = true;
        }
    }

    private void OnDrawGizmos()
    {
        Color color = (_LOS) ? Color.green : Color.red;

        if(_isSensing)
        {
            Debug.DrawLine(transform.position, _player.transform.position, color);
        }
    }

    public bool GetLOSStatus()
    {
        return _LOS;
    }
}
