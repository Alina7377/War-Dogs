using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HealthItemAbility : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private int _healthCount;
    public List<GameObject> Targets { get; set; } = new List<GameObject>();

    private EntityManager _entityManager;

    private void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    public void Execute()
    {
        foreach (var target in Targets)
        {

            if (target.TryGetComponent<CharacterHealth>(out CharacterHealth caracterHelth))
            {
                var entity = caracterHelth.healthEntity;
                _entityManager.SetComponentData<DamageData>(entity, new DamageData { Amount = -_healthCount });
            }
        }
        Destroy(this.gameObject);
    }

    
}
