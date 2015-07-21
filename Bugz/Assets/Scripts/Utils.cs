using UnityEngine;
using System.Collections.Generic;

class Utils
{
    //Gets all the objects in the scene with type T ( this includes interfaces :) )
    public static List<T> GetAll<T>()
    {
        List<T> result = new List<T>();
        GameObject[] gameListeners = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject go in gameListeners)
        {
            T comp = go.GetComponent<T>();
            if (comp != null) result.Add(comp);
        }
        return result;
    }
}