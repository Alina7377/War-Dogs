
using System.Collections;
using Unity.Entities;
using UnityEngine;
using Zenject;


public class CharacterHealth : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] private int _maxHealth = 100;

    [Inject]
    private IGameConfig _gameConfig;

    public Entity healthEntity;

    private EntityManager _selfEntityManager;

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
        if (healthEntity == null)
        {
            Debug.Log("еще не создан");
            return;
        }
        var HealthData = _selfEntityManager.GetComponentData<HealthData>(healthEntity);
        HealthData.MaxHealth = _gameConfig.GetHealth;
        HealthData.Health = _gameConfig.GetHealth;
        _selfEntityManager.SetComponentData(healthEntity, HealthData);
    }

    private IEnumerator DieCharacter() 
    {
        yield return new WaitForSeconds(3);
        World.DefaultGameObjectInjectionWorld.EntityManager.DestroyEntity(healthEntity);
        Destroy(this.gameObject);
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _selfEntityManager = dstManager;
        dstManager.AddComponentData(entity, new HealthData
        {
            MaxHealth = _maxHealth,
            Health = _maxHealth,
            IsDie = false
        });
        dstManager.AddComponentData(entity, new DamageData
        {
            Amount = 0
        });
        healthEntity = entity;
    }

    public void DestroyObject()
    {        
        StartCoroutine(DieCharacter());
    }
}


public struct HealthData : IComponentData
{
    public int Health;
    public int MaxHealth;
    public bool IsDie;
}

public struct DamageData : IComponentData
{
    public int Amount;
}
