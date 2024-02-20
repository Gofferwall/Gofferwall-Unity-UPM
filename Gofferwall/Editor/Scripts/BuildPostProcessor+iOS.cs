#if UNITY_IOS

using System;
using System.IO;
using System.Net;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

using Gofferwall.Extension;

namespace Gofferwall.PostProcessor
{
    class BuildPostProcessorForIos {
        private static string prefixURI = "https://github.com/Gofferwall/Gofferwall-iOS-Sample/releases/download/1.0.0/";
        private static string gofferwallFrameworkPath = "../Packages/com.tnk.gofferwall/Plugins/iOS";
        private static string gofferwallUnityPath = "com.tnk.gofferwall/Plugins/iOS";

        public static void OnPostProcessBuild(string path) {
            CopyGofferwallFrameworks(path, new List<GofferwallFrameworkType>() {
                GofferwallFrameworkType.Core
            });
            UpdateBuildSetting(path);
            UpdateInfoPlist(path);
        }

        private static string GetDefaultTarget(PBXProject project) {
            return project.GetUnityMainTargetGuid();
        }

        private static string GetFrameworkTarget(PBXProject project) {
            return project.GetUnityFrameworkTargetGuid();
        }

        private static void CopyGofferwallFrameworks(string path, List<GofferwallFrameworkType> usingFrameworks) {
            PBXProject project = new PBXProject();
            string projectPath = PBXProject.GetPBXProjectPath(path);     
            string contents = File.ReadAllText(projectPath);
            project.ReadFromString(contents); 

            string buildTargetGUID = GetDefaultTarget(project);
            string embedSectionID = project.AddCopyFilesBuildPhase(buildTargetGUID, "Embed Frameworks", "", "10");

            project.AddBuildProperty(buildTargetGUID, "VALIDATE_WORKSPACE", "YES");
            project.AddBuildProperty(buildTargetGUID, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");
            project.AddBuildProperty(buildTargetGUID, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/Frameworks/" + gofferwallUnityPath);
            
            foreach (GofferwallFrameworkType type in usingFrameworks) {
                if (!type.GetAdapterEnable()) {
                    continue;
                }
                string fileID = project.AddFile(
                    "Frameworks/" + gofferwallUnityPath + "/" + type.GetFileName(),
                    "Frameworks/" + gofferwallUnityPath + "/" + type.GetFileName()
                );
                
                List<string> childFiles = type.GetChildFrameworkName();
                foreach (string childFileName in childFiles) {
                    string childFileID = project.AddFile(
                        "Frameworks/" + gofferwallUnityPath + "/" + childFileName,
                        "Frameworks/" + gofferwallUnityPath + "/" + childFileName
                    );
                    project.AddFileToBuildSection(buildTargetGUID, embedSectionID, childFileID);
                    PBXProjectExtensions.AddFileToEmbedFrameworks(project, buildTargetGUID, childFileID);
                }

                project.AddFileToBuildSection(buildTargetGUID, embedSectionID, fileID);
                PBXProjectExtensions.AddFileToEmbedFrameworks(project, buildTargetGUID, fileID);
            }

            File.WriteAllText(projectPath, project.WriteToString());
            UpdateDuplicateFrameworkSourceReference(projectPath, gofferwallFrameworkPath);
        }

        private static void UpdateDuplicateFrameworkSourceReference(string projectPath, string gofferwallUnityPath) {
            string contents = File.ReadAllText(projectPath);
            string[] contentList = contents.Split(new char[] {'\n'});
            List<string> result = new List<string>();

            foreach (string line in contentList) {
                bool isMatch = Regex.IsMatch(line, ".*Frameworks/" + gofferwallUnityPath + "/; sourceTree = SOURCE_ROOT;");
                if (!isMatch) { result.Add(line); }
            }
            contents = String.Join("\n", result);

            PBXProject project = new PBXProject();
            project.ReadFromString(contents);
            File.WriteAllText(projectPath, project.WriteToString());
        }

        private static void UpdateBuildSetting(string path) {
            PBXProject project = new PBXProject();
            string projectPath = PBXProject.GetPBXProjectPath(path);     
            string contents = File.ReadAllText(projectPath);
            project.ReadFromString(contents); 

            string defaultTarget = GetDefaultTarget(project);
            string buildFrameworkTarget = GetFrameworkTarget(project);

            // Add `-ObjC` to "Other Linker Flags".
            project.AddBuildProperty(defaultTarget, "OTHER_LDFLAGS", "-ObjC");
            // project.AddBuildProperty(defaultTarget, "LD_RUNPATH_SEARCH_PATHS", "/usr/lib/swift");
            // Update 'NO' to "ENABLE_BITCODE"
            project.UpdateBuildProperty(defaultTarget, "ENABLE_BITCODE", new  string [] { "NO" }, new  string [] { "YES" });
            project.UpdateBuildProperty(buildFrameworkTarget, "ENABLE_BITCODE", new  string [] { "NO" }, new  string [] { "YES" });
            File.WriteAllText(projectPath, project.WriteToString());
        }
        
        private static void UpdateInfoPlist(string path) {
            string plistPath = Path.Combine (path, "Info.plist" );
            PlistDocument root = new PlistDocument();
            root.ReadFromFile(plistPath);
            
            Dictionary<string, object> injectPlistInfo = new Dictionary<string, object>{
                // Permissions
                { "NSUserTrackingUsageDescription", "Some ad content may require access to the user tracking." },
            };
            foreach(KeyValuePair<string, object> item in injectPlistInfo) {
                InsertInfoPlist(root.root, item.Key, item.Value);
            }

            // SKAdNetwork
            PlistElementArray targetArray = root.root.CreateArray("SKAdNetworkItems");
            DownloadSkAdNetworkPlistFile(path);
            
            string downloadedPath = Path.Combine(path, "GofferwallSkAdNetworks.plist");
            PlistDocument skAdNetwork = new PlistDocument();
            skAdNetwork.ReadFromFile(downloadedPath);

            PlistElementArray array = (PlistElementArray)skAdNetwork.root["SKAdNetworkItems"];
            foreach(PlistElementDict item in array.values) {
                PlistElementDict dict = targetArray.AddDict();
                dict.SetString("SKAdNetworkIdentifier", item["SKAdNetworkIdentifier"].AsString());
            }

            File.Delete(downloadedPath);

            var settings = FrameworkSettingsRegister.Load();
            var serialized = new SerializedObject(settings);
            string tapjoyAppKey = serialized.FindProperty("_tapjoyAppKey_ios").stringValue;
            string tnkAppKey = serialized.FindProperty("_tnkAppKey_ios").stringValue;
            string mediaID_ios = serialized.FindProperty("_mediaID_ios").stringValue;
            string mediaSecret_ios = serialized.FindProperty("_mediaSecret_ios").stringValue;
            if (tapjoyAppKey != null && tapjoyAppKey.Length > 0) {
                InsertInfoPlist(root.root, "tapjoy_app_key", tapjoyAppKey);
            }
            if (tnkAppKey != null && tnkAppKey.Length > 0) {
                InsertInfoPlist(root.root, "tnkad_app_id", tnkAppKey);
            }
            if (mediaID_ios != null && mediaID_ios.Length > 0) {
                InsertInfoPlist(root.root, "GofferwallMediaId", mediaID_ios);
            }
            if (mediaSecret_ios != null && mediaSecret_ios.Length > 0) {
                InsertInfoPlist(root.root, "GofferwallMediaSecret", mediaSecret_ios);
            }

            string filePath = "Packages/com.tnk.gofferwall/package.json";
            string json = File.ReadAllText(filePath);
            ParsingPackageJson.PackageJson pj = JsonUtility.FromJson<ParsingPackageJson.PackageJson>(json);
            InsertInfoPlist(root.root, "GofferwallUnitySDKVersion", pj.version);

            root.WriteToFile(plistPath);
        }

        private static void DownloadSkAdNetworkPlistFile(string path) {
            string fileName = "GofferwallSkAdNetworks.plist";
            string uriString = prefixURI;
            uriString += "/" + fileName;
            (new WebClient()).DownloadFile(new Uri(uriString), Path.Combine(path, fileName));
        }

        private static void InsertInfoPlist(object plist, string key, object value) {
            if (value is string) {
                if (plist is PlistElementDict) {
                    PlistElementDict plistElementDict = (PlistElementDict)plist;
                    plistElementDict.SetString(key, (string)value);
                } else if (plist is PlistElementArray) {
                    PlistElementArray plistElementArray = (PlistElementArray)plist;
                    if (plistElementArray.IsExistPlist((string)value)) { return; }
                    plistElementArray.AddString((string)value);
                }
                return;  
            } else if  (value is bool) {
                PlistElementDict plistElementDict = (PlistElementDict)plist;
                plistElementDict.SetBoolean(key, (bool)value);
            } 

            if (value is List<string>) {
                PlistElementDict rootPlistElementDict = (PlistElementDict)plist;
                PlistElementArray targetPlistElementArray = (PlistElementArray)rootPlistElementDict[key];
                if (targetPlistElementArray == null) { targetPlistElementArray = rootPlistElementDict.CreateArray(key); }

                foreach (string item in (List<string>)value) {
                    InsertInfoPlist(targetPlistElementArray, null, item);
                }
            } else if (value is Dictionary<string, object>) {
                PlistElementDict rootPlistElementDict = (PlistElementDict)plist;
                PlistElementDict targetPlistElementDict = (PlistElementDict)rootPlistElementDict[key];
                if (targetPlistElementDict == null) { rootPlistElementDict.CreateDict(key); }

                foreach (KeyValuePair<string, object> valueItem in (Dictionary<string, object>)value) {
                    InsertInfoPlist(targetPlistElementDict, valueItem.Key, valueItem.Value);
                }
            }
        }
    }

    public enum GofferwallFrameworkType {
        Core
    }

    static class GofferwallFrameworkTypeExtension {

        public static bool IsEmbedFramework(this GofferwallFrameworkType type) {
            return type == GofferwallFrameworkType.Core;
        }

        public static string GetFileName(this GofferwallFrameworkType type) {
            switch (type) {
                case GofferwallFrameworkType.Core:      return "Gofferwall.framework";
                default:                                return null;
            }
        }

        public static bool GetAdapterEnable(this GofferwallFrameworkType type) {
            switch (type) {
                case GofferwallFrameworkType.Core:      return true;
                default:                                return false;
            }
        }

        public static List<string> GetChildFrameworkName(this GofferwallFrameworkType type) {
            switch (type) {
                case GofferwallFrameworkType.Core:
                    return new List<string>() {
                        "Tapjoy.framework",
                        "TnkRwdSdk2.framework"
                    };
                default:                                return null;
            }
        }
    }
}

#endif
