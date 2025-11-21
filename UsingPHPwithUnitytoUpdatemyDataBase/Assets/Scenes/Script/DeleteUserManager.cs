using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class DeleteUserManager : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Text resultText;

    public void StartDeleteUser()
    {
        StartCoroutine(DeleteUser());
    }

    IEnumerator DeleteUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unity_api/delete_user_secure.php", form))
        {
            yield return www.SendWebRequest();

            string response = www.downloadHandler.text;
            Debug.Log(response);

            if (response.Contains("success"))
                resultText.text = "User deleted successfully.";

            else if (response.Contains("user_not_found"))
                resultText.text = "User does not exist.";

            else if (response.Contains("wrong_password"))
                resultText.text = "Incorrect password.";

            else
                resultText.text = "Error deleting user.";
        }
    }
}
