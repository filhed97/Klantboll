using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkMultiplayer : NetworkBehaviour
{
    Rigidbody rb;
    public float playerSpeed = 5;
    public float MaxVelocity = 15;
    [SerializeField] private Transform ball;
    [SerializeField] private Transform Stewie;
    private static Transform spawnedBall;

    //public bool hasPowerup = false;
    public NetworkVariable<bool> hasPowerup;

    private Vector3[] spawnPos = new Vector3[2];

    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        hasPowerup = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        spawnPos = new Vector3[] { new Vector3(10, 0, 0), new Vector3(-10, 0, 0) };
        var clientId = NetworkManager.Singleton.LocalClientId;
        Stewie.position = spawnPos[clientId];
        Debug.Log("clientid: " + clientId);

        rb = GetComponent<Rigidbody>();
        if (IsOwner && NetworkManager.Singleton.IsServer)
        {
            spawnedBall = Instantiate(ball);
            spawnedBall.GetComponent<NetworkObject>().Spawn(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction.Normalize();
        rb.velocity += direction * playerSpeed * Time.deltaTime;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxVelocity);

        /*if (direction != Vector3.zero)
        {
            rb.transform.forward = direction;
        }*/
    }
  

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("COLLISION");
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