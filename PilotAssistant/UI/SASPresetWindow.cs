﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace PilotAssistant.UI
{
    using Presets;

    internal static class SASPresetWindow
    {
        internal static string newPresetName = "";
        internal static Rect SASPresetwindow = new Rect(550, 50, 50, 50);
        internal static bool bShowPresets = false;

        internal static void Draw()
        {
            if (bShowPresets)
            {
                SASPresetwindow = GUILayout.Window(78934857, SASPresetwindow, drawPresetWindow, "SAS Presets", GUILayout.Height(0));
                SASPresetwindow.x = SASMainWindow.SASwindow.x + SASMainWindow.SASwindow.width;
                SASPresetwindow.y = SASMainWindow.SASwindow.y;
            }
        }

        private static void drawPresetWindow(int id)
        {
            if (GUI.Button(new Rect(SASPresetwindow.width - 16, 2, 14, 14), ""))
            {
                bShowPresets = false;
            }

            if (SurfSAS.bStockSAS)
                drawStockPreset();
            else
                drawSurfPreset();
        }

        private static void drawSurfPreset()
        {
            if (PresetManager.Instance.activeSASPreset != null)
            {
                GUILayout.Label(string.Format("Active Preset: {0}", PresetManager.Instance.activeSASPreset.name));
                if (PresetManager.Instance.activeSASPreset.name != "Default")
                {
                    if (GUILayout.Button("Update Preset"))
                    {
                        PresetManager.Instance.activeSASPreset.Update(SurfSAS.SASControllers);
                        PresetManager.saveToFile();
                    }
                }
                GUILayout.Box("", GUILayout.Height(10), GUILayout.Width(180));
            }

            GUILayout.BeginHorizontal();
            newPresetName = GUILayout.TextField(newPresetName);
            if (GUILayout.Button("+", GUILayout.Width(25)))
            {
                if (newPresetName != "")
                {
                    foreach (PresetSAS p in PresetManager.Instance.SASPresetList)
                    {
                        if (newPresetName == p.name)
                            return;
                    }

                    PresetManager.Instance.SASPresetList.Add(new PresetSAS(SurfSAS.SASControllers, newPresetName));
                    newPresetName = "";
                    PresetManager.Instance.activeSASPreset = PresetManager.Instance.SASPresetList[PresetManager.Instance.SASPresetList.Count - 1];
                    PresetManager.saveToFile();
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Box("", GUILayout.Height(10), GUILayout.Width(180));

            if (GUILayout.Button("Reset to Defaults"))
            {
                PresetManager.loadSASPreset(PresetManager.Instance.defaultSASTuning);
                PresetManager.Instance.activeSASPreset = PresetManager.Instance.defaultSASTuning;
            }

            GUILayout.Box("", GUILayout.Height(10), GUILayout.Width(180));

            foreach (PresetSAS p in PresetManager.Instance.SASPresetList)
            {
                if (p.bStockSAS)
                    continue;

                GUILayout.BeginHorizontal();
                if (GUILayout.Button(p.name))
                {
                    PresetManager.loadSASPreset(p);
                    PresetManager.Instance.activeSASPreset = p;
                }
                if (GUILayout.Button("x", GUILayout.Width(25)))
                {
                    if (PresetManager.Instance.activeSASPreset == p)
                        PresetManager.Instance.activeSASPreset = null;
                    PresetManager.Instance.SASPresetList.Remove(p);
                    PresetManager.saveToFile();
                }
                GUILayout.EndHorizontal();
            }
        }

        private static void drawStockPreset()
        {
            if (PresetManager.Instance.activeStockSASPreset != null)
            {
                GUILayout.Label(string.Format("Active Preset: {0}", PresetManager.Instance.activeStockSASPreset.name));
                if (PresetManager.Instance.activeStockSASPreset.name != "Stock")
                {
                    if (GUILayout.Button("Update Preset"))
                    {
                        PresetManager.Instance.activeStockSASPreset.Update(Utility.FlightData.thisVessel.Autopilot.SAS);
                        PresetManager.saveToFile();
                    }
                }
                GUILayout.Box("", GUILayout.Height(10), GUILayout.Width(180));
            }

            GUILayout.BeginHorizontal();
            newPresetName = GUILayout.TextField(newPresetName);
            if (GUILayout.Button("+", GUILayout.Width(25)))
            {
                if (newPresetName != "")
                {
                    foreach (PresetSAS p in PresetManager.Instance.SASPresetList)
                    {
                        if (newPresetName == p.name)
                            return;
                    }

                    PresetManager.Instance.SASPresetList.Add(new PresetSAS(Utility.FlightData.thisVessel.Autopilot.SAS, newPresetName));
                    newPresetName = "";
                    PresetManager.Instance.activeStockSASPreset = PresetManager.Instance.SASPresetList[PresetManager.Instance.SASPresetList.Count - 1];
                    PresetManager.saveToFile();
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Box("", GUILayout.Height(10), GUILayout.Width(180));

            if (GUILayout.Button("Reset to Defaults"))
            {
                PresetManager.loadStockSASPreset(PresetManager.Instance.defaultStockSASTuning);
                PresetManager.Instance.activeStockSASPreset = PresetManager.Instance.defaultStockSASTuning;
            }

            GUILayout.Box("", GUILayout.Height(10), GUILayout.Width(180));

            foreach (PresetSAS p in PresetManager.Instance.SASPresetList)
            {
                if (!p.bStockSAS)
                    continue;

                GUILayout.BeginHorizontal();
                if (GUILayout.Button(p.name))
                {
                    PresetManager.loadStockSASPreset(p);
                    PresetManager.Instance.activeStockSASPreset = p;
                }
                if (GUILayout.Button("x", GUILayout.Width(25)))
                {
                    if (PresetManager.Instance.activeStockSASPreset == p)
                        PresetManager.Instance.activeStockSASPreset = null;
                    PresetManager.Instance.SASPresetList.Remove(p);
                    PresetManager.saveToFile();
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}
