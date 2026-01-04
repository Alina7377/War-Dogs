using Unity.Entities;
using UnityEngine;

public class BuffArmor : MonoBehaviour, ITempBuff
{
    private Entity _entity;
    private EntityManager _entityManager;
    private float _startArmor = -1;
    private float _addArmor;

    public void Calculate(Entity entyty, EntityManager entityManager)
    {
        _entity = entyty;
        _entityManager = entityManager;

        HealthData healthData = _entityManager.GetComponentData<HealthData>(_entity);
        if (_startArmor == -1)
            _startArmor = healthData.DamageReduction;
        healthData.DamageReduction = _startArmor + _addArmor;
        _entityManager.SetComponentData<HealthData>(_entity, healthData);
    }

    public void CancelAction()
    {
        HealthData healthData = _entityManager.GetComponentData<HealthData>(_entity);
        healthData.DamageReduction -= _addArmor;
        _entityManager.SetComponentData<HealthData>(_entity, healthData);
    }

    public void SetParameter(float val)
    {
        _addArmor = val;
    }        
}
