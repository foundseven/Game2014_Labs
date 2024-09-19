using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private float _verticalSpeed;
    private float _horizontalSpeed;

    [SerializeField] Boundry _verticalSpeedRange;
    [SerializeField] Boundry _horizonalSpeedRange;

    [SerializeField] Boundry _verticalBoundry;
    [SerializeField] Boundry _horizontalBoundry;
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        //move enemy vertically  and horizontally
        //transform.position = new Vector2(transform.position.x + _horizontalSpeed * Time.deltaTime, transform.position.y - _verticalSpeed * Time.deltaTime); 
        transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time,
            _horizontalBoundry.max - _horizontalBoundry.min) + _horizontalBoundry.min,
            transform.position.y + _verticalSpeed * Time.deltaTime);
        //checks if player is off the screen from the bottom and resets accordingly
        if (transform.position.y < _verticalBoundry.min)
        {
            Reset();
        }
        //checks if the player is off the screen from the sides and will change the speed the other direction
        if (transform.position.x > _horizontalBoundry.max || 
            transform.position.x < _horizontalBoundry.min)
        {
            _horizontalSpeed = - _horizontalSpeed;
        }
    }

    //resets the enemys position and speeds
    private void Reset()
    {
        transform.position = new Vector2(Random.Range(_horizontalBoundry.min, _horizontalBoundry.max), _verticalBoundry.max);
        _verticalSpeed = Random.Range(_verticalSpeedRange.min, _verticalSpeedRange.max);
        _horizontalSpeed = Random.Range(_horizonalSpeedRange.min, _horizonalSpeedRange.max);
    }
}
