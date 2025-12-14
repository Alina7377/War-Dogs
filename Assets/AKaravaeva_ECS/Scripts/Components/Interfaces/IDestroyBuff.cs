
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public interface IDestroyBuff: IAbility
{
    public bool IsCanDestroy { get; set; }

}
