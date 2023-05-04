using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkButtons : NetworkBehaviour
{
    [SerializeField] private Button HostBtn;
    [SerializeField] private Button ClientBtn;

    // Update is called once per frame
    private void Awake()
    {
        HostBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
        });

        ClientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}