using UnityEngine;
using RoR2;
using RoR2Cheat;

namespace RoR2Mod
{
    //Main cheat class
    internal class RoRMod : MonoBehaviour
    {
        CharacterBody _Body;
        CharacterMaster _Master;
        LocalUser _LocalUser;
        public static CharacterMaster LocalPlayer = null;
        NetworkUser _NetworkUser;
        TeamManager _TeamManager;
        TeleporterInteraction _Teleporter;

        //Toggles for menu
        bool maxFireRate = false;

        public void OnGUI()
        {
            Render.Begin("MEOWWW", 4f, 10f, 180f, 500f, 4f, 20f, 2f);
            //GUI.Box(new Rect(0f, 0f, 300f, 500f), "");
            if (Render.Button("Toggle Firerate")) { maxFireRate = true; }
        }
        public void Start()
        {
            UpdateLocalPlayer();
            _Master = FindObjectOfType<CharacterMaster>();
            _NetworkUser = FindObjectOfType<NetworkUser>(); 
            _TeamManager = FindObjectOfType<TeamManager>();
            _Teleporter = FindObjectOfType<TeleporterInteraction>();
        }

        public void Update()
        {
            UpdateLocalPlayer();
            _Body = LocalPlayer.GetBody();
            if(maxFireRate == true)
            {
                _Body.baseAttackSpeed = 50f;
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
