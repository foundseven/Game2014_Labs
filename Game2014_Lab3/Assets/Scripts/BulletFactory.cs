using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    //this is what instantiates the prefab essentially

    [SerializeField]
    public static GameObject _bulletPrefab;

    void Awake()
    {
        _bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    //create the gameobject
    public static GameObject CreateBullet(BulletType type)
    {
        GameObject bullet = Instantiate(_bulletPrefab);
        bullet.SetActive(false);

        switch (type)
        {
            case BulletType.PLAYER:
                bullet.GetComponent<BulletBehaviour>()._bulletType = type;
                bullet.transform.eulerAngles = Vector3.zero;
                bullet.GetComponent<SpriteRenderer>().color = Color.white;
                bullet.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Bullet");
                bullet.tag = "PlayerBullet";
                break;
            case BulletType.ENEMY:
                bullet.GetComponent<BulletBehaviour>()._bulletType = type;
                bullet.transform.eulerAngles = new Vector3(0, 0, 180);
                bullet.GetComponent<SpriteRenderer>().color = Color.green;
                bullet.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/EnemySmallBullet");
                bullet.tag = "EnemyBullet";
                break;
        }

        return bullet;
    }
}
