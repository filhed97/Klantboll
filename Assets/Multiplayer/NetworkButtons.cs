using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkButtons : NetworkBehaviour
{
    [SerializeField] private Button HostBtn;
    [SerializeField] private Button ClientBtn;
    [SerializeField] private TMPro.TMP_InputField input;

    // Update is called once per frame
    private void Awake()
    {
        HostBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
        });

        ClientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = input.text;
            NetworkManager.Singleton.StartClient();
        });
    }
}