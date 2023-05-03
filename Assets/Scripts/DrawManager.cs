using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public static DrawManager Instance{ get; private set;}
    [SerializeField] private Line _line;
    [SerializeField] private Line _visibleCopy;
    private Line _currentLine;
    private Camera _cam;
    private static Color _brushColor = Color.white;
    public bool Draw{ get; set;} 
    public const float LINE_RESOLUTION = 0.1f;
    private Vector2 _startPosition;
    private void Awake()
    {
        Instance = this;
        _visibleCopy.ClearLine();
    }
    private void Start() 
    {
        _line.ClearLine();
        _cam = Camera.main;
        gameObject.SetActive(false);
        Draw = false;
    }

    private void Update() 
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButtonDown(0))
        {
            _startPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            _line.SetRendererColor(_brushColor);
            _visibleCopy.SetRendererColor(_brushColor);
            
            _line.ClearLine();
            _visibleCopy.ClearLine();
            Draw = true;
        }

        if(Input.GetMouseButton(0)) 
        {
            if(Draw)
            {
                _line.SetPosition(mousePos);
                _visibleCopy.SetPosition(mousePos);
            }
            else
            {
                _line.ClearLine();
                _visibleCopy.ClearLine();
                Draw = false;
            }
        }

        if(Input.GetMouseButtonUp(0)) 
        {
            _visibleCopy.ClearLine();
            Draw = false;
        }

    }

    public void SetState(bool state)
    {
        gameObject.SetActive(state);
    }
    
    public static void SetLineColor(Color cl)
    {
        _brushColor = cl;
    }

    public Vector2 GetEndOfLine()
    {
        return _line.GetLastPosition();
    }
    public void RemoveCopy()
    {
        _visibleCopy.ClearLine();
    }
    public List<Vector2> GetWay()
    {
        return _line.Points;
    }
    public void SpawnLine()
    {
        _currentLine = Instantiate(_line, _startPosition, Quaternion.identity);
    }
}