using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class playermove : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    public float moveSpeed = 7f; // �����ٶ�  
    public float minSpeed = 2f;//�����ٶ�
    MovementState state;
    private Danmaku danmakuManager;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float jumpSpeedRatio = 0.1f;

    private enum MovementState { idle ,running,jumping,falling}//����ö��
    // Start is called before the first frame update
    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        danmakuManager = FindObjectOfType<Danmaku>();
    }
    //private bool IsDanmakuVisible()
    //{
    //    // ���danmakuManagerΪnull���򷵻�false������������׳�һ������  
    //    if (danmakuManager == null)
    //    {
    //        return false;
    //    }

    //    // ����Danmaku���IsDanmakuActive���Ե�ֵ  
    //    return danmakuManager.IsDanmakuActive;
    //}
    // Update is called once per frame
    private void Update()
    {
        var flag = IsGrounded();
        if (flag || state == MovementState.falling) dirX = Input.GetAxisRaw("Horizontal");
        var deltaSpeedX = dirX * moveSpeed * Time.deltaTime;
        if (flag && Mathf.Abs(rb.velocity.x + deltaSpeedX) < moveSpeed)
            rb.velocity += new Vector2(deltaSpeedX, 0f);
        if (state == MovementState.falling && Mathf.Abs(rb.velocity.x + deltaSpeedX * jumpSpeedRatio) < moveSpeed)
            rb.velocity += new Vector2(deltaSpeedX * jumpSpeedRatio, 0f);
        if(Input.GetButtonDown("Jump") && flag)
        {
            rb.velocity += new Vector2(0f, jumpForce);
        }
        //if (IsDanmakuVisible())
        //{
        //    rb.velocity = new Vector2(rb.velocity.x * (slowSpeed / moveSpeed), rb.velocity.y);
        //    // �ı������ٶ�
        //}
        //else
        //{
        // rb.velocity += new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rb.velocity.y);
    UpdateAnimationState(); 
    }

 private  void UpdateAnimationState()
    {
     if(dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
     else if(dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {

            state = MovementState.idle;
        }
     if(rb.velocity.y>.1f)
        {
            state = MovementState.jumping;
        }
     else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);

    }

       private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    public void ChangeSpeed(float speed)
    {
        if (moveSpeed + speed >= minSpeed)
        {
            moveSpeed += speed;
        }
    }
}
