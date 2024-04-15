using UnityEngine;
using RoR2;

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

        public void OnGUI()
        {
            GUI.Box(new Rect(0f, 0f, 300f, 500f), "");
            //GUI.Label(new Rect(0f, 0f, 100f, 30f), "Label.");
        }
        public void Start()
        {
            UpdateLocalPlayer();
            _Master = FindObjectOfType<CharacterMaster>();
            _NetworkUser = FindObjectOfType<NetworkUser>(); 
            _TeamManager = FindObjectOfType<TeamManager>();
            _Teleporter = FindObjectOfType<TeleporterInteraction>();
            UnlockAll();
        }

        public void Update()
        {
            UpdateLocalPlayer();
            _Body = LocalPlayer.GetBody();
            _Body.baseAttackSpeed = 50f;
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
            /*foreach (LocalUser localUser in LocalUserManager.readOnlyLocalUsersList)
            {
                if (localUser.currentNetworkUser != null && localUser.currentNetworkUser.isLocalPlayer && localUser.cachedMasterController != null && localUser.cachedMasterController.master != null)
                {
                    RoRMod.LocalPlayer = localUser.cachedMasterController.master;
                    break;
                }
            }*/
        }
    }
}
