
using System;

public interface IGameConfig 
{
    public event Action OnUpdate;

    public void UpdateInfo();
    public int GetHealth { get; }
    public float GetSpeed { get; }
    public float GetJerkSpeed { get; }
    public float GetShootDelay { get; }
}
