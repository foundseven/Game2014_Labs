using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatformBehaviour : MonoBehaviour
{
    [SerializeField]
    float _horizontalSpeed = 5;

    [SerializeField]
    float _horizontalDistance = 5;

    Vector2 _startPos;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalDistance) + _startPos.x , transform.position.y);
    }
}
