using System.Collections.Generic;
using UnityEngine;

public class FastRechangeItemAbility : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private float _delayRechange;
    [SerializeField] private float _duration;
    public List<GameObject> Targets { get; set; } = new List<GameObject>();

    public void Execute()
    {
        foreach (var target in Targets)
        {

            if (target.TryGetComponent<CharacterData>(out CharacterData characterData))
            {
                BuffFastRechange buffRechange = target.GetComponent<BuffFastRechange>();
                if (buffRechange == null)
                    buffRechange = target.AddComponent<BuffFastRechange>();
                buffRechange.SetParameter(_delayRechange);

                characterData.AddTempBuff(buffRechange, _duration);
            }
        }
        Destroy(this.gameObject);
    }

    
}
