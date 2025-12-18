using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UpdateCharacterParams : MonoBehaviour
{
    [Inject]
    private IGameConfig _gameConfig;

    public void OnUpdateParams()
    {
        _gameConfig.UpdateInfo();
    }
}
