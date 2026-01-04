using Unity.Entities;
using UnityEngine;

public class BuffIncreasedDamage : MonoBehaviour, ITempBuff
{
    private Entity _entity;
    private EntityManager _entityManager;
    private float _startAddDamage = -1;
    private float _addDamage;

    public void Calculate(Entity entyty, EntityManager entityManager)
    {
        _entity = entyty;
        _entityManager = entityManager;

        ShootData shootData = _entityManager.GetComponentData<ShootData>(_entity);
        if (_startAddDamage == -1)
            _startAddDamage = shootData.Damadge;
        shootData.Damadge = _addDamage;
        _entityManager.SetComponentData<ShootData>(_entity, shootData);
    }

    public void CancelAction()
    {
        ShootData shootData = _entityManager.GetComponentData< ShootData>(_entity);
        shootData.Damadge = _startAddDamage;
        _entityManager.SetComponentData<ShootData>(_entity, shootData);

    }

    public void SetParameter(float val)
    {
        _addDamage = val;
    }        
}
