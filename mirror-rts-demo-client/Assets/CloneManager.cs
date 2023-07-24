//
#if UNITY_EDITOR
using UnityEngine;
using ParrelSync;

public class CloneManager : MonoBehaviour
{
    public NetworkManagerCustom _networkManager;
    // Start is called before the first frame update
    void Start()
    {
        if (ClonesManager.IsClone()) {
            _networkManager.StartClient();
        } else {
            _networkManager.StartHost();
        }
    }
}
#endif