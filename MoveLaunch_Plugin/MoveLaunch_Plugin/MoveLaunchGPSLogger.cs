﻿using KSP.UI.Screens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MoveLaunch
{
    public class MoveLaunchGPSLogger : PartModule
    {
        //[KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "LOG GPS COORDS"),
        // UI_Toggle(controlEnabled = true, scene = UI_Scene.All, disabledText = "", enabledText = "")]
        public bool getGPS = false;

        [KSPAction("GET GPS")]
        public void actionGetGPS(KSPActionParam param)
        {
            GetGPS();
        }

        private void GetGPS()
        {
            double lat = this.vessel.latitude;
            double lon = this.vessel.longitude;
            double alt = this.vessel.altitude;

            Debug.Log("[Move Launch] GPS Location Name: " + GPSname);
            Debug.Log("[Move Launch] GPS Latitude: " + lat);
            Debug.Log("[Move Launch] GPS Longitude: " + lon);
            Debug.Log("[Move Launch] GPS Altitude: " + alt);

            ScreenMsg("GPS Location Name: " + GPSname);
            ScreenMsg("GPS Latitude: " + lat);
            ScreenMsg("GPS Longitude: " + lon);
            ScreenMsg("GPS Altitude: " + alt);

            getGPS = false;
        }
       
        public override void OnStart(StartState state)
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                part.force_activate();
            }
            base.OnStart(state);
        }

        private void ScreenMsg(string msg)
        {
            ScreenMessages.PostScreenMessage(new ScreenMessage(msg, 4, ScreenMessageStyle.UPPER_LEFT));
        }

        public void Update()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                if (getGPS)
                {
                    GetGPS();
                }
            }
        }


        #region GUI
        /// <summary>
        /// GUI
        /// </summary>

        private const float WindowWidth = 220;
        private const float DraggableHeight = 40;
        private const float LeftIndent = 12;
        private const float ContentTop = 20;
        public static bool GuiEnabledMLGPS;
        public static bool HasAddedButton;
        private readonly float _incrButtonWidth = 26;
        private readonly float contentWidth = WindowWidth - 2 * LeftIndent;
        private readonly float entryHeight = 20;
        private float _contentWidth;
        private bool _gameUiToggle;
        private float _windowHeight = 250;
        private Rect _windowRect;
        public bool guiOpen = false;
        private string GPSname = string.Empty;

        private void Start()
        {
            _windowRect = new Rect(Screen.width - 200 - 140, 100, WindowWidth, _windowHeight);
            GameEvents.onHideUI.Add(GameUiDisableMLGPS);
            GameEvents.onShowUI.Add(GameUiEnableMLGPS);
            AddToolbarButton();
            _gameUiToggle = true;
            GPSname = "Enter Name";
        }

        private void OnGUI()
        {
            if (GuiEnabledMLGPS)
            {
                _windowRect = GUI.Window(627252316, _windowRect, GuiWindowMLGPS, "");
            }
        }

        private void GuiWindowMLGPS(int ML)
        {
            GUI.DragWindow(new Rect(0, 0, WindowWidth, DraggableHeight));
            float line = 0;
            _contentWidth = WindowWidth - 2 * LeftIndent;

            DrawTitle(line);
            line++;
            DrawNameEntry(line);
            line++;
            DrawGetGPS(line);

            _windowHeight = ContentTop + line * entryHeight + entryHeight + (entryHeight / 2);
            _windowRect.height = _windowHeight;
        }

        private void AddToolbarButton()
        {
            string textureDir = "MoveLaunch/Plugin/";

            if (!HasAddedButton)
            {
                Texture buttonTexture = GameDatabase.Instance.GetTexture(textureDir + "GPS_icon", false); //texture to use for the button
                ApplicationLauncher.Instance.AddModApplication(EnableGuiMLGPS, DisableGuiMLGPS, Dummy, Dummy, Dummy, Dummy,
                    ApplicationLauncher.AppScenes.FLIGHT, buttonTexture);
                HasAddedButton = true;
            }
        }

        private void Dummy()
        {
        }


        public void EnableGuiMLGPS()
        {
            guiOpen = true;
            GuiEnabledMLGPS = true;
            Debug.Log("[Davinci's Move Launch]: Showing GUI");
        }

        public void DisableGuiMLGPS()
        {
            guiOpen = false;
            GuiEnabledMLGPS = false;
            Debug.Log("[Davinci's Move Launch]: Hiding GUI");
        }

        private void GameUiEnableMLGPS()
        {
            _gameUiToggle = true;
        }

        private void GameUiDisableMLGPS()
        {
            _gameUiToggle = false;
        }

        private void DrawTitle(float line)
        {
            var centerLabel = new GUIStyle
            {
                alignment = TextAnchor.UpperCenter,
                normal = { textColor = Color.white }
            };
            var titleStyle = new GUIStyle(centerLabel)
            {
                fontSize = 14,
                alignment = TextAnchor.MiddleCenter
            };
            GUI.Label(new Rect(0, 0, WindowWidth, 20), "GPS Logger", titleStyle);
        }

        private void DrawNameEntry(float line)
        {
            var leftLabel = new GUIStyle();
            leftLabel.alignment = TextAnchor.UpperLeft;
            leftLabel.normal.textColor = Color.white;

            GUI.Label(new Rect(LeftIndent, ContentTop + line * entryHeight, 60, entryHeight), "Name",
                leftLabel);
            float textFieldWidth = 140;
            var fwdFieldRect = new Rect(LeftIndent + contentWidth - textFieldWidth,
                ContentTop + line * entryHeight, textFieldWidth, entryHeight);
            GPSname = GUI.TextField(fwdFieldRect, GPSname);
        }


        private void DrawGetGPS(float line)
        {
            var saveRect = new Rect(LeftIndent * 1.5f, ContentTop + line * entryHeight, contentWidth * 0.9f, entryHeight);
            if (GUI.Button(saveRect, "LOG GPS COORDS"))
            {
                GetGPS();
            }
        }

        #endregion


    }
}
