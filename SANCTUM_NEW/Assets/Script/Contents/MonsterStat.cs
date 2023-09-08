using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    private void OnEnable()
    {
        _hp = 100;
        _maxHp = 100;
        _bulletDamage = 10.0f;
        _bulletSpeed = 50.0f;
        _range = 5.0f;
        _fireRate = 0.5f;
        _exp = 1;
    }
}
