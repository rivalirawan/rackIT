using UnityEngine;
using UnityEngine.UIElements;

public class SettingsPanelUI : MonoBehaviour
{
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var toggleMusic = root.Q<Toggle>("toggleMusic");
        var toggleSfx = root.Q<Toggle>("toggleSfx");
        var btnClose = root.Q<Button>("btnCloseSettings");

        toggleMusic.RegisterValueChangedCallback(evt =>
        {
            Debug.Log("Musik: " + evt.newValue);
            // Simpan pengaturan musik
        });

        toggleSfx.RegisterValueChangedCallback(evt =>
        {
            Debug.Log("SFX: " + evt.newValue);
            // Simpan pengaturan efek suara
        });

        btnClose.clicked += () =>
        {
            gameObject.SetActive(false);
        };
    }
}
