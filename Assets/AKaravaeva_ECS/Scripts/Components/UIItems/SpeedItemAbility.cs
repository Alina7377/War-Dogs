using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpeedItemAbility : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private float _val;
    [SerializeField] private float _duration;
    public List<GameObject> Targets { get; set; } = new List<GameObject>();

    public void Execute()
    {
        foreach (var target in Targets)
        {

            if (target.TryGetComponent<CharacterData>(out CharacterData characterData))
            {
                BuffSpeed buffSpeed = target.GetComponent<BuffSpeed>();
                if (buffSpeed == null)
                    buffSpeed = target.AddComponent<BuffSpeed>();
                buffSpeed.SetParameter(_val);

               characterData.AddTempBuff(buffSpeed, _duration);                
            }
        }
        Destroy(this.gameObject);
    }

    
}
