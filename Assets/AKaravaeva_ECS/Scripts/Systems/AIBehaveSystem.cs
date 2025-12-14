using System.Collections.Generic;
using System.Diagnostics;
using Unity.Entities;

public class AIBehaveSystem : ComponentSystem
{
    private EntityQuery _aiBehaveQuery;


    protected override void OnCreate()
    {
        _aiBehaveQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_aiBehaveQuery).ForEach(
            (Entity entity, BehaviourManager manager) =>
            {
                manager.actualBehaviour?.Behave();
            });
    }
}
