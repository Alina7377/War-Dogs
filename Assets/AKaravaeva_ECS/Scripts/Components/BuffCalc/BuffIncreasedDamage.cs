using Unity.Entities;
using UnityEngine;

public class BuffIncreasedDamage : MonoBehaviour, ITempBuff
{
    private Entity _entity;
    private EntityManager _entityManager;
    private float _startAddDemage = -1;
    private float _addDemage;

    public void Calculate(Entity entyty, EntityManager entityManager)
    {
        _entity = entyty;
        _entityManager = entityManager;

        ShootData shootData = _entityManager.GetComponentData<ShootData>(_entity);
        if (_startAddDemage == -1)
            _startAddDemage = shootData.Demadge;
        shootData.Demadge = _addDemage;
        _entityManager.SetComponentData<ShootData>(_entity, shootData);
    }

    public void CancelAction()
    {
        ShootData shootData = _entityManager.GetComponentData< ShootData>(_entity);
        shootData.Demadge = _startAddDemage;
        _entityManager.SetComponentData<ShootData>(_entity, shootData);

    }

    public void SetParameter(float val)
    {
        _addDemage = val;
    }        
}
