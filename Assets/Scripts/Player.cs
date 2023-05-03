using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Bounds _exitBounds;
    [SerializeField] private GameObject _exit;
    private SpriteRenderer _renderer;
    private PolygonCollider2D _collider;
    private Rigidbody2D _rb;
    private Animator _anim;
    private Color _color;
    private int _currentWayPoint = 0;
   
    private List<Vector2> _way = new List<Vector2>();
    public float Delay{ get; set;}
    public int WayLength { get { return _way.Count;} }
    public bool HaveWay { get; private set;}
    public bool Move {get; set;}
    public bool GetToExit{ get; set;}
    private const int PLAYER_TAG = 3;
    private const int OBSTACLE_TAG = 8;
    private void Start() 
    {
        _exitBounds = _exit.GetComponent<BoxCollider2D>().bounds;

        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<PolygonCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _color = _renderer.color;
        _exit.GetComponent<SpriteRenderer>().color = _color;
        
        HaveWay = false;
        Move = false;
        GetToExit = false;
    }
    // private void FixedUpdate()
    // {
    //     if(Move) MoveCycle();
    // }
    // private void MoveCycle()
    // {
    //     transform.position = Vector2.MoveTowards(_way[_currentWayPoint - 1], 
    //             _way[_currentWayPoint], Time.deltaTime * 0.0001f);
    //     _currentWayPoint++;
    //     if(_currentWayPoint == _way.Count) 
    //     {
    //          Move = false;
    //          GetToExit = true;
    //     }
    // }

    public IEnumerator MoveCharacter()
    {
        while(Move)
        {
            _anim.SetBool("Move", true);
            _rb.MovePosition(_way[_currentWayPoint]);
            _currentWayPoint++;
            if(_currentWayPoint == WayLength) 
            {
                _anim.SetBool("Move", false);
                Move = false;
                GetToExit = true;
                MoveManager.Instance.WaysPassed();
            }

            yield return new WaitForSeconds(Delay);
        }
    }
    private void OnMouseDown()
    {
        if(!HaveWay)
        {
            DrawManager.SetLineColor(_color);
            DrawManager.Instance.SetState(true);
        }
    }
    private void OnMouseUp()
    {
        DrawManager.Instance.RemoveCopy();

        if(_exitBounds.Contains(DrawManager.Instance.GetEndOfLine()) && !HaveWay)
        {
            _way = DrawManager.Instance.GetWay().ToList();
            DrawManager.Instance.SpawnLine();

            Delay = MoveManager.WAY_TIME / (2 * WayLength);
            HaveWay = true;

            MoveManager.Instance.CheckAllWays();
        }

        DrawManager.Instance.SetState(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == PLAYER_TAG || other.gameObject.layer == OBSTACLE_TAG)
        {
            SceneBehavior.Instance.PlayersFailed();
            Move = false;
            Time.timeScale = 0f;
        }
    }
}
