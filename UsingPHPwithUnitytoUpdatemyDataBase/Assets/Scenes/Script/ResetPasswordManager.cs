using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
public class ResetPasswordManager : MonoBehaviour
{
    public InputField usernameField;
    public InputField newPasswordField;
    public Text resultText;

    public void StartResetPassword()
    {
        StartCoroutine(ResetPassword());
    }

    IEnumerator ResetPassword()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("new_password", newPasswordField.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unity_api/reset_password.php", form))
        {
            yield return www.SendWebRequest();

            string response = www.downloadHandler.text;
            Debug.Log("Response: " + response);

            if (response.Contains("success"))
            {
                resultText.text = "Password updated!";
            }
            else if (response.Contains("user_not_found"))
            {
                resultText.text = "User not found!";
            }
            else
            {
                resultText.text = "Error updating password";
            }
        }
    }
}
