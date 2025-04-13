using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject loginPanelGO;     // drag LoginRegisterPanel ke sini
    public GameObject guidePanelGO;     // drag GuidePanel ke sini
    public GameObject settingsPanelGO;  // drag SettingsPanel ke sini

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var startBtn = root.Q<Button>("btnStart");
        var guideBtn = root.Q<Button>("btnGuide");
        var exitBtn = root.Q<Button>("btnExit");
        var settingsBtn = root.Q<Button>("btnSettings");
        var loginBtn = root.Q<Button>("btnLogin");

        //startBtn.clicked += () => SceneManager.LoadScene("Perakitan");

        guideBtn.clicked += () =>
        {
            if (guidePanelGO != null)
            {
                guidePanelGO.SetActive(true); // tampilkan panel panduan
            }
        };

        settingsBtn.clicked += () =>
        {
            if (settingsPanelGO != null)
            {
                settingsPanelGO.SetActive(true); // tampilkan panel pengaturan
            }
        };

        loginBtn.clicked += () =>
        {
            if (loginPanelGO != null)
            {
                loginPanelGO.SetActive(true); // tampilkan panel login
            }
        };

        exitBtn.clicked += () =>
        {
            Application.Quit();
        };
    }
}
