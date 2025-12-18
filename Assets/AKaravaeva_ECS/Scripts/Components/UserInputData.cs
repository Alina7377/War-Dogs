using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using static UnityEngine.EventSystems.EventTrigger;

public class UserInputData : MonoBehaviour,IConvertGameObjectToEntity
{
    [SerializeField] private float _speed;

    [Inject]
    private IGameConfig _gameConfig;

    public MonoBehaviour shootAction;
    public MonoBehaviour jerkAction;
    private Entity _selfEntity;
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
        if (_selfEntity == null)
        {
            Debug.Log("еще не создан");
            return;
        }
        _selfEntityManager.SetComponentData(_selfEntity, new ControlData { Speed = _gameConfig.GetSpeed });
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _selfEntity = entity;
        _selfEntityManager = dstManager;
        dstManager.AddComponentData(entity, new InputData());
        dstManager.AddComponentData(entity, new ControlData
        {
            Speed = _speed
        });

        if (shootAction != null && shootAction is IAbility)
        {
            dstManager.AddComponentData(entity, new ShootData { BulletType = ETypeBullet.Standart });
        }

        if (jerkAction != null && jerkAction is IAbilityRetBool)
        {
            dstManager.AddComponentData(entity, new JerkData());
        }

        dstManager.AddComponentData(entity, new AnimateData());
    }
}

public struct InputData : IComponentData
{
    public float2 Move;
    public float Shoot;
    public float Jerk;    
}

public struct ControlData : IComponentData
{
    public float Speed;
}

public struct ShootData : IComponentData
{
    public ETypeBullet BulletType;
    public bool IsShoot;
}

public struct JerkData : IComponentData
{
    public bool IsJerk;
}

public struct AnimateData : IComponentData
{
    
}


