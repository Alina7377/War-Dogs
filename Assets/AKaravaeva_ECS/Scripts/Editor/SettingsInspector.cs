using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConfigurationInstaller))]

public class SettingsInspector : Editor
{
    private SerializedProperty _useSO;
    private SerializedProperty _selectSetting;
    private SerializedProperty _indexSerialized;
    private string[] _settingsPlayerCharacteristic;
    private string[] _settingsName;
    private int _index = 0;

    public void OnEnable()
    {
        _useSO = serializedObject.FindProperty("_isUseSO");
        _selectSetting = serializedObject.FindProperty("_playerCharacteristic");
        _indexSerialized = serializedObject.FindProperty("_selectIndex");
        _index = _indexSerialized.intValue;
    }


     public override void OnInspectorGUI()
     {
        base.OnInspectorGUI();
        if (_useSO.boolValue)
        {
            _settingsPlayerCharacteristic = AssetDatabase.FindAssets("t:SOPlayerCharacteristics");
            _settingsName = new string[_settingsPlayerCharacteristic.Length];
            for (int i = 0; i < _settingsPlayerCharacteristic.Length; i++)
            {
                _settingsPlayerCharacteristic[i] = AssetDatabase.GUIDToAssetPath(_settingsPlayerCharacteristic[i]).ToString();   
                _settingsName[i] = Path.GetFileNameWithoutExtension(_settingsPlayerCharacteristic[i]);
            }

            if (_settingsName != null)
            {
                EditorGUILayout.LabelField("Select SO property");
                _index = EditorGUILayout.Popup(_index, _settingsName);
                EditorGUILayout.LabelField(_index.ToString());
                _selectSetting.objectReferenceValue = AssetDatabase.LoadAssetAtPath<SOPlayerCharacteristics>(_settingsPlayerCharacteristic[_index]);
                _indexSerialized.intValue  = _index;
                serializedObject.ApplyModifiedProperties();
            }
        }
     }
}
