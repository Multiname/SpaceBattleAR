using System;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RelayManager : MonoBehaviour
{
    public Action<ulong> OnHostReady = null;

    private async void Start() {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += HandleSignIn;

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        OnHostReady = (id) => {
            if (id != 0) {
                NetworkManager.Singleton.SceneManager.LoadScene("Battle", LoadSceneMode.Single);
            }
        };
        NetworkManager.Singleton.OnClientConnectedCallback += OnHostReady;
    }

    private void OnDestroy() {
        if (OnHostReady != null && NetworkManager.Singleton != null) {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnHostReady;
        }
    }

    private void HandleSignIn() {
        Debug.Log("Signed in: " + AuthenticationService.Instance.PlayerId);
    }

    public void SignOut() {
        AuthenticationService.Instance.SignOut();
        AuthenticationService.Instance.SignedIn -= HandleSignIn;
    }

    public async Task<string> CreateRelay() {
        string joinCode = "";
        try {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);

            joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            Debug.Log(joinCode);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
            );

            NetworkManager.Singleton.StartHost();
        } catch (RelayServiceException e) {
            Debug.Log(e);
        }

        return joinCode;
    }

    public async Task<bool> JoinRelay(string joinCode) {
        try {
            Debug.Log("Joining Relay with: " + joinCode);

            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                joinAllocation.RelayServer.IpV4,
                (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData
            );

            NetworkManager.Singleton.StartClient();
            return true;
        } catch (RelayServiceException e) {
            Debug.Log(e);
        }
        return false;
    }
}
