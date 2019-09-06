using UnityEngine;
using UnityEngine.UI;

public class PlayerNameText : MonoBehaviour
{
    private Text nameText;

    private void Start()
    {
        nameText = GetComponent<Text>();

        if (AuthManager.User != null)
        {
            nameText.text = $"Hi! {AuthManager.User.Email}";
        }
        else
        {
            nameText.text = $"ERROR : AuthManager.User == null";
        }
    }
}
