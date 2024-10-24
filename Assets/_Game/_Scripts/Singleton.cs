using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance => instance;
    [SerializeField] private bool dontDestroyOverLoad;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            if (dontDestroyOverLoad)
            {
                DontDestroyOnLoad(gameObject);
            }

            CustomAwake();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void CustomAwake() { }
}
