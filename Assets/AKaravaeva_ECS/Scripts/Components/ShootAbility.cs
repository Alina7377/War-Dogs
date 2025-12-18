using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Zenject;

[Serializable]
public struct STypeBullet
{
    public ETypeBullet type;
    public GameObject pref;
}

public enum ETypeBullet
{
    Standart,
    MultBull
}

public class ShootAbility : MonoBehaviour, IAbility, IConvertGameObjectToEntity
{
    [SerializeField] private List<STypeBullet> _bullets;
    [SerializeField] private float _shootDelay;
    [SerializeField] private Transform _shootPoint;

    [Inject]
    private IGameConfig _gameConfig;

    private float _shootTime = 0f;
    public Entity shootEntity;

    private void OnEnable()
    {
        _gameConfig.OnUpdate += UpdateParams;
    }

    private void OnDisable()
    {
        _gameConfig.OnUpdate -= UpdateParams;
    }

    private void UpdateParams()
    {
        _shootDelay = _gameConfig.GetShootDelay;
    }

    private void CreateBullet(ShootData shootData, Transform spawnPoint) 
    {
        foreach (var bulletType in _bullets)
        {
            if (bulletType.type == shootData.BulletType)
            {
                var bullet = Instantiate(bulletType.pref, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        shootEntity = entity;
        
    }

    public void Execute()
    {
        if (Time.time < _shootTime + _shootDelay) return;

        _shootTime = Time.time;
        if (_bullets.Count > 0)
        {
            var shootData = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<ShootData>(shootEntity);
            shootData.IsShoot = true;
            var trans = _shootPoint;
            World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(shootEntity, shootData);
            CreateBullet(shootData, trans);
        }
        else
        {
            Debug.Log("[SHOOT ABILITY] No bullet prefab!");
        }
    }

}



