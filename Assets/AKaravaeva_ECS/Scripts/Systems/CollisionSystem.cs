using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using static CollisionAbility;

public class CollisionSystem : ComponentSystem
{
    private EntityQuery _collisionQuery;

    private Collider[] _results = new Collider[50];

    protected override void OnCreate()
    {
        _collisionQuery = GetEntityQuery(ComponentType.ReadOnly<ActorColliderData>(),
                                         ComponentType.ReadOnly<Transform>());
    }


    protected override void OnUpdate()
    {
        var dstManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        Entities.With(_collisionQuery).ForEach(
            (Entity entity, CollisionAbility abilityCollision, ref ActorColliderData colliderData) =>
            {
                var gameObject = abilityCollision.gameObject;
                float3 position = gameObject.transform.position;
                Quaternion rotation = gameObject.transform.rotation;

                abilityCollision.colliders?.Clear();

                int size = 0;

                switch (colliderData.ColliderType) 
                {
                    case ColliderType.Sphere:
                        size = Physics.OverlapSphereNonAlloc(colliderData.SphereCenter, colliderData.SphereRadius, _results, colliderData.LayerMask);
                        break;
                    case ColliderType.Capsule:
                        var point1 = colliderData.CapsuleStart + position;
                        var point2 = colliderData.CapsuleEnd + position;
                        var center = (point1 + point2) / 2f;

                        point1 = (float3)(rotation * (point1 - center)) + center;
                        point2 = (float3)(rotation * (point2 - center)) + center;

                        size = Physics.OverlapCapsuleNonAlloc(point1, point2, colliderData.CapsuleRadius, _results, colliderData.LayerMask);
                        break;
                    case ColliderType.Box:
                        size = Physics.OverlapBoxNonAlloc(colliderData.BoxCenter + position, colliderData.BoxHalfExtents, _results, colliderData.BoxOrientation * rotation, colliderData.LayerMask);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (size > 0)
                {
                    foreach (var result in _results)
                    {
                        abilityCollision.colliders.Add(result);
                    }
                    abilityCollision.Execute();
                }

            });
    }
}
