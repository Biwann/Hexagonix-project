
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("Scene bindings");
        // all scene bindings
    }
}
