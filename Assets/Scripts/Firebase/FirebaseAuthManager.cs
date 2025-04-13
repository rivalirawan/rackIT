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
        var authRequest = new FirebaseAuthRequest
        {
            email = email,
            password = password,
            returnSecureToken = true
        };

        string jsonBody = JsonUtility.ToJson(authRequest);

        using UnityWebRequest request = new UnityWebRequest(FirebaseAuthSignUpUrl + FirebaseConfig.WebAPIKey, "POST");
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            callback(true, "Registrasi berhasil!");
        }
        else
        {
            string errorMessage = ParseFirebaseError(request.downloadHandler.text);
            callback(false, $"Registrasi gagal: {errorMessage}");
        }
    }

    public static IEnumerator LoginUser(string email, string password, Action<bool, string> callback)
    {
        var authRequest = new FirebaseAuthRequest
        {
            email = email,
            password = password,
            returnSecureToken = true
        };

        string jsonBody = JsonUtility.ToJson(authRequest);

        using UnityWebRequest request = new UnityWebRequest(FirebaseAuthSignInUrl + FirebaseConfig.WebAPIKey, "POST");
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            callback(true, "Login berhasil!");
        }
        else
        {
            string errorMessage = ParseFirebaseError(request.downloadHandler.text);
            callback(false, $"Login gagal: {errorMessage}");
        }
    }

    // Optional: parser untuk mengambil pesan error dari response Firebase
    private static string ParseFirebaseError(string json)
    {
        try
        {
            var errorWrapper = JsonUtility.FromJson<FirebaseErrorWrapper>(json);
            return errorWrapper.error.message;
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
