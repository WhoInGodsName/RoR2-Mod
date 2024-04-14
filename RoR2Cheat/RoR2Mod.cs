using UnityEngine;
using RoR2;

namespace RoR2Mod
{
    //Main cheat class
    internal class RoRMod : MonoBehaviour
    {
        CharacterBody _Body = FindObjectOfType<CharacterBody>();

        public void OnGUI()
        {
            GUI.Box(new Rect(0f, 0f, 100f, 30f), "");
            GUI.Label(new Rect(0f, 0f, 100f, 30f), "Label.");
        }
        public void Start()
        {

            _Body = FindObjectOfType<CharacterBody>();
        }

        public void Update()
        {
            _Body.sprintingSpeedMultiplier = 50f;
        }

    }
}
