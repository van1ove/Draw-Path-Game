using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private EdgeCollider2D _collider;
    private readonly List<Vector2> _points = new List<Vector2>();
    public List<Vector2> Points { get { return _points;} }

    private const int WALL_LAYER = 9;
    
    private void Start() 
    {
        _collider.transform.position -= transform.position;    
    }
    public void SetPosition(Vector2 position)
    {
        if(!CanAppend(position)) return;
        Points.Add(position);

        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, position);

        _collider.points = Points.ToArray();
    }

    private bool CanAppend(Vector2 position)
    {
        if(_lineRenderer.positionCount == 0) return true;
        return Vector2.Distance(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1), position) > DrawManager.LINE_RESOLUTION;
    }

    public void SetRendererColor(Color cl)
    {
        _lineRenderer.startColor = cl;
        _lineRenderer.endColor = cl;
    }
    public void ClearLine()
    {
        Points.Clear();
        _collider.points = Points.ToArray();
        _lineRenderer.positionCount = 0;
    }
    public Vector2 GetLastPosition()
    {
        // Vector2(2000, 2000) - тояка за пределами поля
        return (_lineRenderer.positionCount > 0) ? _lineRenderer.GetPosition(_lineRenderer.positionCount - 1) 
            : new Vector2(2000, 2000);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == WALL_LAYER)
        {
            DrawManager.Instance.Draw = false;
        }    
    }
}
