using UnityEngine;
using Zenject;

public class GlobalProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("Global bindings");
        // global bindings
    }
}
