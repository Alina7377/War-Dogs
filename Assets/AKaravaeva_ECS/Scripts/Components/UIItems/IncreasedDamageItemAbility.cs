using System.Collections.Generic;
using UnityEngine;

public class IncreasedDamageItemAbility : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private float _addDemage;
    [SerializeField] private float _duration;
    public List<GameObject> Targets { get; set; } = new List<GameObject>();

    public void Execute()
    {
        foreach (var target in Targets)
        {

            if (target.TryGetComponent<CharacterData>(out CharacterData characterData))
            {
                BuffIncreasedDamage buffDamage = target.GetComponent<BuffIncreasedDamage>();
                if (buffDamage == null)
                    buffDamage = target.AddComponent<BuffIncreasedDamage>();
                buffDamage.SetParameter(_addDemage);

                characterData.AddTempBuff(buffDamage, _duration);
            }
        }
        Destroy(this.gameObject);
    }

    
}
