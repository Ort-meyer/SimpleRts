using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void SetX(this Transform transform, float x)
    {
        Vector3 newPosition =
           new Vector3(x, transform.position.y, transform.position.z);

        transform.position = newPosition;
    }
}

public static class DictionaryExtensions
{
    /* Adds val to list. If list doesn't exist, create empty and then add */
    public static void AddToList<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictionary, TKey key, TValue val)
    {
        if (!dictionary.ContainsKey(key))
        {
            dictionary.Add(key, new List<TValue>());
        }
        dictionary[key].Add(val);
    }
}

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    //Returns the instance of this singleton.
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    Debug.LogError("An instance of " + typeof(T) +
                       " is needed in the scene, but there is none.");
                }
            }

            return instance;
        }
    }
}