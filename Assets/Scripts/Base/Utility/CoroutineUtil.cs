using System.Collections;
using UnityEngine;
using Base.Tools;

public class CoroutineUtil : MonoBehaviourSingleton<CoroutineUtil>
{
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    static public Coroutine StartStaticCoroutine(IEnumerator coroutine)
    {
        return Instance.StartCoroutine(coroutine);
    }

    static public void StopStaticCoroutine(IEnumerator coroutine)
    {
        Instance.StopCoroutine(coroutine);
    }

    public IEnumerator TimedDestroy(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }

    public IEnumerator TimedRelease(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}
