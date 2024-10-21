using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    int _totalBulletNum;

    GameObject _bulletPrefab;
    Queue<GameObject> _bulletPool = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        _bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");

        for(int i = 0; i < _totalBulletNum; i++)
        {
            CreateBullet();
        }
    }

    void CreateBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform);
        bullet.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }
    public GameObject GetBullet(BulletType type)
    {
        if(_bulletPool.Count <= 1)
        {
            CreateBullet();
        }
        GameObject bullet = _bulletPool.Dequeue();
        bullet.SetActive(true);

        switch(type)
        {
            case BulletType.PLAYER:
                bullet.transform.eulerAngles = Vector3.zero;
                bullet.GetComponent<SpriteRenderer>().color = Color.white;
                bullet.tag = "PlayerBullet";
                break;
            case BulletType.ENEMY:
                bullet.transform.eulerAngles = new Vector3(0, 0, 180);
                bullet.GetComponent<SpriteRenderer>().color = Color.green;
                bullet.tag = "EnemyBullet";
                break;
        }
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }
}
