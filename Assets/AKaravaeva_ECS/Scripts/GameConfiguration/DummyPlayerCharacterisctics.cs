using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayerCharacterisctics: IGameConfig
{
    private int _startHealth = 100;
    private float _startSpeed = 5;
    private float _jerkSpeed = 10;
    private float _shootDelay = 2f;

    public int GetHealth => _startHealth;

    public float GetSpeed => _startSpeed;

    public float GetJerkSpeed => _jerkSpeed;

    public float GetShootDelay => _shootDelay;
}
