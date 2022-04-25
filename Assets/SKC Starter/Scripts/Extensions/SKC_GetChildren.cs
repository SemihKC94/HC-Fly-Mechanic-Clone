using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class SKC_GetChildren
{
    public static List<Transform> GetChildren(this Transform transform)
    {
        var children = new List<Transform>();

        // Iterate over all children in transform.
        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        return children;
    }
    public static Transform[] GetChildrenArray(this Transform transform)
    {
        var children = new Transform[transform.childCount];

        // Iterate over all children in transform.
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            children[i] = child;
        }
        return children;
    }
    public static List<Renderer> GetRenderers(this Transform transform)
    {
        var children = new List<Renderer>();

        // Iterate over all children in transform.
        foreach (Transform child in transform)
        {
            if(child.GetComponent<Renderer>()) children.Add(child.GetComponent<Renderer>());
        }

        return children;
    }

    public static Renderer[] GetRenderersArray(this Transform transform)
    {
        var temp = 0;

        // Iterate over all children in transform.
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<Renderer>()) temp++;
        }
        var children = new Renderer[temp];
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<Renderer>())
            {
                Renderer child = transform.GetChild(i).GetComponent<Renderer>();
                children[i] = child;
            }
        }
        return children;
    }
}
