using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class NetworkPortManager : MonoBehaviour
{
    [SerializeField] private ushort hostPort = 7778;
    [SerializeField] private ushort clientPort = 7777;

    void Start()
    {
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

        if (transport == null)
        {
            Debug.LogError("UnityTransport component not found on NetworkManager.");
            return;
        }

        if (Application.isEditor || !Application.isBatchMode)
        {
            // Example buttons or key commands to start host or client
            Debug.Log("Press H to start Host, C to start Client.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartHostWithPort();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            StartClientWithPort();
        }
    }

    public void StartHostWithPort()
    {
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.ConnectionData.Port = hostPort;
        NetworkManager.Singleton.StartHost();
        Debug.Log($"Host started on port {hostPort}");
    }

    public void StartClientWithPort()
    {
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.ConnectionData.Port = clientPort;
        NetworkManager.Singleton.StartClient();
        Debug.Log($"Client started on port {clientPort}");
    }
}
