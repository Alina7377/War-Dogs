using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

public class DummyPlayerCharacterisctics: IGameConfig
{
    public event Action OnUpdate;

    private string _url = "http://drive.google.com/uc?id=1e44QJD_BEytOPnw_lVJ1-0Eiw-Bg8lOr";
    private struct SConfigData
    {
        public int Health;
        public float Speed;
        public float JerkSpeed;
        public float ShootDelay;
    }

    private int _startHealth = 100;
    private float _startSpeed = 5;
    private float _jerkSpeed = 10;
    private float _shootDelay = 2f;

    public  DummyPlayerCharacterisctics()
    {
        var text = ObservableWWW.Get(_url)
        .Subscribe(x =>
        {
            SConfigData  loadPlayerData = JsonUtility.FromJson<SConfigData>(x);
            _startHealth = loadPlayerData.Health;
            _startSpeed = loadPlayerData.Speed;
            _jerkSpeed = loadPlayerData.JerkSpeed;
            _shootDelay = loadPlayerData.ShootDelay;

            Debug.Log("Данные загружены!");
        });
    }

    public int GetHealth => _startHealth;

    public float GetSpeed => _startSpeed;

    public float GetJerkSpeed => _jerkSpeed;

    public float GetShootDelay => _shootDelay;

    public void UpdateInfo()
    {
        OnUpdate?.Invoke();
    }
}
