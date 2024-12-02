using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHazard : MonoBehaviour, IDamage
{
    [SerializeField]
    int _damage = 5;

    public int Damage()
    {
        return _damage;
    }
}
