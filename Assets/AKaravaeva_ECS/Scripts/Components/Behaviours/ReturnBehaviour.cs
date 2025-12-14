using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReturnBehaviour : MonoBehaviour,IBehaviour
{
    [SerializeField] private float _basePriority = 1.1f;
    [SerializeField] private float _maxDistanceForStartPoint;

    private BehaviourManager _behaviourManager;
    private NavMeshAgent _agent;
    private Vector3 _startPoint;


    private void Start()
    {
        _behaviourManager = gameObject.GetComponent<BehaviourManager>();
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _startPoint = transform.position;
    }

    public void Behave()
    {
        if (_behaviourManager.targetPlayer != null)
        {
            _behaviourManager.targetPlayer = null;
        }
        _agent.SetDestination(_startPoint);
    }

    public float Evalute()
    {  
        return Mathf.Min(Vector3.Distance(_startPoint, transform.position) / _maxDistanceForStartPoint, _basePriority);
    }
    
}
