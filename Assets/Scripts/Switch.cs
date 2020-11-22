using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public List<GameObject> objectsToDisable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i = 0; i < objectsToDisable.Count; ++i)
        {
            objectsToDisable[i].SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
