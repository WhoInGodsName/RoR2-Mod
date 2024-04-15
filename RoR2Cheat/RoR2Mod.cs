using UnityEngine;
using RoR2;

namespace RoR2Mod
{
    //Main cheat class
    internal class RoRMod : MonoBehaviour
    {
        CharacterBody _Body;
        CharacterMaster _Master;
        NetworkUser _NetworkUser;
        TeamManager _TeamManager;
        TeleporterInteraction _Teleporter;

        public void OnGUI()
        {
            GUI.Box(new Rect(0f, 0f, 300f, 500f), "");
            //GUI.Label(new Rect(0f, 0f, 100f, 30f), "Label.");
        }
        public void Start()
        {

            _Body = FindObjectOfType<CharacterBody>();
            _Master = FindObjectOfType<CharacterMaster>();
            _NetworkUser = FindObjectOfType<NetworkUser>();
            _TeamManager = FindObjectOfType<TeamManager>();
            _Teleporter = FindObjectOfType<TeleporterInteraction>();
        }

        public void Update()
        {
            _Body.sprintingSpeedMultiplier = 50f;
        }

    }
}
