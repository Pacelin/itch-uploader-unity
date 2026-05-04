# Unity Itch.io Uploader

Upload your Unity game builds to [Itch.io](https://itch.io) right from the Unity Editor.
The package automates calling butler and lets you configure upload parameters without leaving Unity.

## 🚀 Features

- One‑click upload of a build to Itch.io.
- Configurable parameters: game name, build path, version, and platform.
- Editor integration – a dedicated item in the `Tools` menu. In Unity 6000.3, a MainToolbarElement is also available.

## 📋 Requirements

- **Operating system:** Windows
- **Minimum Unity version:** 2020.3 LTS  
- **Tested on:** Unity 6000.3 LTS  
- **No external dependencies** – `butler` is bundled with the package.

## 📦 Installation

1. Go to the [Releases](https://github.com/Pacelin/itch-uploader-unity/releases) page of this repository.
2. Download the latest `.unitypackage` file.
3. In Unity, open `Assets > Import Package > Custom Package...` and select the downloaded file.
4. Make sure all files are selected and click `Import`.

That’s it! The package is now installed and ready to use.

## 🚀 First upload – automatic authentication

No manual setup is required. The first time you try to upload a build, the package will:

- Detect that `butler` (the Itch.io command-line tool) is bundled inside the package.
- Open a login window or prompt you to authenticate with Itch.io (depending on the integration).
- Store your credentials locally so subsequent uploads just work.

> **Note:** You only need to log in once. The package handles everything else.

## 🎮 Usage in Unity

1. Open the package window: `Tools > Itch Uploader`.
2. Fill in the required fields:
   - **User codename** - your user codename on Itch.io.
   - **Game codename** – your game’s codename on Itch.io (e.g., `my-game` from the URL).
   - **Platform codename** – target platform channel (e.g., `windows`, `macos`, `linux`, `html5`).
   - **Version** – version that will show on Itch.io (e.g., `1.0.0`).
3. Optionally click **Save** for save current settings.
4. Click **Upload Build**.

The upload progress will be shown in the editor console. The first time you click upload, the authentication flow will start automatically.
