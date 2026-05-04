using System.IO;
using UnityEditor;
using UnityEngine;
#if UNITY_6000_3_OR_NEWER
using UnityEditor.Toolbars;
#endif

namespace Pacelin.ItchUploader.Editor
{
    [EditorWindowTitle(title = "Itch Uploader")]
    public class ItchUploaderWindow : EditorWindow
    {
        private string _user;
        private string _game;
        private string _version;
        private string _platform;

        public void Initialize()
        {
            _user = ItchUploaderSettings.UserName;
            _game = ItchUploaderSettings.GameName;
            _version = ItchUploaderSettings.Version;
            _platform = ItchUploaderSettings.Platform;
            position = new(Event.current.mousePosition + new Vector2(-150, 100), 
                new Vector2(300, 105));
        }
       
#if UNITY_6000_3_OR_NEWER
        [MainToolbarElement("Tools/Itch Uploader", defaultDockPosition = MainToolbarDockPosition.Left)]
        public static MainToolbarElement OpenItchUploader()
        {
            var content = new MainToolbarContent("Itch Uploader");
            return new MainToolbarButton(content, () =>
            {
                var window = GetWindow<ItchUploaderWindow>();
                window.Initialize();
            });
        }
#endif
        
        [MenuItem("Tools/Itch Uploader")]
        public static void OpenItchUploaderByTools()
        {
            var window = GetWindow<ItchUploaderWindow>();
            window.Initialize();
        }

        private void OnGUI()
        {
            _user = EditorGUILayout.TextField("User codename: ", _user);
            _game = EditorGUILayout.TextField("Game codename: ", _game);
            _platform = EditorGUILayout.TextField("Platform codename: ", _platform);
            _version = EditorGUILayout.TextField("Version: ", _version);

            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button(new GUIContent("Save")))
            {
                ItchUploaderSettings.UserName = _user;
                ItchUploaderSettings.GameName = _game;
                ItchUploaderSettings.Version = _version;
            }
            
            if (GUILayout.Button(new GUIContent("Open Page")))
                Application.OpenURL($"https://{_user.ToLower()}.itch.io/{_game.ToLower()}");

            if (!string.IsNullOrEmpty(ItchUploaderSettings.LastBuildPath) &&
                Directory.Exists(ItchUploaderSettings.LastBuildPath))
            {
                if (GUILayout.Button(new GUIContent("Upload Last Build")))
                {
                    Close();
                    ItchUploader.RunUpload(ItchUploaderSettings.LastBuildPath);
                }
            }

            if (GUILayout.Button(new GUIContent("Upload Build")))
            {
                var path = EditorUtility.OpenFolderPanel("Select Build Folder", ItchUploaderSettings.SelectedBuildPath, "");
                if (string.IsNullOrEmpty(path))
                    return;
                if (!Directory.Exists(path))
                {
                    Debug.LogError("Build folder not found");
                    return;
                }
                
                Close();
                ItchUploaderSettings.SelectedBuildPath = path;
                ItchUploader.RunUpload(path);
            }
            
            EditorGUILayout.EndHorizontal();
        }
    }
}