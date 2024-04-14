using UnityEngine;
using RoR2;

namespace RoR2Mod
{ 
    public class Loader : MonoBehaviour
    {
        public static void Init()
        {
            Loader.Load = new GameObject();
            Loader.Load.AddComponent<RoRMod>();
            UnityEngine.Object.DontDestroyOnLoad(Loader.Load);
        }

        public static void Unload()
        {
            _Unload();
        }

        private static void _Unload()
        {
            GameObject.Destroy(Load);
        }

        private static GameObject Load;
    }
}
