using System.Collections.Generic;
using UnityEngine;

public class ArmorItemAbility : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private float _resistance;
    [SerializeField] private float _duration;
    public List<GameObject> Targets { get; set; } = new List<GameObject>();

    public void Execute()
    {
        foreach (var target in Targets)
        {

            if (target.TryGetComponent<CharacterData>(out CharacterData characterData))
            {
                BuffArmor buffArmor = target.GetComponent<BuffArmor>();
                if (buffArmor == null)
                    buffArmor = target.AddComponent<BuffArmor>();
                buffArmor.SetParameter(_resistance);

                characterData.AddTempBuff(buffArmor, _duration);
            }
        }
        Destroy(this.gameObject);
    }

    
}
