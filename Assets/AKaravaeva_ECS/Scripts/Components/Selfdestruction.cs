using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Selfdestruction : MonoBehaviour, IAbilityTarget, IConvertGameObjectToEntity
{
    public List<MonoBehaviour> destroyPermissions = new List<MonoBehaviour>();

    private List<IDestroyBuff> _permissions = new List<IDestroyBuff>();

    public List<GameObject> Targets { get; set; }

    private Entity _selfEntity;

    private void Start()
    {
        foreach (var action in destroyPermissions)
        {
            if (action is IDestroyBuff ability)
            {
                _permissions.Add(ability);
            }
            else
            {
                Debug.LogError("Ошибка получения IDestroyBuff");
            }
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _selfEntity = entity;
    }

    public void Execute()
    {
        bool isCanDestroy = true;
        foreach (var action in _permissions)
        {
            if (!action.IsCanDestroy)
                isCanDestroy = false;
        }
        if (!isCanDestroy) return;
        World.DefaultGameObjectInjectionWorld.EntityManager.DestroyEntity(_selfEntity);
        Destroy(this.gameObject);
    }    
}
