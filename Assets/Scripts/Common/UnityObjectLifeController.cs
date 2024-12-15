using System;
using System.Collections;
using UnityEngine;

public sealed class UnityObjectLifeController
{
    public UnityObjectLifeController(
        Func<GameObject, Vector3, Quaternion, GameObject> instantiateFunc,
        Action<GameObject> destroyAction,
        Func<IEnumerator, Coroutine> startCoroutine)
    {
        _instantiateFunc = instantiateFunc;
        _destroyAction = destroyAction;
        _startCoroutine = startCoroutine;
    }

    public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        => _instantiateFunc(prefab, position, rotation);

    public void Destroy(GameObject gameObject)
        => _destroyAction(gameObject);

    public Coroutine StartCoroutine(IEnumerator routine)
        => _startCoroutine(routine);

    private readonly Func<GameObject, Vector3, Quaternion, GameObject> _instantiateFunc;
    private readonly Action<GameObject> _destroyAction;
    private readonly Func<IEnumerator, Coroutine> _startCoroutine;
}