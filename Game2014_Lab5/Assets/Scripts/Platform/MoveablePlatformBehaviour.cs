using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatformBehaviour : MonoBehaviour
{
    [SerializeField]
    PlatformMovementTypes _type;

    [SerializeField]
    float _horizontalSpeed = 5;

    [SerializeField]
    float _horizontalDistance = 5;

    [SerializeField]
    float _verticalSpeed = 5;

    [SerializeField]
    float _verticalDistance = 5;

    [SerializeField]
    List<Transform> _pathList = new List<Transform>();

    List<Vector2> _destinations = new List<Vector2>();

    int _destinationIndex = 0;

    Vector2 _startPos;
    Vector2 _endPos;

    float _timer;

    [SerializeField]
    [Range(0f, 0.1f)]
    float _customMovementTimeChangeFactor;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in _pathList)
        {
            _destinations.Add(t.position);
        }
        _destinations.Add(transform.position);

        _startPos = transform.position;
        _endPos = _destinations[0];
    }

    private void FixedUpdate()
    {
        if(_type == PlatformMovementTypes.CUSTOM)
        {
            if(_timer >= 1) 
            {
                _timer = 0;
                _destinationIndex++;
                if(_destinationIndex >= _destinations.Count)
                {
                    _destinationIndex = 0;
                }
                _startPos = transform.position;
                _endPos = _destinations[_destinationIndex];
            }
            else
            {
                _timer += _customMovementTimeChangeFactor;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Move();
       // transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalDistance) + _startPos.x , transform.position.y);
    }

    void Move()
    {
        switch(_type)
        {
            case PlatformMovementTypes.HORIZONTAL:
                transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalDistance) + _startPos.x, transform.position.y);
                break;
            case PlatformMovementTypes.VERTICAL:
                transform.position = new Vector2(transform.position.x, Mathf.PingPong(_verticalSpeed * Time.time, _verticalDistance) + _startPos.y);
                break;
            case PlatformMovementTypes.DIAGONAL_RIGHT:
                transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalDistance) + _startPos.x,
                                                 Mathf.PingPong(_verticalSpeed * Time.time, _verticalDistance) + _startPos.y);
                break;
            case PlatformMovementTypes.DIAGONAL_LEFT:
                transform.position = new Vector2(_startPos.x - Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalDistance),
                                                 Mathf.PingPong(_verticalSpeed * Time.time, _verticalDistance) + _startPos.y);
                break;
            case PlatformMovementTypes.CUSTOM:
                transform.position = Vector2.Lerp(_startPos, _endPos, _timer);
                break;

        }
    }
}
