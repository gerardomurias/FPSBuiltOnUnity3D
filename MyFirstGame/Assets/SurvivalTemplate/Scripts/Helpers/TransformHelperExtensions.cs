using UnityEngine;

public static class TransformHelperExtensions
{
    public static Transform FindDeepChild(this Transform parentTransform, string childName)
    {
        foreach (Transform child in parentTransform)
        {
            if (child.name == childName)
                return child;

            var result = child.FindDeepChild(childName);

            if (result != null)
                return result;
        }

        return null;
    }
}