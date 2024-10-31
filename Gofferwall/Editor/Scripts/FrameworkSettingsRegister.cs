using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using Gofferwall.Editor;

namespace Gofferwall
{
    public static class FrameworkSettingsRegister
    {
        public const string SERVICE_JSON_KEY_INIT_TAPJOY    = "tapjoy_sdk_key";
        public const string SERVICE_JSON_KEY_INIT_TNK       = "tnkad_app_id";

        private const string SERVICE_JSON_KEY_GOFFERWALL    = "gofferwall";
        private const string SERVICE_JSON_KEY_NETWORK       = "network";
        private const string SERVICE_JSON_KEY_ADS           = "ads";
        private const string SERVICE_JSON_KEY_SETTINGS      = "settings";
        private const string SERVICE_JSON_KEY_OFFERWALL     = "gofferwallAd";

        private const string PATH_GOFFERWALL_EDITOR           = "/Gofferwall/Editor";
        private static string SettingsPath                  = "Assets/Gofferwall/Editor/Gofferwall.asset";

        private static string[] OS_Type     = { "None", "AOS & iOS", "AOS", "iOS" };
        private static string[] AOS_Type    = { "None", "AOS(iOS 기능 추가 시 자동 추가)", "AOS", "None(iOS 기능 추가 시 자동 추가)" };
        private static string[] iOS_Type    = { "None", "iOS(AOS 기능 추가 시 자동 추가)", "None(AOS 기능 추가 시 자동 추가)", "iOS" };



        /// <summary>
        /// install plugins without UI prompt
        /// </summary>
        /// <param name="androidJsonFilePath">path of (mediaId)_AndroidGofferwall.json</param>
        /// <param name="iOSJsonFilePath">path of (mediaId)_iOSGofferwall.json</param>
        public static bool GofferwallImportJson(string androidJsonFilePath, string iOSJsonFilePath) {
            bool isAndroidPath  = (androidJsonFilePath != string.Empty && androidJsonFilePath.Trim().Length > 0);
            bool isiOSPath      = (iOSJsonFilePath != string.Empty && iOSJsonFilePath.Trim().Length > 0);

            if (isAndroidPath) {
                SettingsJson(androidJsonFilePath, true);
            }

            if (isiOSPath) {
                SettingsJson(iOSJsonFilePath, false);
            }

            if (isAndroidPath) {
                return BuildPostProcessorForAndroid.CreateGofferwallAndroidFiles(false);
            } else if (isiOSPath) {
                return true;
            } else {
                return false;
            }
        }



        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new SettingsProvider("Project/GofferwallSDK", SettingsScope.Project)
            {
                guiHandler = (searchContext) =>
                {
                    string filePath = "Packages/com.tnk.gofferwall/package.json";
                    string json = File.ReadAllText(filePath);
                    ParsingPackageJson.PackageJson pj = JsonUtility.FromJson<ParsingPackageJson.PackageJson>(json);
                    
                    var serialized = new SerializedObject(Load());
                    EditorGUILayout.HelpBox(string.Format("Version {0}", pj.version), MessageType.None);
                    EditorGUILayout.Space();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Settings Android from json file", GUILayout.Height(30))) {
                        SettingsJson(EditorUtility.OpenFilePanel("Settings Android from json file", null, "json"), true);
                    }
                    if (GUILayout.Button("Settings iOS from json file", GUILayout.Height(30))) {
                        SettingsJson(EditorUtility.OpenFilePanel("Settings iOS from json file", null, "json"), false);
                    }
                    GUILayout.EndHorizontal();
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(serialized.FindProperty("_mediaID_aos"), new GUIContent("Media ID(AOS)"));
                    EditorGUILayout.PropertyField(serialized.FindProperty("_mediaSecret_aos"), new GUIContent("Media Secret(AOS)"));
                    EditorGUILayout.PropertyField(serialized.FindProperty("_mediaID_ios"), new GUIContent("Media ID(iOS)"));
                    EditorGUILayout.PropertyField(serialized.FindProperty("_mediaSecret_ios"), new GUIContent("Media Secret(iOS)"));
                    EditorGUILayout.Space();

                    GUILayout.BeginHorizontal();
                    int tapjoyAdapter = serialized.FindProperty("_tapjoyAdapter").intValue;
                    tapjoyAdapter = EditorGUILayout.Popup("Tapjoy Adapter", tapjoyAdapter, OS_Type);
                    serialized.FindProperty("_tapjoyAdapter").intValue = tapjoyAdapter;
                    GUILayout.EndHorizontal();
                    EditorGUI.BeginDisabledGroup(tapjoyAdapter == 0 || tapjoyAdapter == 3);
                    EditorGUILayout.PropertyField(serialized.FindProperty("_tapjoyAppKey_aos"), new GUIContent("Tapjoy App Key(AOS)"));
                    EditorGUI.EndDisabledGroup();
                    EditorGUI.BeginDisabledGroup((tapjoyAdapter == 0 || tapjoyAdapter == 2));
                    EditorGUILayout.PropertyField(serialized.FindProperty("_tapjoyAppKey_ios"), new GUIContent("Tapjoy App Key(iOS)"));
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.Space();

                    GUILayout.BeginHorizontal();
                    int tnkAdapter = serialized.FindProperty("_tnkAdapter").intValue;
                    tnkAdapter = EditorGUILayout.Popup("TNK Adapter", tnkAdapter, OS_Type);
                    serialized.FindProperty("_tnkAdapter").intValue = tnkAdapter;
                    GUILayout.EndHorizontal();
                    EditorGUI.BeginDisabledGroup(tnkAdapter == 0 || tnkAdapter == 3);
                    EditorGUILayout.PropertyField(serialized.FindProperty("_tnkAppKey_aos"), new GUIContent("TNK App Key(AOS)"));
                    EditorGUI.EndDisabledGroup();
                    EditorGUI.BeginDisabledGroup((tnkAdapter == 0 || tnkAdapter == 2));
                    EditorGUILayout.PropertyField(serialized.FindProperty("_tnkAppKey_ios"), new GUIContent("TNK App Key(iOS)"));
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.Space();

                    EditorGUILayout.EndFoldoutHeaderGroup();
                    if (EditorGUI.EndChangeCheck())
                    {
                        serialized.ApplyModifiedProperties();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    if (GUILayout.Button("Create Gofferwall Android & iOS Files", GUILayout.Height(30)))
                    {
                        AssetDatabase.SaveAssets();
                        var aos_id = serialized.FindProperty("_mediaID_aos").stringValue;
                        var ios_id = serialized.FindProperty("_mediaID_ios").stringValue;
                        if ((aos_id != null && aos_id.Trim().Length > 0 && BuildPostProcessorForAndroid.CreateGofferwallAndroidFiles(true))
                        || (ios_id != null && ios_id.Trim().Length > 0)) {
                            EditorUtility.ClearProgressBar();
                            EditorUtility.DisplayDialog("Succeed to install", "파일이 정상적으로 생성되었습니다.", "닫기");
                        } else {
                            EditorUtility.ClearProgressBar();
                            EditorUtility.DisplayDialog("Failed to install", "파일 생성 실패", "닫기");
                        }
                    }
                },
                
                keywords = new HashSet<string>(new[] { "Gofferwall", "GofferwallSDK", "MediaId" })
            };
        }

        public static FrameworkSettings Load()
        {
            var settings = AssetDatabase.LoadAssetAtPath<FrameworkSettings>(SettingsPath);
            if (settings == null)
            {
                CreateGofferwallFrameworksDirectory();
                settings = ScriptableObject.CreateInstance<FrameworkSettings>();
                AssetDatabase.CreateAsset(settings, SettingsPath);
                AssetDatabase.SaveAssets();
            }

            return settings;
        }

        // json 파일로 세팅값들 설정
        private static void SettingsJson(string filePath, bool isAndroid) {
            if (filePath == string.Empty || filePath.Trim().Length < 1) {
                return;
            }

            FileManager fm = new FileManager();
            Dictionary<string, object> settings = fm.ReadJsonFile(filePath);
            if (settings == null) {
                Debug.LogError("can't get service setting from: " + filePath);
                return;
            }

            var serialized = new SerializedObject(Load());
            // Gofferwall 값 설정
            if (!settings.ContainsKey(SERVICE_JSON_KEY_GOFFERWALL) || settings[SERVICE_JSON_KEY_GOFFERWALL] == null) {
                Debug.LogError("missing json key [" + SERVICE_JSON_KEY_GOFFERWALL + "] from service setting");
            } else {
                Dictionary<string, object> gofferwallInfo = settings[SERVICE_JSON_KEY_GOFFERWALL] as Dictionary<string, object>;
                if (!gofferwallInfo.ContainsKey(SERVICE_JSON_KEY_SETTINGS) || gofferwallInfo[SERVICE_JSON_KEY_SETTINGS] == null) {
                    Debug.LogError("missing json key [" + SERVICE_JSON_KEY_SETTINGS + "] from service setting");
                } else {
                    Dictionary<string, object> gofferwallInfoSettings = gofferwallInfo[SERVICE_JSON_KEY_SETTINGS] as Dictionary<string, object>;
                    if (isAndroid) {
                        serialized.FindProperty("_mediaID_aos").stringValue = gofferwallInfoSettings["gofferwall_media_id"].ToString();
                        serialized.FindProperty("_mediaSecret_aos").stringValue = gofferwallInfoSettings["gofferwall_media_secret"].ToString();
                    } else {
                        serialized.FindProperty("_mediaID_ios").stringValue = gofferwallInfoSettings["gofferwall_media_id"].ToString();
                        serialized.FindProperty("_mediaSecret_ios").stringValue = gofferwallInfoSettings["gofferwall_media_secret"].ToString();
                    }
                    serialized.ApplyModifiedProperties();
                }
            }

            // adapter 사용 유무 및 Key 설정
            if (!settings.ContainsKey(SERVICE_JSON_KEY_NETWORK) || settings[SERVICE_JSON_KEY_NETWORK] == null) {
                Debug.LogError("missing json key [" + SERVICE_JSON_KEY_NETWORK + "] from service setting");
            } else {
                Dictionary<string, object> gofferwallNetworks = settings[SERVICE_JSON_KEY_NETWORK] as Dictionary<string, object>;
                foreach (string adNetworkName in gofferwallNetworks.Keys) {
                    if (GofferwallAdapterSettings.GetIsSetting(adNetworkName, isAndroid)) {
                        Dictionary<string, object> networkInfo = gofferwallNetworks[adNetworkName] as Dictionary<string, object>;
                        Dictionary<string, object> networkInfoAds = networkInfo[SERVICE_JSON_KEY_ADS] as Dictionary<string, object>;
                        bool offerwallAdEnabled = Boolean.Parse(networkInfoAds[SERVICE_JSON_KEY_OFFERWALL].ToString());
                        int adapter = serialized.FindProperty("_" + adNetworkName + "Adapter").intValue;
                        if (offerwallAdEnabled) {
                            if (isAndroid) {
                                if (adapter < 1) {
                                    serialized.FindProperty("_" + adNetworkName + "Adapter").intValue = 2;
                                } else if (adapter == 1 || adapter == 3) {
                                    serialized.FindProperty("_" + adNetworkName + "Adapter").intValue = 1;
                                }
                            } else {
                                if (adapter < 1) {
                                    serialized.FindProperty("_" + adNetworkName + "Adapter").intValue = 3;
                                } else if (adapter == 1 || adapter == 2) {
                                    serialized.FindProperty("_" + adNetworkName + "Adapter").intValue = 1;
                                }
                            }
                        } else {
                            if (isAndroid) {
                                if (adapter == 1) {
                                    serialized.FindProperty("_" + adNetworkName + "Adapter").intValue = 3;
                                } else if (adapter == 2) {
                                    serialized.FindProperty("_" + adNetworkName + "Adapter").intValue = 0;
                                }
                            } else {
                                if (adapter == 1) {
                                    serialized.FindProperty("_" + adNetworkName + "Adapter").intValue = 2;
                                } else if (adapter == 3) {
                                    serialized.FindProperty("_" + adNetworkName + "Adapter").intValue = 0;
                                }
                            }
                        }

                        if (GofferwallAdapterSettings.TAPJOY == adNetworkName && networkInfo.ContainsKey(SERVICE_JSON_KEY_SETTINGS) && networkInfo[SERVICE_JSON_KEY_SETTINGS] != null) {
                            Dictionary<string, object> networkInfoSettings = networkInfo[SERVICE_JSON_KEY_SETTINGS] as Dictionary<string, object>;
                            if (networkInfoSettings.ContainsKey(SERVICE_JSON_KEY_INIT_TAPJOY) && networkInfoSettings[SERVICE_JSON_KEY_INIT_TAPJOY] != null) {
                                string admobKey = networkInfoSettings[SERVICE_JSON_KEY_INIT_TAPJOY].ToString();
                                if (admobKey != null && admobKey.Length > 0) {
                                    if (isAndroid) {
                                        serialized.FindProperty("_tapjoyAppKey_aos").stringValue = admobKey;
                                    } else {
                                        serialized.FindProperty("_tapjoyAppKey_ios").stringValue = admobKey;
                                    }
                                }
                            }
                        }

                        if (GofferwallAdapterSettings.TNK == adNetworkName && networkInfo.ContainsKey(SERVICE_JSON_KEY_SETTINGS) && networkInfo[SERVICE_JSON_KEY_SETTINGS] != null) {
                            Dictionary<string, object> networkInfoSettings = networkInfo[SERVICE_JSON_KEY_SETTINGS] as Dictionary<string, object>;
                            if (networkInfoSettings.ContainsKey(SERVICE_JSON_KEY_INIT_TNK) && networkInfoSettings[SERVICE_JSON_KEY_INIT_TNK] != null) {
                                string admobKey = networkInfoSettings[SERVICE_JSON_KEY_INIT_TNK].ToString();
                                if (admobKey != null && admobKey.Length > 0) {
                                    if (isAndroid) {
                                        serialized.FindProperty("_tnkAppKey_aos").stringValue = admobKey;
                                    } else {
                                        serialized.FindProperty("_tnkAppKey_ios").stringValue = admobKey;
                                    }
                                }
                            }
                        }
                    }
                }
                serialized.ApplyModifiedProperties();
            }
        }

        private static void CreateGofferwallFrameworksDirectory()
        {
            if (!Directory.Exists(Application.dataPath + PATH_GOFFERWALL_EDITOR))
            {
                Directory.CreateDirectory(Application.dataPath + PATH_GOFFERWALL_EDITOR);
            }
        }
    }

    static class GofferwallAdapterSettings {
        public const string TAPJOY      = "tapjoy";
        public const string TNK         = "tnk";

        public static bool GetIsSetting(string network, bool isAndroid) {
            if (isAndroid) {
                switch (network) {
                    case TAPJOY:
                    case TNK:
                    return true;
                    default: return false;
                }
            } else {
                switch (network) {
                    case TAPJOY:
                    case TNK:
                    return true;
                    default: return false;
                }
            }
        }
    }
}