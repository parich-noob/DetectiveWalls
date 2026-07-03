using TMPro;
using UnityEngine;

public class SafeUIManager : MonoBehaviour
{
    public GameObject passwordPanel;
    public TMP_InputField inputField;
    public SafeController safeController;

    private const string correctCode = "524081";

    public void ShowPanel()
    {
        passwordPanel.SetActive(true);
    }

    public void HidePanel()
    {
        passwordPanel.SetActive(false);
    }

    public void CheckCode()
    {
        if (inputField.text == correctCode)
        {
            safeController.OpenSafe();
            HidePanel();
            AudioManager.Instance.PlayChest();
        }
        else
        {
            Debug.Log("Wrong Code");
            inputField.text = "";
        }
    }
}