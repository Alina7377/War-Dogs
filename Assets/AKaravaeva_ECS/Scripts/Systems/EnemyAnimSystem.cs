using System.Text.RegularExpressions;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimSystem : ComponentSystem
{
    private EntityQuery _characterAnimQuery;


    protected override void OnCreate()
    {
        _characterAnimQuery = GetEntityQuery(
            ComponentType.ReadOnly<EnemyAnimateData>(),
            ComponentType.ReadOnly<AIAgent>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_characterAnimQuery).ForEach(
            (Entity entity, BehaviourManager behManager,  ref EnemyAnimateData animateData, ref AIAgent AIAgent) =>
            {
                if (behManager.animator != null && behManager.agent != null)
                {
                    if (animateData.isAttack)
                    {
                        behManager.animator.SetTrigger("Attak");
                        animateData.isAttack = false;
                    }
                    else
                    {
                        behManager.animator.SetFloat("Speed", behManager.agent.velocity.magnitude / behManager.agent.speed);
                    }
                }
                else
                    Debug.Log("Не назначен Аниматор или AI агент: " + behManager.animator + " " + behManager.agent);              
                    
            });
    }
}
