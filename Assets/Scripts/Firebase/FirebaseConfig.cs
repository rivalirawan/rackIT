using UnityEngine;

[System.Serializable]
public class FirebaseConfigData {
    public string web_api_key;
}

public static class FirebaseConfig {
    public static string WebAPIKey;

    static FirebaseConfig() {
        LoadConfig();
    }

    private static void LoadConfig() {
        TextAsset configText = Resources.Load<TextAsset>("FirebaseConfig");
        if (configText == null) {
            Debug.LogError("FirebaseConfig.json tidak ditemukan di Resources!");
            return;
        }

        FirebaseConfigData config = JsonUtility.FromJson<FirebaseConfigData>(configText.text);
        WebAPIKey = config.web_api_key;
    }
}
