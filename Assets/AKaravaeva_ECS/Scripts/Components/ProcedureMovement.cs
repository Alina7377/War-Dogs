using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ProcedureMovement : MonoBehaviour, IAbilityTarget, IDestroyBuff
{
    [HideInInspector] public List<GameObject> Targets { get; set; }
    [HideInInspector]public bool IsCanDestroy { get; set; }

    [SerializeField] private Transform _trapObject;
    [SerializeField] private float _minPointY;
    private float _maxPointY;
    [SerializeField] private float _rateOfFall;
    [SerializeField] private float _rateOfLifting;

    private bool _canActivate = true;

    private void Start()
    {
        if (_trapObject != null)
            _maxPointY = _trapObject.position.y;
        else
            Debug.Log("Не назначен перемещаемый объект");
    }

    public void Execute()
    {
        foreach (var target in Targets)
        {
            if (target.TryGetComponent<CharacterHealth>(out CharacterHealth caracterHelth) && _canActivate)
            {
                ActivateTrap();
            }
        }
    }

    private void ActivateTrap()
    {
        if (_trapObject == null)
        {
            Debug.Log("Не назначен перемещаемый объект");
            return;
        }
        _canActivate = false;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_trapObject.DOMoveY(_minPointY, _rateOfFall));
        sequence.AppendInterval(2);
        sequence.Append(_trapObject.DOMoveY(_maxPointY, _rateOfLifting).OnComplete(ReloadTrap));
    }

    private void ReloadTrap()
    {
        _canActivate = true;
    }
}
