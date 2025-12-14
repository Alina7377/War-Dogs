
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public interface IAbilityTarget: IAbility
{
    List<GameObject> Targets { get; set; }


}
