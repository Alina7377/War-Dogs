
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Utilits;


public class CollisionAbility : MonoBehaviour, IAbility,IConvertGameObjectToEntity
{
    public Collider Collider;

    public List<MonoBehaviour> collisionActions = new List<MonoBehaviour>();
    [SerializeField] private LayerMask _layerMask;
    public List<IAbilityTarget> collisionAbilities = new List<IAbilityTarget>();

    [HideInInspector] public List<Collider> colliders = new List<Collider>();


    private void Start()
    {
        foreach (var action in collisionActions)
        {
            if (action is IAbilityTarget ability)
            {
                collisionAbilities.Add(ability);
            }
            else
            {
                Debug.LogError("Ошибка получения IAbility");
            }
        }
    }

    public void Execute()
    {
        foreach (var action in collisionAbilities)
        {
            action.Targets = new List<GameObject>();
            colliders.ForEach(c =>
            {
                if (c != null)
                    action.Targets.Add(c.gameObject);
            });
            action.Execute();
        }
    }


    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        float3 position = gameObject.transform.position;
        switch (Collider)
        {
            case SphereCollider sphere:
                sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Sphere,
                    SphereCenter = sphereCenter,
                    SphereRadius = sphereRadius,
                    InitialTakeOOff = true,
                    LayerMask = _layerMask
                });
                break;
            case CapsuleCollider capsule:
                capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Capsule,
                    CapsuleStart = capsuleStart - position,
                    CapsuleEnd = capsuleEnd - position,
                    CapsuleRadius = capsuleRadius,
                    InitialTakeOOff = true,
                    LayerMask = _layerMask
                });
                break;
            case BoxCollider box:
                box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtents, out var boxOrientation);
                dstManager.AddComponentData(entity, new ActorColliderData 
                {
                    ColliderType = ColliderType.Box,
                    BoxCenter = boxCenter - position,
                    BoxHalfExtents = boxHalfExtents,
                    BoxOrientation = boxOrientation,
                    InitialTakeOOff = true,
                    LayerMask = _layerMask
                });
                break;
        }
    }


    public struct  ActorColliderData: IComponentData
    {
        public ColliderType ColliderType;
        public float3 SphereCenter;
        public float SphereRadius;
        public float3 CapsuleStart;
        public float3 CapsuleEnd;
        public float CapsuleRadius;
        public float3 BoxCenter;
        public float3 BoxHalfExtents;
        public quaternion BoxOrientation;
        public bool InitialTakeOOff;
        public LayerMask LayerMask;
    }

    public enum ColliderType 
    {
        Sphere = 0,
        Capsule = 1,
        Box = 2
    }
}
    
