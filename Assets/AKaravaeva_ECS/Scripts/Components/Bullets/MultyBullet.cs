using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MultyBullet : StandartBullet
{
    [SerializeField] private int _countStrokes;
    [SerializeField] private float _angle = 130;
    [SerializeField] private Rigidbody _rigit;

    private int _currentStorkes = 0;

    public override void Live()
    {
        if (Time.time > _timeCreate + _timeToLive)
        {
            Die();
        }
    }

    private void Start()
    {
        _rigit.velocity = transform.forward * _speedFly;
        _timeCreate = Time.time;
    }


    private void Update()
    {
        Live();
    }


    private void OnCollisionEnter(Collision collision)
    {
        _currentStorkes++;
        if (_currentStorkes >= _countStrokes)
            Die();
        else
        {
            transform.Rotate(new Vector3(0, 1, 0), _angle);
            _rigit.velocity = transform.forward * _speedFly;
        }
    }

}
