using UnityEngine;
using Zenject;

public class ConfigurationInstaller : MonoInstaller
{
    [SerializeField] private bool _isUseSO;
    [SerializeField] private SOPlayerCharacteristics _playerCharacteristic;

    public override void InstallBindings()
    {
        if (_isUseSO && _playerCharacteristic != null)
        {
            Container.Bind<IGameConfig>().To<PlayerCharactersInSO>().AsSingle().WithArguments(_playerCharacteristic);
        }
        else
            Container.Bind<IGameConfig>().To<DummyPlayerCharacterisctics>().AsSingle();

    }
}