using Unity.Entities;
using UnityEngine;

public class BuffFastRechange : MonoBehaviour, ITempBuff
{
    private Entity _entity;
    private EntityManager _entityManager;
    private float _startDelay = 0;
    private float _newDelay;

    public void Calculate(Entity entyty, EntityManager entityManager)
    {
        _entity = entyty;
        _entityManager = entityManager;

        ShootData shootData = _entityManager.GetComponentData<ShootData>(_entity);
        if (_startDelay == 0)
            _startDelay = shootData.DelayShoot;
        shootData.DelayShoot = Mathf.Max(0.1f, _newDelay);
        _entityManager.SetComponentData<ShootData>(_entity, shootData);
    }

    public void CancelAction()
    {
        ShootData shootData = _entityManager.GetComponentData< ShootData>(_entity);
        shootData.DelayShoot = _startDelay;
        _entityManager.SetComponentData<ShootData>(_entity, shootData);

    }

    public void SetParameter(float val)
    {
        _newDelay = val;
    }        
}
