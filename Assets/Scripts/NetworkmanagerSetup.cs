using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkmanagerSetup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Check if there is already a host
        if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsClient)
            return;

        if (SystemInfo.deviceUniqueIdentifier == NetworkManager.Singleton.NetworkConfig.ConnectionData.ToString())
        {
            // Start as host if no other host exists
            Debug.Log("Starting as Host");
            NetworkManager.Singleton.StartHost();
        }
        else
        {
            // Join as client
            Debug.Log("Joining as Client");
            NetworkManager.Singleton.StartClient();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
