
using Unity.Entities;
using UnityEngine;

public class HealthSystem : ComponentSystem
{
    private EntityQuery _healthQuery;

    protected override void OnCreate()
    {
        _healthQuery = GetEntityQuery(ComponentType.ReadOnly<HealthData>());
    }

    protected override void OnUpdate()
    {
        var dstManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        Entities.With(_healthQuery).ForEach(
            (Entity entity, CharacterHealth charavterHealth, ref HealthData healthData, ref DamageData damageData) =>
            {
                if (!(damageData.Amount == 0))
                {
                    healthData.Health -= Mathf.Max(1,damageData.Amount - (int)healthData.DamageReduction);
                    damageData.Amount = 0;
                    if (healthData.Health <= 0)
                    {
                        healthData.IsDie = true;
                        charavterHealth.DestroyObject();
                    }
                    else
                        if (healthData.Health >= healthData.MaxHealth)
                    {
                        healthData.Health = healthData.MaxHealth;
                    }                    
                }

            }
            );
    }
}
