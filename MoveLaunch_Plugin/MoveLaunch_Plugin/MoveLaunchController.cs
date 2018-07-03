using KSP.UI.Screens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MoveLaunch
{
    public class MoveLaunchController : PartModule
    {
        public bool guiOpen = false;
        public bool launchChanged = false;

        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "LAUNCH SITE CHANGED"),
         UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool launchSiteChanged = false;

        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "KSC Beach"),
         UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool beach = false;

        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "KSC Island Beach"),
         UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool kscIslandBeach = false;

        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "Baikerbanur"),
         UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool baikerbanur = false;

        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "Pyramids"),
         UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool pyramids = false;

        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "KSC Harbor East"),
         UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool kscHarborEast = false;

        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "KSC Island New Harbor"),
         UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool kscIslandNewHarbor = false;
         
        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "KSC Isand Channel"),
         UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool kscIsandChannel = false;

        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "Missile Range 200"),
         UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool MissileRange200Island = false;

        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "Tirpitz Bay"),
         UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool TirpitzBay = false;

        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "Kerbini Atol"),
         UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool KerbiniAtol = false;

        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "Kerbini Island"),
         UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool KerbiniIsland = false;

        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "North Pole"),
         UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool NorthPole = false;

        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "South Pole"),
         UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool SouthPole = false;

        //[KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "Midway Island"),
        // UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool MidwayIsland = false;

        //[KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "Trunk Peninsula"),
        // UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool TrunkPeninsula = false;

        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "island Runway"),
         UI_Toggle(controlEnabled = false, scene = UI_Scene.All, disabledText = "", enabledText = "TRUE")]
        public bool islandRunway = false;

        public bool modify = true;
        
        public override void OnStart(StartState state)
        {
            if (HighLogic.LoadedSceneIsEditor)
            {
                part.force_activate();
                ResetCoords();
            }

            if (HighLogic.LoadedSceneIsFlight)
            {
                part.force_activate();
            }

            base.OnStart(state);
        }

        private void ScreenMsg(string msg)
        {
            ScreenMessages.PostScreenMessage(new ScreenMessage(msg, 4, ScreenMessageStyle.UPPER_CENTER));
        }

        public void ResetCoords()
        {
            islandRunway = false;
            TrunkPeninsula = false;
            KerbiniIsland = false;
            MidwayIsland = false;
            NorthPole = false;
            SouthPole = false;
            launchSiteChanged = false;
            kscIsandChannel = false;
            kscHarborEast = false;
            MissileRange200Island = false;
            kscIslandNewHarbor = false;
            TirpitzBay = false;
            KerbiniAtol = false;
            beach = false;
            kscIslandBeach = false;
            baikerbanur = false;
            pyramids = false;
            MoveLaunch.instance.ResetLaunchCoords();
        }

        public override void OnFixedUpdate()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                if (launchSiteChanged && !this.vessel.HoldPhysics)
                {
                    launchSiteChanged = false;

                    if (beach)
                    {
                        MoveLaunch.instance.LaunchToBeach();
                    }

                    if (kscIslandBeach)
                    {
                        MoveLaunch.instance.LaunchToIslandBeach();
                    }

                    if (baikerbanur)
                    {
                        MoveLaunch.instance.LaunchToBaikerbanur();
                    }

                    if (pyramids)
                    {
                        MoveLaunch.instance.LaunchToPyramids();
                    }

                    if (kscHarborEast)
                    {
                        MoveLaunch.instance.LaunchTokscHarborEast();
                    }

                    if (kscIsandChannel)
                    {
                        MoveLaunch.instance.LaunchTokscIsandChannel();
                    }

                    if (kscIslandNewHarbor)
                    {
                        MoveLaunch.instance.LaunchTokscIslandNewHarbor();
                    }

                    if (MissileRange200Island)
                    {
                        MoveLaunch.instance.LaunchToMissileRange200Island();
                    }

                    if (TirpitzBay)
                    {
                        MoveLaunch.instance.LaunchToTirpitzBay();
                    }

                    if (KerbiniAtol)
                    {
                        MoveLaunch.instance.LaunchToKerbiniAtol();
                    }

                    if (islandRunway)
                    {
                        MoveLaunch.instance.LaunchToIslandRunway();
                    }

                    if (MidwayIsland)
                    {
                        MoveLaunch.instance.LaunchToMidwayIsland();
                    }

                    if (TrunkPeninsula)
                    {
                        MoveLaunch.instance.LaunchToTrunkPeninsula();
                    }

                    if (NorthPole)
                    {
                        MoveLaunch.instance.LaunchToNorthPole();
                    }

                    if (SouthPole)
                    {
                        MoveLaunch.instance.LaunchToSouthPole();
                    }

                    if (KerbiniIsland)
                    {
                        MoveLaunch.instance.LaunchToKerbiniIsland();
                    }
                    StartCoroutine(KillPart());
                }
            }
            base.OnFixedUpdate();
        }

        IEnumerator KillPart()
        {
            yield return new WaitForSeconds(0.5f);
            this.part.Die();
        }

        //////////////////////////////////////////////////////////////////////////////////////////

        // Runway: lat -0.0485890418349364, long 285.276094692895, alt 71.9665353324963
        // Beach: lat -0.039751185006272, long 285.639486693549, alt 1.68487426708452
        // Beach by Island: lat -1.53556797173857, long 287.956960620886, alt 1.56112247915007

        //  public static bool launchSiteChanged;

        /// <summary>
        /// //////////////////////////////////////////////////////////////
        /// </summary>

        private void Dummy()
        {
        }



    }
}
