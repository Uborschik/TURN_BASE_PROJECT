using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class GameObjectExtensions
    {
        public static void HideInHierarchy(this GameObject gameObject)
        {
            gameObject.hideFlags = HideFlags.HideInHierarchy;
        }

        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (!component) component = gameObject.AddComponent<T>();

            return component;
        }

        public static T OrNull<T>(this T obj) where T : Object => obj ? obj : null;

        public static void DestroyChildren(this GameObject gameObject)
        {
            gameObject.transform.DestroyChildren();
        }

        public static void DestroyChildrenImmediate(this GameObject gameObject)
        {
            gameObject.transform.DestroyChildrenImmediate();
        }

        public static void EnableChildren(this GameObject gameObject)
        {
            gameObject.transform.EnableChildren();
        }

        public static void DisableChildren(this GameObject gameObject)
        {
            gameObject.transform.DisableChildren();
        }

        public static void ResetTransformation(this GameObject gameObject)
        {
            gameObject.transform.Reset();
        }

        public static string Path(this GameObject gameObject)
        {
            return "/" + string.Join("/", gameObject.GetComponentsInParent<Transform>().Select(t => t.name).Reverse().ToArray());
        }

        public static string PathFull(this GameObject gameObject)
        {
            return gameObject.Path() + "/" + gameObject.name;
        }

        public static void SetLayersRecursively(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            gameObject.transform.ForEveryChild(child => child.gameObject.SetLayersRecursively(layer));
        }
    }
}