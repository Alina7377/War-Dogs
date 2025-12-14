using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharactersInSO :  IGameConfig
{
    private SOPlayerCharacteristics _soConfig;


    public PlayerCharactersInSO(SOPlayerCharacteristics soConfig)
    {
        _soConfig = soConfig;
    }

    public int GetHealth => _soConfig.startHealth;

    public float GetSpeed => _soConfig.startSpeed;

    public float GetJerkSpeed => _soConfig.jerkSpeed;

    public float GetShootDelay => _soConfig.shootDelay;
    
}
