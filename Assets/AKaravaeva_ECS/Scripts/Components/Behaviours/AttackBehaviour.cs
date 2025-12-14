using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AttackBehaviour : MonoBehaviour,IBehaviour
{
    [SerializeField] private float _basePriority = 5f;
    [SerializeField] private int _damage;
    [SerializeField] private float _respite;
    [SerializeField] private float _attackDistance;

    private BehaviourManager _behaviourManager;
    private bool _isCanAttack = true;

    private EntityManager _entityManager;

    private void Start()
    {
        _behaviourManager = gameObject.GetComponent<BehaviourManager>();
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    private IEnumerator Respite(float time)
    {
        yield return new WaitForSeconds(time);
        _isCanAttack = true;
    }


    public void Behave()
    {
        if (_behaviourManager.targetPlayer.TryGetComponent<CharacterHealth>(out CharacterHealth caracterHelth))
        {
            var entity = caracterHelth.healthEntity;
            _entityManager.SetComponentData<DamageData>(entity, new DamageData { Amount = _damage });
        }
        var animateData = _entityManager.GetComponentData<EnemyAnimateData>(_behaviourManager.entityObject);
        animateData.isAttack = true;
        _entityManager.SetComponentData(_behaviourManager.entityObject, animateData);
        _isCanAttack = false;
        StartCoroutine(Respite(_respite));
    }

    public float Evalute()
    {
        if (_behaviourManager.targetPlayer != null && _isCanAttack)
        {
            if (_attackDistance >= Vector3.Distance(_behaviourManager.targetPlayer.transform.position, transform.position))
                return _basePriority;
        }
        return 0f;

    }
    
}
