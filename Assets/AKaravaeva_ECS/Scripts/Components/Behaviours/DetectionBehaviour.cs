using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectionBehaviour : MonoBehaviour,IBehaviour
{
    [SerializeField] private float _basePriority = 0.5f;
    [SerializeField] private float _distanceDetected;

    private GameObject[] _players;
    private BehaviourManager _behaviourManager;

    private void Start()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");
        _behaviourManager = gameObject.GetComponent<BehaviourManager>();
    }

    public void Behave()
    {
        foreach (GameObject player in _players)
        {
            if (player == null) continue;
            if (Vector3.Distance(transform.position, player.transform.position) <= _distanceDetected)
            {
                _behaviourManager.targetPlayer = player.transform;
            }
        }
    }

    public float Evalute()
    {
        if (_behaviourManager.targetPlayer == null)
            return _basePriority;
        return 0f;

    }
    
}
