using UnityEngine;

public sealed class FigureCreator : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private GameObject _collider;

    // на старте подписка на ивенты OnMouseDrag фигуры и на OnMouseUp фигуры
    // также на OnPlacing чтобы реализовать изменения 

    public void CreateFigure()
    {

    }
}