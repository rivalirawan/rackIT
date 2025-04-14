using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject loginPanelGO;
    public GameObject guidePanelGO;
    public GameObject settingsPanelGO;

    private Label labelUserName;
    private Button loginBtn;
    private Button logoutBtn;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var startBtn = root.Q<Button>("btnStart");
        var guideBtn = root.Q<Button>("btnGuide");
        var exitBtn = root.Q<Button>("btnExit");
        var settingsBtn = root.Q<Button>("btnSettings");
        loginBtn = root.Q<Button>("btnLogin");
        logoutBtn = root.Q<Button>("btnLogout");
        labelUserName = root.Q<Label>("labelUserName");

        startBtn.clicked += () => SceneManager.LoadScene("Perakitan");

        guideBtn.clicked += () =>
        {
            if (guidePanelGO != null) guidePanelGO.SetActive(true);
        };

        settingsBtn.clicked += () =>
        {
            if (settingsPanelGO != null) settingsPanelGO.SetActive(true);
        };

        loginBtn.clicked += () =>
        {
            if (loginPanelGO != null) loginPanelGO.SetActive(true);
        };

        logoutBtn.clicked += Logout;

        exitBtn.clicked += () =>
        {
            Application.Quit();
        };

        // Auto-login check
        if (PlayerPrefs.HasKey("email"))
        {
            string email = PlayerPrefs.GetString("email");
            labelUserName.text = email;
            loginBtn.style.display = DisplayStyle.None;
            logoutBtn.style.display = DisplayStyle.Flex;
        }
        else
        {
            labelUserName.text = "";
            loginBtn.style.display = DisplayStyle.Flex;
            logoutBtn.style.display = DisplayStyle.None;
        }
    }
    public void OnLoginSuccess(string email)
    {
        labelUserName.text = email;
        loginBtn.style.display = DisplayStyle.None;
        logoutBtn.style.display = DisplayStyle.Flex;
    }
    
    private void Logout()
    {
        PlayerPrefs.DeleteKey("email");
        labelUserName.text = "";
        loginBtn.style.display = DisplayStyle.Flex;
        logoutBtn.style.display = DisplayStyle.None;
    }
}
