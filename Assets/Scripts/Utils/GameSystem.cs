using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    private static GameSystem _instance;

    private GameSystems _gameSystems;

    public static GameSystem Instance => _instance;

    [RuntimeInitializeOnLoadMethod]
    public static void AutoGenerate()
    {
        GameObject gameObject = new GameObject();
        _instance = gameObject.AddComponent<GameSystem>();
        gameObject.name = nameof(GameSystem);
        GameObject.DontDestroyOnLoad(gameObject);

        var instances = Resources.FindObjectsOfTypeAll<GameSystems>();
        if (instances.Length > 0)
        {
            foreach (var instance in instances)
            {
                if (instance.inUse)
                {
                    _instance._gameSystems = instance;
                    _instance.LoadAllSystems();
                    break;
                }
            }

            if (_instance._gameSystems == null)
            {
                Debug.LogErrorFormat("[GameSystem] None of GameSystem instances were set inUse");
            }
        }
        else if (instances.Length == 0)
        {
            Debug.LogErrorFormat("[GameSystem] No instance of GameSystems found!");
        }
    }

    public void LoadAllSystems()
    {
        foreach (var so in _instance._gameSystems._gameSystems)
        {
            ((ISystemData) so).Init(_instance.gameObject);
        }
    }
}
