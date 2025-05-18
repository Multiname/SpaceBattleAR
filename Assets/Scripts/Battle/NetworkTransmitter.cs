using Unity.Netcode;
using UnityEngine;

public class NetworkTransmitter : NetworkBehaviour {
    [SerializeField]
    private GameManager gameManager;

    private bool hostIsReady = false;
    private bool clientIsReady = false;

    private int playerIndex = 0;

    [HideInInspector]
    public int currentPlayerIndex = 0;

    private void Awake() {
        if (NetworkManager.Singleton.LocalClientId != 0) {
            playerIndex = 1;
        }
        currentPlayerIndex = Random.Range(0, 2);
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
        return playerIndex;
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
        if (playerIndex != currentPlayerIndex) {
            gameManager.SyncSpaceshipSpawning(spaceshipIndex, column);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SyncTurnEndingServerRpc() {
        SyncTurnEndingClientRpc();
    }

    [ClientRpc]
    private void SyncTurnEndingClientRpc() {
        if (playerIndex == currentPlayerIndex) {
            gameManager.SyncTurnEnding();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SyncSpaceshipAttackingServerRpc(int attackerIndex, int targetIndex) {
        SyncSpaceshipAttackingClientRpc(attackerIndex, targetIndex);
    }

    [ClientRpc]
    private void SyncSpaceshipAttackingClientRpc(int attackerIndex, int targetIndex) {
        if (playerIndex != currentPlayerIndex) {
            gameManager.SyncSpaceshipAttacking(attackerIndex, targetIndex);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SyncSpaceshipForwardMovingServerRpc(int spaceshipIndex) {
        SyncSpaceshipForwardMovingClientRpc(spaceshipIndex);
    }

    [ClientRpc]
    private void SyncSpaceshipForwardMovingClientRpc(int spaceshipIndex) {
        if (playerIndex != currentPlayerIndex) {
            gameManager.SyncSpaceshipForwardMoving(spaceshipIndex);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SyncSpaceshipEliminatingServerRpc(int casterIndex, int targetIndex) {
        SyncSpaceshipEliminatingClientRpc(casterIndex, targetIndex);
    }

    [ClientRpc]
    private void SyncSpaceshipEliminatingClientRpc(int casterIndex, int targetIndex) {
        if (playerIndex != currentPlayerIndex) {
            gameManager.SyncSpaceshipEliminating(casterIndex, targetIndex);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void EndMatchServerRpc() {
        DeclareWinnerClientRpc();
    }

    [ClientRpc]
    private void DeclareWinnerClientRpc() {
        if (playerIndex != currentPlayerIndex) {
            gameManager.DeclareWinner();
            LeaveBattle();
        }
    }

    public void LeaveBattle() {
        NetworkManager.Singleton.Shutdown();
    }
}
