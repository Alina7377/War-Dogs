using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public class GiveItemPicUp : MonoBehaviour,IAbilityTarget, IDestroyBuff, IItem
{
    [SerializeField] private GameObject _uiItem;
    private bool _isActive = true;

    public List<GameObject> Targets   { get; set; }

    public bool IsCanDestroy { get; set ; }

    public GameObject UIItem => _uiItem;


    private void Start()
    {
        IsCanDestroy = false;
    }

    public void Execute()
    {
       
        if (_isActive)
        {
            _isActive = false;
            foreach (var target in Targets)
            {
                if (target.TryGetComponent<CharacterData>(out CharacterData caracterData))
                {
                    GameObject newItem = Instantiate(UIItem, caracterData.inventaryGrouproot.transform);
                    newItem.GetComponent<IAbilityTarget>().Targets.Add(target);
                    IsCanDestroy = true;
                }
            }            
        }
       
    }
}


