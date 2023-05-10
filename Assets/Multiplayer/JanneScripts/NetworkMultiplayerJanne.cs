using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkMultiplayerJanne : NetworkBehaviour
{
    Rigidbody rb;
    [SerializeField] private Transform ball;
    [SerializeField] private Transform Player2;
    private static Transform spawnedBall;
    public NetworkVariable<bool> hasPowerup;
    private Vector3[] spawnPos = new Vector3[2];

    public override void OnNetworkSpawn()
    {
        hasPowerup = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        spawnPos = new Vector3[] { new Vector3(10, 0, 0), new Vector3(-10, 0, 0) };
        var clientId = NetworkManager.Singleton.LocalClientId;
        Player2.position = spawnPos[clientId];
        Debug.Log("clientid: " + clientId);

        rb = GetComponent<Rigidbody>();
        if (IsOwner && NetworkManager.Singleton.IsServer)
        {
            spawnedBall = Instantiate(ball);
            spawnedBall.GetComponent<NetworkObject>().Spawn(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
            Debug.Log("COLLISION");
        if (other.CompareTag("Ball"))
        {
            RequestOwnershipServerRpc();
        }
    }

    [ServerRpc]
    public void RequestOwnershipServerRpc(ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        spawnedBall.GetComponent<NetworkObject>().ChangeOwnership(clientId);
    }
}