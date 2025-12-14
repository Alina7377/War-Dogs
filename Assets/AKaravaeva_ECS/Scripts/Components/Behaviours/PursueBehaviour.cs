using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursueBehaviour : MonoBehaviour,IBehaviour
{
    [SerializeField] private float _maxDistancePursure;

    private BehaviourManager _behaviourManager;
    private NavMeshAgent _agent;


    private void Start()
    {
        _behaviourManager = gameObject.GetComponent<BehaviourManager>();
        _agent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void Behave()
    {
        if (_behaviourManager.targetPlayer != null)
            _agent.SetDestination(_behaviourManager.targetPlayer.transform.position);
    }

    public float Evalute()
    {
        if (_behaviourManager.targetPlayer != null)
            return _maxDistancePursure / Vector3.Distance(_behaviourManager.targetPlayer.transform.position, transform.position);
        return 0f;

    }
    
}
