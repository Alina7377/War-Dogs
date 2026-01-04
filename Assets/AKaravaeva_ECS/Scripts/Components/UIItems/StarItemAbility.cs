using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarItemAbility : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private int _scoreCount;
    public List<GameObject> Targets { get; set; } = new List<GameObject>();

    public void Execute()
    {
        foreach (var target in Targets)
        {
            var characterData = target.GetComponent<CharacterData>();
            if (characterData != null)
                characterData.AddScore(_scoreCount);
        }
        Destroy(this.gameObject);
    }

    
}
