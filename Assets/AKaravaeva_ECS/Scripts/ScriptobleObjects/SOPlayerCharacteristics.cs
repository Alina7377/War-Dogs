using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacters",
                 menuName = "PlayerCharacters", order = 50)]
public class SOPlayerCharacteristics : ScriptableObject
{
    public int startHealth;
    public float startSpeed;
    public float jerkSpeed;
    public float shootDelay;
}
