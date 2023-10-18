using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameSystemConfig", order = 1)]
public class GameSystems : ScriptableObject
{
    [InfoBox("This Scriptable objects require to have SystemData as parent class")]
    [ReorderableList]
    [SerializeField] public List<ScriptableObject> _gameSystems;

    [InfoBox("Only is allowed to enable one GameSystem at once, enabling this GameSystems will disable the others")]
    [DisableIf("inUse")]
    public bool inUse = false;

    public void OnValidate()
    {
        var instances = Resources.FindObjectsOfTypeAll<GameSystems>();
        if (instances.Length == 1)
        {
            inUse = true;
        }

        if (inUse)
        {
            foreach (var instance in instances)
            {
                if (instance != this)
                {
                    instance.inUse = false;
                }
            }

            for (int i = 0; i < _gameSystems.Count; i++)
            {
                try
                {
                    var gameSystem = (ISystemData) _gameSystems[i];
                }
                catch (InvalidCastException)
                {
                    Debug.LogErrorFormat("Invalid cast");
                    _gameSystems[i] = null;
                }
            }
        }
    }
    }