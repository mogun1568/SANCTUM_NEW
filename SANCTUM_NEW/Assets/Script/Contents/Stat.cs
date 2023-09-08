using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] protected int _hp;
    [SerializeField] protected int _maxHp;
    [SerializeField] protected float _bulletDamage;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected float _range;
    [SerializeField] protected float _fireRate;
    [SerializeField] protected float _fireCountdown;
    [SerializeField] protected int _exp;

    public int HP { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public float BulletDamage { get { return _bulletDamage; } set { _bulletDamage = value; } }
    public float BulletSpeed { get { return _bulletSpeed; } set { _bulletSpeed = value; } }
    public float Range { get { return _range; } set { _range = value; } }
    public float FireRate { get { return _fireRate; } set { _fireRate = value; } }
    public float FireCountdown { get { return _fireCountdown; } set { _fireCountdown = value; } }
    public int Exp { get { return _exp; } set { _exp = value; } }
}
