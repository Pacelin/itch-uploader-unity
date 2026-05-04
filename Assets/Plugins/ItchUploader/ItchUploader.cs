using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Pacelin.ItchUploader.Editor
{
    public static class ItchUploader
    {
        private static string ButlerPath => Application.dataPath + "/Plugins/ItchUploader/itch-butler/butler.exe";
        
        private const string UPGRADE_ARGS = "upgrade --assume-yes";
        private const string LOGIN_ARGS = "login";
        private const string PUSH_ARGS = "push \"{0}\" {1}/{2}:{3} --userversion {4}";

        private const string ITCH_UPLOADER = "Itch Uploader";

        public static void RunUpload(string buildPath)
        {
            try
            {
                if (!Directory.Exists(buildPath))
                {
                    Debug.LogError("Build directory not found.");
                    return;
                }

                var directory = buildPath;
                var user = ItchUploaderSettings.UserName.ToLower();
                var game = ItchUploaderSettings.GameName.ToLower();
                var version = ItchUploaderSettings.Version;
                var channel = ItchUploaderSettings.Platform;
                
                EditorUtility.DisplayProgressBar(ITCH_UPLOADER, "Check for updates", 0.1f);
                Thread.Sleep(500);
                CheckForUpdates();
                
                EditorUtility.DisplayProgressBar(ITCH_UPLOADER, "Login", 0.2f);
                Thread.Sleep(500);
                Login();
                
                EditorUtility.DisplayProgressBar(ITCH_UPLOADER, "Upload", 0.5f);
                Thread.Sleep(500);
                Upload(directory, user, game, channel, version);

                Debug.Log("New Build Uploaded on Itch Successful");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        private static void CheckForUpdates()
        {
            var startInfo = new ProcessStartInfo(ButlerPath, UPGRADE_ARGS);
            startInfo.UseShellExecute = false;
            var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        private static void Login()
        {
            var startInfo = new ProcessStartInfo(ButlerPath, LOGIN_ARGS);
            startInfo.UseShellExecute = false;
            var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        private static void Upload(string directory, string user, string game, string channel, string version)
        {
            var arguments = string.Format(PUSH_ARGS, directory, user, game, channel, version);
            var startInfo = new ProcessStartInfo(ButlerPath, arguments);
            
            startInfo.UseShellExecute = false;
            var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }
    }
}