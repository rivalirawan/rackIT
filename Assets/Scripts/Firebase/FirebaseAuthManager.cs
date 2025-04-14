using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class FirebaseAuthRequest
{
    public string email;
    public string password;
    public bool returnSecureToken = true;
}

public static class FirebaseAuthManager
{
    private const string FirebaseAuthSignUpUrl = "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=";
    private const string FirebaseAuthSignInUrl = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=";

    public static IEnumerator RegisterUser(string email, string password, Action<bool, string> callback)
    {
        yield return SendAuthRequest(
            FirebaseAuthSignUpUrl + FirebaseConfig.WebAPIKey,
            email, password,
            successMsg: "Registrasi berhasil!",
            failurePrefix: "Registrasi gagal:",
            callback
        );
    }

    public static IEnumerator LoginUser(string email, string password, Action<bool, string> callback)
    {
        yield return SendAuthRequest(
            FirebaseAuthSignInUrl + FirebaseConfig.WebAPIKey,
            email, password,
            successMsg: "Login berhasil!",
            failurePrefix: "Login gagal:",
            callback
        );
    }

    private static IEnumerator SendAuthRequest(string url, string email, string password, string successMsg, string failurePrefix, Action<bool, string> callback)
    {
        var authRequest = new FirebaseAuthRequest
        {
            email = email,
            password = password
        };

        string jsonBody = JsonUtility.ToJson(authRequest);
        using UnityWebRequest request = new UnityWebRequest(url, "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonBody)),
            downloadHandler = new DownloadHandlerBuffer()
        };
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Simpan email ke PlayerPrefs
            PlayerPrefs.SetString("user_email", email);
            PlayerPrefs.Save();

            callback(true, successMsg);
        }
        else
        {
            string errorMessage = ParseFirebaseError(request.downloadHandler.text);
            callback(false, $"{failurePrefix} {errorMessage}");
        }
    }

    private static string ParseFirebaseError(string json)
    {
        try
        {
            var errorWrapper = JsonUtility.FromJson<FirebaseErrorWrapper>(json);
            return errorWrapper?.error?.message ?? "Tidak diketahui.";
        }
        catch
        {
            return "Terjadi kesalahan tidak dikenal.";
        }
    }

    [Serializable]
    private class FirebaseErrorWrapper
    {
        public FirebaseError error;
    }

    [Serializable]
    private class FirebaseError
    {
        public string message;
    }
}
