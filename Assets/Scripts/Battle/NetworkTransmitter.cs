using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetworkTransmitter : NetworkBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private TextMeshProUGUI text;

    private NetworkVariable<bool> hostIsReady = new(false);
    private NetworkVariable<bool> clientIsReady = new(false);

    public NetworkVariable<int> currentPlayerIndex = new(0);

    private void Awake() {
        currentPlayerIndex.Value = Random.Range(0, 2);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetReadyServerRpc(ServerRpcParams serverRpcParams) {
        if (serverRpcParams.Receive.SenderClientId == OwnerClientId) {
            hostIsReady.Value = true;
        } else {
            clientIsReady.Value = true;
        }

        text.SetText(NetworkManager.Singleton.LocalClientId + "\n" + currentPlayerIndex.Value);

        if (hostIsReady.Value && clientIsReady.Value) {
            SetReadyClientRpc();
        }
    }

    [ClientRpc]
    private void SetReadyClientRpc() {
        gameManager.StartGame();
    }

    public int GetPlayerId() {
        return (int)NetworkManager.Singleton.LocalClientId;
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetCurrentPlayerInderServerRpc(int index) {
        currentPlayerIndex.Value = index;
    }
}
