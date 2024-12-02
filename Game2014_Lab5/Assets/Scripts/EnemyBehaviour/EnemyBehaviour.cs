using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamage
{
    [SerializeField]
    float _speed = .1f;

    [SerializeField]
    Transform _baseCenterPoint;

    [SerializeField]
    Transform _frontGroundPoint;

    [SerializeField]
    Transform _frontObstaclePoint;

    [SerializeField]
    float _groundCheckDistance;

    [SerializeField]
    LayerMask _layerMask;

    [SerializeField]
    int _damage;

    PlayerDetection _playerDetector;

    Animator _animator;

    bool _isGrounded;
    bool _isOnTheEdge;
    bool _isThereFrontObstacle;

    private void Start()
    {
        _playerDetector = GetComponentInChildren<PlayerDetection>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.Linecast(_baseCenterPoint.position, _baseCenterPoint.position + Vector3.down * _groundCheckDistance, _layerMask);
        _isOnTheEdge = Physics2D.Linecast(_baseCenterPoint.position, _frontGroundPoint.position, _layerMask);
        _isThereFrontObstacle = Physics2D.Linecast(_baseCenterPoint.position, _frontObstaclePoint.position, _layerMask);

        if(_isGrounded && (!_isOnTheEdge || _isThereFrontObstacle))
        {
            ChangeDirection();
        }

        _animator.SetInteger("State", (int)AnimationStates.IDLE);

        if (_isGrounded && !_playerDetector.GetLOSStatus())
        {
            Move();
        }
    }

    void Move()
    {
        _animator.SetInteger("State", (int)AnimationStates.WALK);
        transform.position += Vector3.left * transform.localScale.x * _speed;
    }

    void ChangeDirection()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    public int Damage()
    {
        return _damage;
    }
    private void OnDrawGizmos()
    {
        Debug.DrawLine(_baseCenterPoint.position, _baseCenterPoint.position + Vector3.down * _groundCheckDistance);
        Debug.DrawLine(_baseCenterPoint.position, _frontGroundPoint.position);

    }
}
