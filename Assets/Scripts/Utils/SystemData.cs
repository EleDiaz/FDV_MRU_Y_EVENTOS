using UnityEngine;

public interface ISystemData
{
    void Init(GameObject gameObject);
}

public abstract class SystemData<T> : ScriptableObject, ISystemData where T : MonoBehaviour
{
    public void Init(GameObject gameObject)
    {
        T component = gameObject.AddComponent<T>();
        SetupComponent(component);
    }

    public abstract void SetupComponent(T component);
}