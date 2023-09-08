using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStat : Stat
{
    [SerializeField] protected string _towerType;
    [SerializeField] protected int _level;

    public string TowerType { get { return _towerType; } set { _towerType = value; } }
    public int Level { get { return _level; } set { _level = value; } }

    public void OnEnable()
    {
        //Managers.Select.getItem

        if (_towerType == "StandardTower")
        {
            _level = 1;
            _hp = 100;
            _maxHp = 100;
            _bulletDamage = 50f;
            _bulletSpeed = 50f;
            _range = 15f;
            _fireRate = 1f;
            _exp = 1;
        } 
    }
}
