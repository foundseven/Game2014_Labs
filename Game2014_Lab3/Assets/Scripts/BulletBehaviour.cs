using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    float _speed = 1;

    [SerializeField]
    float _baseSpeed = 3;

    [SerializeField]
    Boundry _boundry;

    BulletManager _bulletManager;

    public BulletType _bulletType;

    // Start is called before the first frame update
    void Start()
    {
        _baseSpeed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * _speed * Time.deltaTime;

        if (transform.position.y > _boundry.max || transform.position.y < _boundry.min)
        {
            ReturnToPool();
        }
    }

    public void ReturnToPool()
    {
        BulletManager.ReturnBullet(this.gameObject, _bulletType);
    }

    public void RelativeSpeedAddition(float speed)
    {
        _speed = _baseSpeed + speed;
    }
}
