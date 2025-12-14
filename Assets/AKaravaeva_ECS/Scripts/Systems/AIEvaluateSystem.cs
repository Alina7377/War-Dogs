using System.Collections.Generic;
using System.Diagnostics;
using Unity.Entities;

public class AIEvaluateSystem : ComponentSystem
{
    private EntityQuery _aiEvaluateQuery;


    protected override void OnCreate()
    {
        _aiEvaluateQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_aiEvaluateQuery).ForEach(
            (Entity entity, BehaviourManager manager) =>
            {

                float hightScore = float.MinValue;

                manager.actualBehaviour = null;

                foreach (var behaviour in manager.behaviours)
                {
                    var currentScore = behaviour.Evalute();
                    if (currentScore > hightScore)
                    {
                        hightScore = currentScore;
                        manager.actualBehaviour = behaviour;
                    }
                }
            });
    }
}
