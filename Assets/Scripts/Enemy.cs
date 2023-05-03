using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 0.05f;
    [Header("Moving points")]
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;
    private Animator _anim;
    private Vector2 _direction;
    
    private void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetBool("Move", true);
        _direction = _point2.position - _point1.position;
    }

    private void FixedUpdate()
    {
        if(transform.position == _point1.position || transform.position == _point2.position)
        {
            _speed *= -1;
            transform.localScale = new Vector2(transform.localScale.x * (-1), transform.localScale.y);

        }
        transform.Translate(_direction.normalized *_speed);
    }
}
