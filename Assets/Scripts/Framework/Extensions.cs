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

public static class Vector3Extensions
{
    public static float GetDiffAngle2D(this Vector3 forward, Vector3 vectorToTarget)
    {
        Vector2 currentDirection = new Vector2(forward.x, forward.z);
        Vector2 targetDirection = new Vector2(vectorToTarget.x, vectorToTarget.z);

        float diffAngle = Vector2.Angle(currentDirection, targetDirection);

        // For some reason, this angle is absolute. Do some algebra magic to get negative angle
        Vector3 cross = Vector3.Cross(forward, vectorToTarget);
        if (cross.y < 0)
        {
            diffAngle *= -1;
        }
        return diffAngle;
    }
    public static Vector3 SetX(this Vector3 vec3, float x)
    {
        return new Vector3(x, vec3.y, vec3.z);
    }
    public static Vector3 SetY(this Vector3 vec3, float y)
    {
        return new Vector3(vec3.x, y, vec3.z);
    }
    public static Vector3 ZeroY(this Vector3 vec3)
    {
        return new Vector3(vec3.x, 0, vec3.z);
    }

    public static float[] GetVals(this Vector3 vec3)
    {
        return new float[] { vec3.x, vec3.y, vec3.z };
    }
}

public static class FloatExtensions
{
    public static float Sign(this float number)
    {
        if (number == 0)
        {
            return 0;
        }
        else
        {
            return Mathf.Sign(number);
        }
    }

    public static float Abs(this float number)
    {
        return Mathf.Abs(number);
    }

    // Limits the number to absLimit, with sign (i.e. -40 limited to 30 returns -30)
    public static float LimitWithSign(this float number, float absLimit)
    {
        if (Mathf.Abs(number) > absLimit)
        {
            number = Mathf.Sign(number) * absLimit;
        }
        return number;
    }

    public static float Sum(this float[] numbers)
    {
        float r = 0;
        foreach (float f in numbers)
        {
            r += f;
        }
        return r;
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

public static class Helpers
{
    // Taken from https://www.gamedev.net/forums/topic/107074-calculating-projectile-launch-angle-to-hit-a-target/?page=3
    // Returns angle necessary to hit the target with the given parameters
    public static float GetAngleToHit(float distanceToTarget, float heightDifference, float speed)
    {
        // Broken up for debugging purposes. Keeping it around for readability
        float u = speed;
        float us = Mathf.Pow(u, 2);
        float x = distanceToTarget;
        float xs = Mathf.Pow(x, 2);
        float y = heightDifference;
        float g = 9.81f;

        float xsus = xs / us;
        float gxsus = g * xsus;
        float part0 = y + 0.5f * gxsus;
        float part0point5 = us - 2 * g * part0;
        float part1 = u - Mathf.Sqrt(part0point5);
        float part2 = g * (x / u);

        float angle = Mathf.Atan(part1 / part2);
        if(float.IsNaN(angle)) // Out of range. This is kinda ugly but should work for most cases
        {
            return Mathf.Sign(heightDifference) * 45;
        }
        return angle * Mathf.Rad2Deg;


    }
}
