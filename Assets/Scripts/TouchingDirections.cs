using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{

    CapsuleCollider2D touchingCol;
    public ContactFilter2D castFilter;
    Animator anim;

    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];


    [SerializeField]
    private bool _isGrounded;
    public bool IsGrounded { get {
        return _isGrounded;
    } private set{
        _isGrounded = value;
        anim.SetBool(AnimationStrings.isGrounded, value);
    } }

    private bool _isOnWall;

    public bool IsOnWall{get{
        return _isOnWall;
    }
    set{
        _isOnWall = value;
        anim.SetBool(AnimationStrings.isOnWall, value);
    } }

    private bool _isOnCeiling;
    public bool IsOnCeiling {
        get {
            return _isOnCeiling;
        }
        set{
            _isOnCeiling = value;
            anim.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }


    private void Awake() {
        touchingCol = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    private Vector2 wallDetection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    // Update is called once per frame
    private void FixedUpdate() 
    {
       IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
       IsOnWall = touchingCol.Cast(wallDetection, castFilter, wallHits, wallDistance) > 0;
       IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
