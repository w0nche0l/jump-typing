using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityUtil
{
    public static void DestroyWithChildren(Transform transform)
    {
        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            Object.DestroyImmediate(child.gameObject);
        }

        Object.DestroyImmediate(transform.gameObject);
    }

}
