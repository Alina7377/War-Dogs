using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ShootPerk : MonoBehaviour, IAbilityTarget, IDestroyBuff
{
    public ETypeBullet TypeBullet = ETypeBullet.Standart;

    public List<GameObject> Targets { get ; set ; }
    public bool IsCanDestroy { get; set; }

    private EntityManager _entityManager;
    private void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        IsCanDestroy = false;
    }

    public void Execute()
    {
        foreach (var target in Targets)
        {
            if (target.TryGetComponent<ShootAbility>(out ShootAbility shootAbility)) 
            {
                var entity = shootAbility.shootEntity;
                _entityManager.SetComponentData<ShootData>(entity, new ShootData { BulletType = TypeBullet });
                IsCanDestroy = true;
            }
        }
    }
}
