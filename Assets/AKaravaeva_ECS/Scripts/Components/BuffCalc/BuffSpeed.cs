using Unity.Entities;
using UnityEngine;

public class BuffSpeed : MonoBehaviour, ITempBuff
{
    private Entity _entity;
    private EntityManager _entityManager;
    private float _startSpeed = 0;
    private float _addSpeed;

    public void Calculate(Entity entyty, EntityManager entityManager)
    {
        _entity = entyty;
        _entityManager = entityManager;

        ControlData controlData = _entityManager.GetComponentData<ControlData>(_entity);
        if (_startSpeed == 0)
            _startSpeed = controlData.Speed;
        controlData.Speed = _startSpeed + _addSpeed;
        _entityManager.SetComponentData<ControlData>(_entity, controlData);
    }

    public void CancelAction()
    {
        ControlData controlData = _entityManager.GetComponentData<ControlData>(_entity);
        controlData.Speed -= _addSpeed;
        _entityManager.SetComponentData<ControlData>(_entity, controlData);

    }

    public void SetParameter(float val)
    {
        _addSpeed = val;
    }        
}
