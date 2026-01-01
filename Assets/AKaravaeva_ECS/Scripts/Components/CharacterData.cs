using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> _levelUpActions;
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private int _score = 0;
    [SerializeField] private int _scoreToNextLevel = 20;

    public GameObject inventaryGrouproot;


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

    public void AddScore(int newScore) 
    {
        _score += newScore;
        if (_score >= _scoreToNextLevel) LevelUP();        
    }

}
