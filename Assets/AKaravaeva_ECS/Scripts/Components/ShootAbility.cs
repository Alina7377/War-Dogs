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
    private CharacterData _characterData;
    public Entity shootEntity;

    private ShootData _shootData;

    private EntityManager _entityManager; 
    private void OnEnable()
    {
        _gameConfig.OnUpdate += UpdateParams;
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
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
                IBullet iBullet = bullet.GetComponent<IBullet>();
                if (iBullet !=null)
                {
                    iBullet.AddValDemage(_entityManager.GetComponentData<ShootData>(shootEntity).Demadge);
                }
            }
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        shootEntity = entity;
        _characterData = GetComponent<CharacterData>();
        dstManager.AddComponentData(entity, new ShootData { 
            BulletType = ETypeBullet.Standart,
            DelayShoot = _shootDelay
        });

    }

    public void Execute()
    {
        var shootData = _entityManager.GetComponentData<ShootData>(shootEntity);

        if (Time.time < _shootTime + shootData.DelayShoot) return;

        _shootTime = Time.time;
        if (_bullets.Count > 0)
        {            
            shootData.IsShoot = true;
            var trans = _shootPoint;
            World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(shootEntity, shootData);
            CreateBullet(shootData, trans);
            if (_characterData != null)
            {
                _characterData.AddScore(5);
            }
            
        }
        else
        {
            Debug.Log("[SHOOT ABILITY] No bullet prefab!");
        }
    }

}



