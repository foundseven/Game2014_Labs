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

    Rigidbody2D _rigidBody;

    bool _isGrounded;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundingPoint.position, _groundingRadius, _groundLayerMask);

        Move();

        Jump();
    }

    public void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        if (xInput != 0.0f)
        {
            Vector2 force = Vector2.right * xInput * _horizontalForce;
            if (!_isGrounded)
            {
                force *= _airFactor;
            }

            _rigidBody.AddForce(force);

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

        if (_isGrounded && jumpPressed != 0.0f)
        {
            _rigidBody.AddForce(Vector2.up * _verticalForce);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundingPoint.position, _groundingRadius);
    }
}
