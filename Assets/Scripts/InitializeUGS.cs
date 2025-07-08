
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

public class InitializeUGS : MonoBehaviour
{
    public string environment = "production";
    async void Awake()
    {
        try
        {
            var options = new InitializationOptions().SetEnvironmentName(environment);
            await UnityServices.InitializeAsync(options);
            Debug.Log("UGS Initialized Successfully");
        }
        catch
        {
            Debug.Log("UGS Initialization Failed!");
        }
    }
}
