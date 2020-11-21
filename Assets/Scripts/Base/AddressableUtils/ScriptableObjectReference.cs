using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[System.Serializable]
public class ScriptableObjectReference<T> : AssetReferenceT<T> where T:ScriptableObject
{
    public ScriptableObjectReference(string guid) : base(guid)
    {
    }

    public override bool ValidateAsset(Object obj)
    {
        ScriptableObject so = obj as T;

        if (so != null)
        {
            return true;
        }
        return false;
    }
}