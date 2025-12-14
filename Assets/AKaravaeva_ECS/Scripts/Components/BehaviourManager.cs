using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourManager : MonoBehaviour, IConvertGameObjectToEntity
{
    public List<MonoBehaviour> behavioursObject;
    public List<IBehaviour> behaviours = new List<IBehaviour>();
    public IBehaviour actualBehaviour;
    [HideInInspector] public Transform targetPlayer;
    public NavMeshAgent agent;
    public Animator animator;
    [HideInInspector] public Entity entityObject;

    private void Start()
    {
        foreach (var behObject in behavioursObject)
        {
            if (behObject is IBehaviour behave)
            {
                behaviours.Add(behave);
            }
            else
            {
                Debug.LogError("Ошибка получения IBehaviour");
            }
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<AIAgent>(entity);
        dstManager.AddComponent<EnemyAnimateData>(entity);
        entityObject = entity;
    }

}

public struct AIAgent : IComponentData 
{
   
}

public struct EnemyAnimateData : IComponentData
{
    public bool isAttack;

}
