using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager
{
    //system that manages the bullets in the containers
    //property
    public static BulletManager Instance 
    { 
        //anytime i call bullet manager . instance it is going to check if it is valid
        get 
        {
            if (_instance == null)
            {
                _instance = new BulletManager();
            }
            return _instance;
        } 
    }
    //field
    private static BulletManager _instance;
    //now i can add logic to the properties


    [SerializeField]
    int _totalBulletNum = 20;

    GameObject _bulletPrefab;
    //making a dictionary
    Dictionary<BulletType, Queue<GameObject>> bullets;


    private BulletManager() 
    {
        //init the dictionary of bullets
        bullets = new Dictionary<BulletType, Queue<GameObject>>();


        //need a working queue
        //(int) is just like casting
        for(int i = 0; i < (int) BulletType.SIZE; i++)
        {
            //casts it to a bullet type like how we did before
            bullets[(BulletType)i] = new Queue<GameObject> ();
        }
        for(int i = 0; i < _totalBulletNum; i++)
        {
            bullets[BulletType.PLAYER].Enqueue(BulletFactory.CreateBullet(BulletType.PLAYER));
        }
        for (int i = 0; i < _totalBulletNum; i++)
        {
            bullets[BulletType.ENEMY].Enqueue(BulletFactory.CreateBullet(BulletType.ENEMY));
        }
    }

    public static GameObject GetBullet(BulletType type)
    {
        if (Instance.bullets[type].Count <= 1)
        {
            BulletFactory.CreateBullet(type);
        }

        GameObject bullet = Instance.bullets[type].Dequeue();
        bullet.SetActive(true);

        return bullet;
    }

    public static void ReturnBullet(GameObject bullet, BulletType type)
    {
        bullet.SetActive(false);
        Instance.bullets[type].Enqueue(bullet);
    }
}
