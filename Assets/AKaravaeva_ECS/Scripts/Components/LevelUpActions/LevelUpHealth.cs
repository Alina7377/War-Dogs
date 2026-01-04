using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class LevelUpHealth : MonoBehaviour, ILevelUp
{

    [SerializeField] private int _minLevel; 
    [SerializeField] private CharacterHealth _characterHealth;

    private Entity _characterEntity;
    private EntityManager _entityManager;



    private void Start()
    {
        if (_characterHealth == null)
        {
            Debug.Log("Не назначен компонент здоровья");
            return;
        }
        _characterEntity = _characterHealth.healthEntity;
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    public void LevelUp(CharacterData data, int currentLevel)
    {
        if (MinLevel > currentLevel) return;

        var healthData = _entityManager.GetComponentData<HealthData>(_characterEntity);

        healthData.MaxHealth += 20;
        healthData.Health = healthData.MaxHealth;

        _entityManager.SetComponentData<HealthData>(_characterEntity, healthData);
    }

    public int MinLevel => _minLevel;
}
