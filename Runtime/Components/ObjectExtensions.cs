using UnityEngine;

namespace UsefulExtensions.Components
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Retrieves an existing component of type <typeparamref name="T"/> from the given GameObject.
        /// If the component does not exist, a new instance of the component is added to the GameObject and returned.
        /// </summary>
        /// <param name="gameObject">The GameObject on which the component is retrieved or added.</param>
        /// <typeparam name="T">The type of the component to retrieve or add.</typeparam>
        /// <returns>The existing or newly added component of type <typeparamref name="T"/>.</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }

        /// <summary>
        /// Destroys the specified object, using the appropriate method based on whether the application is in play mode.
        /// </summary>
        /// <param name="obj">The object to be destroyed.</param>
        /// <typeparam name="T">The type of the object to be destroyed, which must inherit from UnityEngine.Object.</typeparam>
        public static void Destroy<T>(T obj) where T : Object
        {
            if (Application.isPlaying)
            {
                Object.Destroy(obj);
            }
            else
            {
                Object.DestroyImmediate(obj);
            }
        }
    }
}
