using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public class ApplyDemage : MonoBehaviour,IAbilityTarget, IDestroyBuff
{
    public int Damage = 5;
    public float recharge = 2;

    private bool _isActive = true;

    public List<GameObject> Targets   { get; set; }

    public bool IsCanDestroy { get; set ; }

    private EntityManager _entityManager;

    private void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        IsCanDestroy = false;
    }

    public void Execute()
    {
        if (_isActive)
            StartCoroutine(Attack(recharge));
       
    }

    private IEnumerator Attack(float time)
    {

        foreach (var target in Targets)
        {
            if (target.TryGetComponent<CharacterHealth>(out CharacterHealth caracterHelth))
            {
                var entity = caracterHelth.healthEntity;
                _entityManager.SetComponentData<DamageData>(entity, new DamageData { Amount = Damage });
                IsCanDestroy = true;
            }
        }

        _isActive = false;
        yield return new WaitForSeconds(time);
        
        _isActive = true;

    }



}


