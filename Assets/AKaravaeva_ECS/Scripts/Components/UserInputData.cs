using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

public class UserInputData : MonoBehaviour,IConvertGameObjectToEntity
{
    [SerializeField] private float _speed;

    [Inject]
    private IGameConfig _gameConfig;

    public MonoBehaviour shootAction;
    public MonoBehaviour jerkAction;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new InputData());
        dstManager.AddComponentData(entity, new ControlData
        {
            Speed = _gameConfig.GetSpeed
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


