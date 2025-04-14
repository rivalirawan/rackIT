using UnityEngine;
using UnityEngine.UIElements;

public class LoginRegisterPanelUI : MonoBehaviour
{
    [SerializeField] private MainMenuUI mainMenuUI;

    private void OnEnable()
    {
        var root = GetComponentInChildren<UIDocument>().rootVisualElement;

        var panelTitle = root.Q<Label>("panelTitle");

        var loginButton = root.Q<Button>("loginButton");
        var switchToRegister = root.Q<Label>("switchToRegister");
        var loginGroup = root.Q<VisualElement>("loginGroup");
        var registerGroup = root.Q<VisualElement>("registerGroup");

        var registerButton = root.Q<Button>("registerButton");
        var switchToLogin = root.Q<Label>("switchToLogin");

        var closeBtn = root.Q<Button>("btnClose");

        // Input fields
        var emailInput = root.Q<TextField>("emailInput");
        var passwordInput = root.Q<TextField>("passwordInput");

        var registerNameInput = root.Q<TextField>("registerNameInput");
        var registerEmailInput = root.Q<TextField>("registerEmailInput");
        var registerPasswordInput = root.Q<TextField>("registerPasswordInput");
        var registerConfirmInput = root.Q<TextField>("registerConfirmInput");

        // Reset state ke Login saat panel dibuka
        loginGroup.style.display = DisplayStyle.Flex;
        registerGroup.style.display = DisplayStyle.None;
        switchToRegister.style.display = DisplayStyle.Flex;
        panelTitle.text = "Login";

        // Tombol Login
        loginButton.clicked += () =>
        {
            string email = emailInput.value;
            string password = passwordInput.value;

            Debug.Log($"Login attempt: {email}");

            StartCoroutine(FirebaseAuthManager.LoginUser(email, password, (success, message) =>
            {
                if (success)
                {
                    Debug.Log(message);

                    PlayerPrefs.SetString("email", email); // auto-login simpan email

                    mainMenuUI?.OnLoginSuccess(email); // PANGGIL ini untuk update UI langsung
                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogWarning(message);
                }
            }));
        };

        // Tombol Close Panel
        closeBtn.clicked += () =>
        {
            gameObject.SetActive(false);
        };

        // Switch ke Register
        switchToRegister.RegisterCallback<ClickEvent>(evt =>
        {
            panelTitle.text = "Register";
            loginGroup.style.display = DisplayStyle.None;
            registerGroup.style.display = DisplayStyle.Flex;
            switchToRegister.style.display = DisplayStyle.None;

            // Optional: Reset input
            registerNameInput.value = "";
            registerEmailInput.value = "";
            registerPasswordInput.value = "";
            registerConfirmInput.value = "";
        });

        // Tombol Register
        registerButton.clicked += () =>
        {
            string nama = registerNameInput.value;
            string email = registerEmailInput.value;
            string pass = registerPasswordInput.value;
            string confirm = registerConfirmInput.value;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                Debug.LogWarning("Email dan password wajib diisi.");
                return;
            }

            if (pass != confirm)
            {
                Debug.LogWarning("Password tidak cocok!");
                return;
            }

            StartCoroutine(FirebaseAuthManager.RegisterUser(email, pass, (success, message) =>
            {
                if (success)
                {
                    PlayerPrefs.SetString("email", email);
                    Debug.Log(message);
                    panelTitle.text = "Login";
                    registerGroup.style.display = DisplayStyle.None;
                    loginGroup.style.display = DisplayStyle.Flex;
                    switchToRegister.style.display = DisplayStyle.Flex;
                }
                else
                {
                    Debug.LogWarning(message);
                }
            }));
        };

        // Switch back ke Login
        switchToLogin.RegisterCallback<ClickEvent>(evt =>
        {
            panelTitle.text = "Login";
            registerGroup.style.display = DisplayStyle.None;
            loginGroup.style.display = DisplayStyle.Flex;
            switchToRegister.style.display = DisplayStyle.Flex;
        });
    }
}
