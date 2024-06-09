using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    protected bool isDontDestroy;

    //Property
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;

                if (_instance == null)
                {
                    //Create Object
                    string typeName = typeof(T).Name;
                    GameObject obj = new GameObject(typeName);

                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    public virtual void Init()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

            Debug.Log(transform.name + " is Destroy");
        }
        else
        {
            _instance = this as T;
            if (isDontDestroy)
            {
                DontDestroyOnLoad(gameObject);
            }

            Debug.Log(transform.name + " is Init");
        }

    }
}
