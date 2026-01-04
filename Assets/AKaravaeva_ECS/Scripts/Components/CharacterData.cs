using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CharacterData : MonoBehaviour,IConvertGameObjectToEntity
{
    [SerializeField] private List<MonoBehaviour> _levelUpActions;
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private int _score = 0;
    [SerializeField] private int _scoreToNextLevel = 20;

    public GameObject inventaryGrouproot;

    private List<TempBuffData> _tempBuffData= new List<TempBuffData>(); // Убрать New из назавания

    private Entity _entity;
    private EntityManager _entityManager;

    private int FindBuff(MonoBehaviour component)
    {
        for (int i = 0; i < _tempBuffData.Count; i++)
        {
            if (_tempBuffData[i].Component == component)
            {
                return i;
            }
        }
        return -1;
    }

    private void LevelUP()
    {
        _currentLevel++;
        _scoreToNextLevel *= 2;
        foreach (var action in _levelUpActions)
        {
            if (!(action is ILevelUp levelUp)) return;
            levelUp.LevelUp(this, _currentLevel);
        }
    }
    
    public int CountBuff()
    {
        return _tempBuffData.Count;
    }

    public void AddScore(int newScore) 
    {
        _score += newScore;
        if (_score >= _scoreToNextLevel) LevelUP();        
    }


    public void AddTempBuff(MonoBehaviour component, float duration)
    {
        if (component is ITempBuff tempBuff)
        {
            int index = FindBuff(component);
            TempBuffData newComponent = new TempBuffData
            {
                Component = component,
                DurationActions = duration,
                StartTime = Time.time
            };
            if (index == -1)
            {
                _tempBuffData.Add(newComponent);
            }
            else
            {
                _tempBuffData[index] = newComponent;
            }
            tempBuff.Calculate(_entity, _entityManager);           
        }
    }


    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _entity = entity;
        _entityManager = dstManager;
    }

    public void CheackTimeBuff() 
    {
        List<TempBuffData> deleteRecordsBuff = new List<TempBuffData>();
        for (int i = 0; i < _tempBuffData.Count; i++)
        {            
            if (Time.time - _tempBuffData[i].StartTime >= _tempBuffData[i].DurationActions)
            {
                if (_tempBuffData[i].Component is ITempBuff tempBuff)
                    tempBuff.CancelAction();
                Destroy(_tempBuffData[i].Component);        
                deleteRecordsBuff.Add(_tempBuffData[i]);
            }
        }
        foreach (TempBuffData record in deleteRecordsBuff)
        {
            _tempBuffData.Remove(record);
        }
        deleteRecordsBuff.Clear();

       
    }
}

public struct TempBuffData
{
    public MonoBehaviour Component;
    public float StartTime;
    public float DurationActions;
}
