using System;
using UnityEngine;

public static class Util
{
    public static T GetOrAddCompoenet<T>(GameObject go) where T : UnityEngine.Component
    {
        T compoent = go.GetComponent<T>();
        if (compoent == null)
            compoent = go.AddComponent<T>();

        return compoent;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive)
        {
            for (int i = 0; i < go.transform.childCount; ++i)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }
        return null;
    }
    public static bool NullCheck<T>(T Value)
    {
        if (Value == null)
        {
            Type type = typeof(T);
            Debug.LogError($"{type.ToString()} Is Null");
            return false;
        }
        return true;
    }

}
