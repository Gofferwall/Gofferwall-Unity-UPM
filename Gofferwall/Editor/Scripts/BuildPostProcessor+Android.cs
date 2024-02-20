using System;
using System.IO;
using System.Net;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Gofferwall.Editor;

namespace Gofferwall
{
    class BuildPostProcessorForAndroid
    {
        #region CONST VARIABLES
        private const string PATH_GOFFERWALL_FILES        = "/Gofferwall/GofferwallAppSettingsFiles";
        private const string PATH_GOFFERWALL_EDITOR       = "/Gofferwall/GofferwallAppSettingsFiles/Editor";
        private const string PATH_GOFFERWALL_MANIFEST     = "/Gofferwall/GofferwallAppSettingsFiles/Plugins/Gofferwall.androidlib";
        private const string DISPLAY_PROGRESS_DIALOG_TITLE = "Gofferwall Install";
        private const string PROJECT_PROPERTIES_CONTENT = "android.library=true";
        #endregion

        public static bool CreateGofferwallAndroidFiles(bool isProgress)
        {
            bool isFrameworks = CopyGofferwallFrameworks(
                new List<GofferwallFrameworkAndroidType>()
                {
                    GofferwallFrameworkAndroidType.Tapjoy,
                    GofferwallFrameworkAndroidType.TNK
                }
            , isProgress);
            bool isUpdateManifest = UpdateAndroidManifest(isProgress);
            bool isUpdateProperties = UpdateProperties(isProgress);

            return (isFrameworks && isUpdateManifest && isUpdateProperties);
        }

        /*** properties 파일 생성 start ***/
        private static bool UpdateProperties(bool isProgress) {
            if (isProgress) {
                if (EditorUtility.DisplayCancelableProgressBar(
                        DISPLAY_PROGRESS_DIALOG_TITLE,
                        "Update project.properties",
                        0.6f
                    )){
                    EditorUtility.ClearProgressBar();
                    return false;
                }
            }

            string propertiesPath = CreateGofferwallManifestDirectory();      // 폴더 생성
            propertiesPath += "/project.properties";                         // 파일명 지정

            try {
                File.WriteAllText(propertiesPath, PROJECT_PROPERTIES_CONTENT);
            } catch (Exception e) {
                Debug.LogError("failed to write file: " + propertiesPath);
                Debug.LogError("" + e);
                return false;
            }

            return true;
        }
        /*** properties 파일 생성 end ***/

        /*** AndroidManifest 파일 생성 start ***/
        private static bool UpdateAndroidManifest(bool isProgress)
        {
            if (isProgress) {
                if (EditorUtility.DisplayCancelableProgressBar(
                        DISPLAY_PROGRESS_DIALOG_TITLE,
                        "Update AndroidManifest.xml",
                        0.5f
                    )){
                    EditorUtility.ClearProgressBar();
                    return false;
                }
            }

            ManifestHandler manifestHandler = GetManifestHandler();     // Manifest에 필수 항목 추가
            string manifestPath = CreateGofferwallManifestDirectory();    // 폴더 생성
            manifestPath += "/AndroidManifest.xml";                     // 파일명 지정

            return manifestHandler.WriteXmlFile(manifestPath);
        }

        private static ManifestHandler GetManifestHandler()
        {
            ManifestHandler handler = new ManifestHandler("com.nps.Gofferwall");
            handler.AddMetaData(
                "<meta-data android:name=\"adiscope.global.mediaId\" android:value=\"" + GetMediaId_AOS() + "\" />"
            );
            handler.AddMetaData(
                "<meta-data android:name=\"adiscope.global.mediaSecret\" android:value=\"" + GetMediaSecret_AOS() + "\" />"
            );
            handler.AddMetaData(
                "<meta-data android:name=\"adiscope.global.unityVersion\" android:value=\"" + GetUnityVersion() + "\" />"
            );
            handler.AddMetaData(
                "<meta-data android:name=\"tnkad_app_id\" android:value=\"" + GetTNKAppId() + "\" />"
            );

            return handler;
        }

        private static string CreateGofferwallManifestDirectory()
        {
            string manifestPath = Application.dataPath + PATH_GOFFERWALL_MANIFEST;
            if (!Directory.Exists(manifestPath))
            {
                Directory.CreateDirectory(manifestPath);
            }
            return manifestPath;
        }

        private static string GetMediaId_AOS() {
            var serialized = GetSettingsRegisterSerializedObject();
            return serialized.FindProperty("_mediaID_aos").stringValue;
        }

        private static string GetMediaSecret_AOS() {
            var serialized = GetSettingsRegisterSerializedObject();
            return serialized.FindProperty("_mediaSecret_aos").stringValue;
        }

        private static string GetTNKAppId() {
            var serialized = GetSettingsRegisterSerializedObject();
            return serialized.FindProperty("_tnkAppKey_aos").stringValue;
        }

        private static SerializedObject GetSettingsRegisterSerializedObject() {
            var settings = FrameworkSettingsRegister.Load();
            return new SerializedObject(settings);
        }

        private static string GetUnityVersion() {
            string filePath = "Packages/com.tnk.gofferwall/package.json";
            string json = File.ReadAllText(filePath);
            ParsingPackageJson.PackageJson pj = JsonUtility.FromJson<ParsingPackageJson.PackageJson>(json);
            return pj.version;
        }
        /*** AndroidManifest 파일 생성 end ***/

        /*** edm4u를 설정 하기 위해 adapter 파일 생성 start ***/
        private static bool CopyGofferwallFrameworks(List<GofferwallFrameworkAndroidType> usingFrameworks, bool isProgress)
        {
            DeleteGofferwallFrameworks(usingFrameworks); // 사용 안 하는 adapter edm4u 파일 삭제
            CreateGofferwallFrameworksDirectory(); // edm4u 파일을 copy 할 폴더 생성
            return FileDownloadEdm4uAdapter(usingFrameworks, isProgress); // edm4u 파일 다운로드
        }

        private static void DeleteGofferwallFrameworks(List<GofferwallFrameworkAndroidType> usingFrameworks)
        {
            string path = Application.dataPath + PATH_GOFFERWALL_EDITOR;
            if (Directory.Exists(path))
            {
                foreach (string filename in Directory.GetFiles(path, "*.xml"))
                {
                    foreach (GofferwallFrameworkAndroidType type in usingFrameworks) {
                        if (filename.Contains(type.GetFileName())) {
                            try
                            {
                                File.Delete(filename);
                            }
                            catch (IOException)
                            {
                                File.Delete(filename);
                            }
                            catch (UnauthorizedAccessException)
                            {
                                File.Delete(filename);
                            }

                            string metaFilename = filename + ".meta";
                            if (File.Exists(metaFilename)) {
                                try
                                {
                                    File.Delete(metaFilename);
                                }
                                catch (IOException)
                                {
                                    File.Delete(metaFilename);
                                }
                                catch (UnauthorizedAccessException)
                                {
                                    File.Delete(metaFilename);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void CreateGofferwallFrameworksDirectory()
        {
            if (!Directory.Exists(Application.dataPath + PATH_GOFFERWALL_EDITOR))
            {
                Directory.CreateDirectory(Application.dataPath + PATH_GOFFERWALL_EDITOR);
            }
        }

        private static bool FileDownloadEdm4uAdapter(List<GofferwallFrameworkAndroidType> usingFrameworks, bool isProgress)
        {
            float progress = 0.4f / usingFrameworks.Count;
            float totalProgress = 0.1f + progress;
            foreach (GofferwallFrameworkAndroidType type in usingFrameworks)
            {
                Debug.Log("Create : " + type.GetFileName() + " / enable : " + type.GetAdapterEnable());
                if (!type.GetAdapterEnable())
                {
                    continue;
                }

                if (isProgress) {
                    if (EditorUtility.DisplayCancelableProgressBar(
                            DISPLAY_PROGRESS_DIALOG_TITLE,
                            "Download Adapter Files",
                            totalProgress))
                    {
                        EditorUtility.ClearProgressBar();
                        return false;
                    }

                    totalProgress += progress;
                }

                if(!DownloadAdapterFile(type.GetFilePath(), type.GetFileName())){
                    EditorUtility.ClearProgressBar();
                    EditorUtility.DisplayDialog("Failed to install", "파일 생성 실패", "닫기");
                    return false;
                }
            }

            return true;
        }

        private static bool DownloadAdapterFile(string file_path, string file_name)
        {
            string uriString = file_path;
            uriString += file_name;
            try
            {
                Debug.Log("DownloadAdapterFile : " + uriString);
                (new WebClient()).DownloadFile(
                    new Uri(uriString),
                    Path.Combine(Application.dataPath + PATH_GOFFERWALL_EDITOR, file_name)
                );
            }
            catch (Exception exception) { 
                Debug.LogError("failed to download adapter file: " + exception.Message);
                EditorUtility.ClearProgressBar();
                return false;
            }

            return true;
        }
        /*** edm4u를 설정 하기 위해 adapter 파일 생성 end ***/
    }

    // Adapter 제거 시 Dependencies 제를 위해 유지 해야 함
    public enum GofferwallFrameworkAndroidType
    {
        Tapjoy,
        TNK
    }

    static class GofferwallFrameworkAndroidTypeExtension
    {
        private const string TAPJOY_FILE_NAME       = "TapjoyDependencies.xml";
        private const string TNK_FILE_NAME          = "TNKDependencies.xml";


        private const string GOFFERWALL_FILE_PATH   = "https://github.com/Gofferwall/Gofferwall-Android-Sample/releases/download/";
        private const string TAPJOY_FILE_PATH       = GOFFERWALL_FILE_PATH + "1.1.0/";
        private const string TNK_FILE_PATH          = GOFFERWALL_FILE_PATH + "1.1.0/";

        public static string GetFileName(this GofferwallFrameworkAndroidType type)
        {
            switch (type)
            {
                case GofferwallFrameworkAndroidType.Tapjoy:         return TAPJOY_FILE_NAME;
                case GofferwallFrameworkAndroidType.TNK:            return TNK_FILE_NAME;
                default:                                            return null;
            }
        }

        public static string GetFilePath(this GofferwallFrameworkAndroidType type)
        {
            switch (type)
            {
                case GofferwallFrameworkAndroidType.Tapjoy:         return TAPJOY_FILE_PATH;
                case GofferwallFrameworkAndroidType.TNK:            return TNK_FILE_PATH;
                default:                                            return null;
            }
        }

        public static bool GetAdapterEnable(this GofferwallFrameworkAndroidType type)
        {
            var settings = FrameworkSettingsRegister.Load();
            var serialized = new SerializedObject(settings);

            switch (type)
            {
                case GofferwallFrameworkAndroidType.Tapjoy:          return (serialized.FindProperty("_tapjoyAdapter").intValue == 1 || serialized.FindProperty("_tapjoyAdapter").intValue == 2);
                case GofferwallFrameworkAndroidType.TNK:            return (serialized.FindProperty("_tnkAdapter").intValue == 1 || serialized.FindProperty("_tnkAdapter").intValue == 2);
                default:                                            return false;
            }
        }
    }
}
