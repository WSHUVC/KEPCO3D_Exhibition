using UnityEngine;
namespace WSH.Util
{
    public static class UnityUtil
    {
        public static T TryAddComponent<T>(this GameObject target) where T : Component
        {
            if (target.TryGetComponent<T>(out var result))
                return result;

            return target.AddComponent<T>();
        }
    }
}