using System;
using UnityEngine;
using RoR2;
using RoR2Cheat;

namespace RoR2Mod
{
    //Main cheat class
    internal class RoRMod : MonoBehaviour
    {
        CharacterBody _Body;
        public static CharacterMaster LocalPlayer = null;
        NetworkUser _NetworkUser;
        TeamManager _TeamManager;
        TeleporterInteraction _Teleporter;
        CharacterVars characterVars = new CharacterVars();

        //Toggles for menu
        bool maxFireRate = false;
        bool godMode = false;

        //Player Speed
        string speedLabel = "   > Character Speed <";
        bool increaseSpeed = false;
        bool decreaseSpeed = false;

        int x = 1;

        public void OnGUI()
        {
            Render.Begin("Risk of Tears", 4f, 1f, 180f, 500f, 10f, 20f, 2f);
            //GUI.Box(new Rect(0f, 0f, 300f, 500f), godMode.ToString());
            if (Render.Button("Toggle Firerate")) { maxFireRate = true; }
            if (Render.Button("Toggle Godmode")) { godMode = !godMode; }
            Render.Label(speedLabel);
            if (Render.Button("+ Speed")) { increaseSpeed = true;}
            if (Render.Button("- Speed")) { decreaseSpeed = true; }
        }
        public void Start()
        {
            UpdateLocalPlayer();
            _NetworkUser = FindObjectOfType<NetworkUser>(); 
            _TeamManager = FindObjectOfType<TeamManager>();
            _Teleporter = FindObjectOfType<TeleporterInteraction>();
        }

        public void Update()
        {
            string y = x.ToString();
            UpdateLocalPlayer();
            _Body = LocalPlayer.GetBody();
            if(maxFireRate == true)
            {
                _Body.baseAttackSpeed = 50f;
            }
            if(godMode == true)
            {
                characterVars.health = _Body.healthComponent.health;
                _Body.healthComponent.godMode = true;
                _Body.healthComponent.health = 999999999999f;
            }
            else if(godMode == false)
            {
                _Body.healthComponent.godMode = false;
            }

            //Character speed toggle up and down.
            if(increaseSpeed == true)
            {
                increaseSpeed = !increaseSpeed;
                _Body.baseMoveSpeed += 1f;
            }
            if (decreaseSpeed == true)
            {
                decreaseSpeed = !decreaseSpeed;
                _Body.baseMoveSpeed -= 1f;
            }

        }

        public void UnlockAll()
        {
            UserProfile userProfile = LocalUserManager.GetFirstLocalUser().userProfile;

            foreach (ItemIndex item in ItemCatalog.allItems)
            {
                userProfile.DiscoverPickup(PickupCatalog.FindPickupIndex(item));
            }
            foreach (EquipmentIndex equipmentIndex in EquipmentCatalog.allEquipment)
            {
                userProfile.DiscoverPickup(PickupCatalog.FindPickupIndex(equipmentIndex));
            }

            UserAchievementManager UAM = AchievementManager.GetUserAchievementManager(RoR2.LocalUserManager.GetFirstLocalUser());
            foreach (AchievementDef AD in AchievementManager.allAchievementDefs)
            {
                UAM.GrantAchievement(AD);
            }
        }

        public static void UpdateLocalPlayer()
        {
            if (RoRMod.LocalPlayer != null)
            {
                return;
            }
            LocalPlayer = LocalUserManager.GetFirstLocalUser().cachedMasterController.master;
            /*for(int i = 0; i < LocalUserManager.readOnlyLocalUsersList.Count; i++) 
            {
                if (LocalUserManager.readOnlyLocalUsersList[i].currentNetworkUser != null 
                    && LocalUserManager.readOnlyLocalUsersList[i].currentNetworkUser.isLocalPlayer 
                    && LocalUserManager.readOnlyLocalUsersList[i].cachedMasterController != null 
                    && LocalUserManager.readOnlyLocalUsersList[i].cachedMasterController.master != null)
                {
                    RoRMod.LocalPlayer = LocalUserManager.readOnlyLocalUsersList[i].cachedMasterController.master;
                    break;
                }
            }*/
        }
    }
}
