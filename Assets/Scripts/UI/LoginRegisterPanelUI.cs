using UnityEngine;
using UnityEngine.UIElements;

public class LoginRegisterPanelUI : MonoBehaviour
{
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

        // Reset display visibility
        loginGroup.style.display = DisplayStyle.Flex;
        registerGroup.style.display = DisplayStyle.None;
        switchToRegister.style.display = DisplayStyle.Flex;
        panelTitle.text = "Login";

        // Hapus semua event listener sebelumnya (opsional, jika pakai RegisterCallback, ini aman)
        loginButton.clicked += () =>
        {
            string email = root.Q<TextField>("emailInput").value;
            string password = root.Q<TextField>("passwordInput").value;

            Debug.Log($"Login attempt: {email} / {password}");
            // TODO: koneksi ke backend
        };

        closeBtn.clicked += () =>
        {
            gameObject.SetActive(false); // hide panel login
        };

        switchToRegister.RegisterCallback<ClickEvent>(evt =>
        {
            panelTitle.text = "Register";
            registerGroup.style.display = DisplayStyle.Flex;
            loginGroup.style.display = DisplayStyle.None;
            switchToRegister.style.display = DisplayStyle.None;
        });

        registerButton.clicked += () =>
        {
            string nama = root.Q<TextField>("registerNameInput").value;
            string email = root.Q<TextField>("registerEmailInput").value;
            string pass = root.Q<TextField>("registerPasswordInput").value;
            string confirm = root.Q<TextField>("registerConfirmInput").value;

            if (pass != confirm)
            {
                Debug.LogWarning("Password tidak cocok!");
                return;
            }

            Debug.Log($"Registering: {nama} / {email}");
            // TODO: koneksi ke backend
        };

        switchToLogin.RegisterCallback<ClickEvent>(evt =>
        {
            panelTitle.text = "Login";
            registerGroup.style.display = DisplayStyle.None;
            loginGroup.style.display = DisplayStyle.Flex;
            switchToRegister.style.display = DisplayStyle.Flex;
        });
    }
}
