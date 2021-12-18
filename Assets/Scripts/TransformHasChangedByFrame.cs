using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHasChangedByFrame : MonoBehaviour
{
    IEnumerator coroutine;
 
    void Start()
    {
        StartCoroutine(EndOfFrameReset());
    }
 
    IEnumerator EndOfFrameReset()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            transform.hasChanged = false;//set to false so we can detect the next time it changes
        }
        yield return null;
    }
}