using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private enum WindowState {
        SELECTION,
        HOST,
        CLIENT
    }

    private WindowState windowState = WindowState.SELECTION;

    [SerializeField]
    private GameObject startButtons;
    [SerializeField]
    private GameObject hostPanel;
    [SerializeField]
    private GameObject clientPanel;

    [SerializeField]
    private TextMeshProUGUI codeText;
    [SerializeField]
    private TMP_InputField codeInputField;

    [SerializeField]
    private RelayManager relayManager;

    public void GoBack() {
        switch (windowState) {
            case WindowState.SELECTION:
                ReturnToMenu();
                break;
            case WindowState.HOST:
                CancelHosting();
                break;
            case WindowState.CLIENT:
                ReturnToSelection();
                break;
        }
    }

    private void ReturnToMenu() {
        relayManager.SignOut();
        SceneManager.LoadScene("Menu");
    }

    private void CancelHosting() {
        relayManager.Shutdown();

        windowState = WindowState.SELECTION;
        codeText.SetText("");
        hostPanel.SetActive(false);
        startButtons.SetActive(true);
    }

    private void ReturnToSelection() {
        windowState = WindowState.SELECTION;
        codeInputField.text = "";
        clientPanel.SetActive(false);
        startButtons.SetActive(true);
    }

    public async void StartHost() {
        windowState = WindowState.HOST;
        startButtons.SetActive(false);
        hostPanel.SetActive(true);

        string joinCode = await relayManager.CreateRelay();
        codeText.SetText(joinCode);
    }

    public void StartClient() {
        windowState = WindowState.CLIENT;
        startButtons.SetActive(false);
        clientPanel.SetActive(true);
    }

    public async void ConnectClient() {
        string joinCode = codeInputField.text;
        if (joinCode != "") {
            await relayManager.JoinRelay(joinCode);
        }
    }
}
