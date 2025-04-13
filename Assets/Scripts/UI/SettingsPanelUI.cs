using UnityEngine;
using UnityEngine.UIElements;

public class SettingsPanelUI : MonoBehaviour
{
    private const string MUSIC_KEY = "MusicEnabled";
    private const string SFX_KEY = "SfxEnabled";

    private Toggle toggleMusic;
    private Toggle toggleSfx;

    private void OnEnable()
    {
        var root = GetComponentInChildren<UIDocument>().rootVisualElement;

        toggleMusic = root.Q<Toggle>("toggleMusic");
        toggleSfx = root.Q<Toggle>("toggleSfx");
        var btnClose = root.Q<Button>("btnCloseSettings");

        // Load saved preferences
        toggleMusic.value = PlayerPrefs.GetInt(MUSIC_KEY, 1) == 1;
        toggleSfx.value = PlayerPrefs.GetInt(SFX_KEY, 1) == 1;

        // Register change callbacks
        toggleMusic.RegisterValueChangedCallback(evt =>
        {
            PlayerPrefs.SetInt(MUSIC_KEY, evt.newValue ? 1 : 0);
            PlayerPrefs.Save();
            Debug.Log("Musik: " + evt.newValue);
        });

        toggleSfx.RegisterValueChangedCallback(evt =>
        {
            PlayerPrefs.SetInt(SFX_KEY, evt.newValue ? 1 : 0);
            PlayerPrefs.Save();
            Debug.Log("SFX: " + evt.newValue);
        });

        btnClose.clicked += () =>
        {
            gameObject.SetActive(false);
        };
    }
}
