using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetworkTransmitter : NetworkBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private TextMeshProUGUI text;

    private bool hostIsReady = false;
    private bool clientIsReady = false;

    [HideInInspector]
    public int currentPlayerIndex = 0;

    private void Awake() {
        // currentPlayerIndex.Value = Random.Range(0, 2);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetReadyServerRpc(ServerRpcParams serverRpcParams) {
        if (serverRpcParams.Receive.SenderClientId == OwnerClientId) {
            hostIsReady = true;
        } else {
            clientIsReady = true;
        }

        if (hostIsReady && clientIsReady) {
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
    public void SetCurrentPlayerIndexServerRpc(int index) {
        SetCurrentPlayerIndexClientRpc(index);
    }

    [ClientRpc]
    private void SetCurrentPlayerIndexClientRpc(int index) {
        currentPlayerIndex = index;
    }

    [ServerRpc(RequireOwnership = false)]
    public void SyncSpaceshipSpawningServerRpc(int spaceshipIndex, int column) {
        SyncSpaceshipSpawningClientRpc(spaceshipIndex, column);
    }

    [ClientRpc]
    private void SyncSpaceshipSpawningClientRpc(int spaceshipIndex, int column) {
        if ((int)NetworkManager.Singleton.LocalClientId != currentPlayerIndex) {
            gameManager.SyncSpaceshipSpawning(spaceshipIndex, column);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SyncTurnEndingServerRpc() {
        SyncTurnEndingClientRpc();
    }

    [ClientRpc]
    private void SyncTurnEndingClientRpc() {
        if ((int)NetworkManager.Singleton.LocalClientId == currentPlayerIndex) {
            gameManager.SyncTurnEnding();
        }
    }
}
