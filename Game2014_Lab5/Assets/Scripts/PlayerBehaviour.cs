using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    float _horizontalForce;

    [SerializeField]
    float _verticalForce;

    [SerializeField]
    float _horizontalSpeedLimit;

    [SerializeField]
    [Range(0f, 1f)]
    float _airFactor;

    [SerializeField]
    Transform _groundingPoint;

    [SerializeField]
    float _groundingRadius;

    [SerializeField]
    LayerMask _groundLayerMask;

    [SerializeField]
    [Range(0f, 1f)]
    float _leftJoystickVerticalThreshold;

    [SerializeField]
    float _deathFallSpeed = 5;

    Animator _animator;

    Rigidbody2D _rigidBody;

    Joystick _leftJoystick;
    HealthBarController _healthBar;
    bool _isGrounded;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _healthBar = GetComponentInChildren<HealthBarController>();

        if (GameObject.Find("OnScreenControllers"))
        {
            _leftJoystick = GameObject.Find("LeftJoystick").GetComponent<Joystick>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundingPoint.position, _groundingRadius, _groundLayerMask);

        Move();
        Jump();
        AnimtorStateControl();
    }

    public void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        if(_leftJoystick)
        {
            xInput = _leftJoystick.Horizontal;
            Debug.Log(_leftJoystick.Horizontal + "-" + _leftJoystick.Vertical);
        }
        if (xInput != 0.0f)
        {
            Vector2 force = Vector2.right * xInput * _horizontalForce;
            if (!_isGrounded)
            {
                force = new Vector2(force.x * _airFactor, force.y);
            }

            _rigidBody.AddForce(force);

            GetComponent<SpriteRenderer>().flipX = (force.x < 0.0f);

            if(Mathf.Abs(_rigidBody.velocity.x) > _horizontalSpeedLimit) 
            {
                float updateXvalue = Mathf.Clamp(_rigidBody.velocity.x, -_horizontalSpeedLimit, _horizontalSpeedLimit);
                _rigidBody.velocity = new Vector2(updateXvalue, _rigidBody.velocity.y);
            }
            //_rigidBody.velocity = new Vector2(Vector2.ClampMagnitude(_rigidBody.velocity, _horizontalSpeedLimit).x, _rigidBody.velocity.y);
        }
    }

    public void Jump()
    {
        var jumpPressed = Input.GetAxisRaw("Jump");
        if(_leftJoystick)
        {
            jumpPressed = _leftJoystick.Vertical;
        }
        if (_isGrounded && jumpPressed > _leftJoystickVerticalThreshold)
        {
            _rigidBody.AddForce(Vector2.up * _verticalForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
           _healthBar.TakeDamage(collision.GetComponent<IDamage>().Damage());

        }
    }

    void AnimtorStateControl()
    {
        if (_isGrounded) 
        { 
            if (Mathf.Abs(_rigidBody.velocity.x) > 0.1)
            {
                _animator.SetInteger("State", (int)AnimationStates.WALK);
            }
            else
            {
                _animator.SetInteger("State", (int)AnimationStates.IDLE);
            }
        }
        else
        {
            if(Mathf.Abs(_rigidBody.velocity.y) > _deathFallSpeed) 
            {
                _animator.SetInteger("State", (int)AnimationStates.FALL);
            }
            else
            {
                _animator.SetInteger("State", (int)AnimationStates.JUMP);
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundingPoint.position, _groundingRadius);
    }
}
