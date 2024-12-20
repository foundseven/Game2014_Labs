using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private float _verticalSpeed;
    private float _horizontalSpeed;

    [SerializeField] 
    Boundry _verticalSpeedRange;

    [SerializeField] 
    Boundry _horizonalSpeedRange;

    [SerializeField] 
    Boundry _verticalBoundry;

    [SerializeField] 
    Boundry _horizontalBoundry;

    [SerializeField]
    Transform _shootingPoint;

    [SerializeField]
    [Range(0.01f, 1.00f)]
    float _shootingCooldownTime;

    GameObject _bulletPrefab;
    BulletManager _bulletManager;

    GameController _gameController;
    SpriteRenderer _spriteRenderer;

    Color[] _colors = {Color.green, Color.cyan, Color.white,
    Color.magenta, Color.grey};
    // Start is called before the first frame update
    void Start()
    {
        _bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameController = FindObjectOfType<GameController>();
        Reset();
        StartCoroutine(ShootingRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        //move enemy vertically  and horizontally
        transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time,
                             _horizontalBoundry.max - _horizontalBoundry.min) + _horizontalBoundry.min,
                             transform.position.y + _verticalSpeed * Time.deltaTime);
        //checks if player is off the screen from the bottom and resets accordingly
        if (transform.position.y < _verticalBoundry.min)
        {
            Reset();
        }
    }
    IEnumerator ShootingRoutine()
    {
        GameObject bullet = BulletManager.GetBullet(BulletType.ENEMY);
        bullet.transform.position = _shootingPoint.position;
        bullet.GetComponent<BulletBehaviour>().RelativeSpeedAddition(Mathf.Abs(_verticalSpeed));
        yield return new WaitForSeconds(_shootingCooldownTime);

        StartCoroutine(ShootingRoutine());
    }

    public IEnumerator DyingRoutine()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
    public void DyingSequence()
    {
        _spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;  
    }

    //resets the enemys position and speeds
    private void Reset()
    {
        _spriteRenderer.color = _colors[Random.Range(0, _colors.Length)];
        _spriteRenderer.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        transform.position = new Vector2(Random.Range(_horizontalBoundry.min, _horizontalBoundry.max), _verticalBoundry.max);
        transform.localScale = new Vector3(1f + Random.Range(-.3f, .3f), 1f + Random.Range(-.3f, .3f), 1f);
        _verticalSpeed = Random.Range(_verticalSpeedRange.min, _verticalSpeedRange.max);
        _horizontalSpeed = Random.Range(_horizonalSpeedRange.min, _horizonalSpeedRange.max);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBullet"))
        {
            _gameController.ChangeScore(15);
            collision.GetComponent<BulletBehaviour>().ReturnToPool();
            StartCoroutine(DyingRoutine());
        }
    }

}
