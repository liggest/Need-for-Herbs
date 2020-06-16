using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("移动")]
    [SerializeField] float WalkSpeed;
    [SerializeField] float AccelerateTime;
    [SerializeField] float DecelerateTime;
    [SerializeField] Vector2 InputOffset;
    [SerializeField] bool CanMove=true;

    [Header("跳跃")]
    [SerializeField] float JumpingSpeed;
    [SerializeField] float FallMultiplier;
    [SerializeField] float LowJumpMultiplier;
    [SerializeField] bool CanJump=true;


    [Header("触地判定")]
    [SerializeField] private Vector2 PointOffset;
    [SerializeField] private Vector2 Size;
    [SerializeField] private LayerMask GroundLayerMask;
    [SerializeField] bool GravityModifier=true;


    [Header("冲刺")]
    [SerializeField] bool WasDashed;
    [SerializeField] float DragForce;
    [SerializeField] float DragMaxForce;
    [SerializeField] float DragDuration;
    [SerializeField] float DashWaitTime;

    Vector2 dir;

    Rigidbody2D Rig;
    Animator Anim;
    public GameObject Bag;

    [SerializeField] bool isOnGround;
    [SerializeField] float velocityX;
    [SerializeField] bool isJumping;
    [SerializeField] bool GravitySwitch = true;

    [SerializeField] bool isAbleToCtrl = true;
    [SerializeField] bool isDead = false;

    void Awake()
    {
        Rig = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        OpenMyBag();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAbleToCtrl)
        {

            isOnGround = OnGround();

            #region 移动
            if (CanMove)
            {
                if (Input.GetAxisRaw("Horizontal") > InputOffset.x)
                {
                    if (Rig.velocity.x < WalkSpeed * Time.fixedDeltaTime * 60)
                        Rig.velocity = new Vector2(Mathf.SmoothDamp(Rig.velocity.x, WalkSpeed * Time.fixedDeltaTime * 60, ref velocityX, AccelerateTime), Rig.velocity.y);

                    transform.eulerAngles = new Vector3(0, 0, 0);
                    Anim.SetBool("isrunning", true);
                }

                else if (Input.GetAxisRaw("Horizontal") < InputOffset.x * -1)
                {
                    if (Rig.velocity.x > WalkSpeed * Time.fixedDeltaTime * 60 * -1)
                        Rig.velocity = new Vector2(Mathf.SmoothDamp(Rig.velocity.x, WalkSpeed * Time.fixedDeltaTime * 60 * -1, ref velocityX, AccelerateTime), Rig.velocity.y);
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    Anim.SetBool("isrunning", true);
                }

                else
                {
                    Rig.velocity = new Vector2(Mathf.SmoothDamp(Rig.velocity.x, 0, ref velocityX, DecelerateTime), Rig.velocity.y);
                    Anim.SetBool("isrunning", false);
                }
            }
            #endregion

            #region 跳跃
            if (CanJump)
            {
                if (Input.GetAxisRaw("Jump") == 1 && !isJumping)
                {
                    Rig.velocity = new Vector2(Rig.velocity.x, JumpingSpeed);
                    isJumping = true;
                    Anim.SetTrigger("takeof");
                }

                if (isOnGround && Input.GetAxisRaw("Jump") == 0)
                {
                    isJumping = false;

                }
                if (isOnGround)
                {
                    Anim.SetBool("isjump", false);
                }
                else
                {
                    Anim.SetBool("isjump", true);
                }
            }
            #endregion

            #region 重力控制器
            if (GravityModifier)
            {
                if (Rig.velocity.y < 0)//玩家下坠
                {
                    Rig.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1) * Time.fixedDeltaTime;//加速下坠
                }
                else if (Rig.velocity.y > 0 && Input.GetAxis("Jump") != 1)//玩家上升，并且没有按下跳跃键
                {
                    Rig.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1) * Time.fixedDeltaTime;//减缓上升
                }
            }
            #endregion

            #region 冲刺
            if (Input.GetAxisRaw("Dash") == 1 && !WasDashed)
            {
                WasDashed = true;
                dir = GetDir();
                StartCoroutine(Dash());//使用
                                       //将玩家当前所有动量清零
                Rig.velocity = Vector2.zero;
                //施加一个力，让玩家飞出去
                Rig.velocity += dir.normalized * DragForce;

            }

            if (isOnGround && Input.GetAxisRaw("Dash") == 0)
            {
                WasDashed = false;
            }
            #endregion
        }
    }

    IEnumerator Dash()
    {
        //关闭玩家各种移动和跳跃的功能
        CanMove = false;
        CanJump = false;
        //关闭重力调整器
        GravityModifier = false;
        //关闭重力影响
        Rig.gravityScale = 0;
        //施加空气阻力（Rigibody.Drag）
        DOVirtual.Float(DragMaxForce, 0, DragDuration, RigidbodyDrag);
        //等待一段时间
        yield return new WaitForSeconds(DashWaitTime);
        //开启所有关闭的东西
        CanMove = true;
        CanJump = true;
        GravityModifier = true;
        Rig.gravityScale = 1;
    }

    public void RigidbodyDrag(float x)
    {
        Rig.drag = x;
    }

    public Vector2 GetDir()
    {
        Vector2 tempDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (tempDir.x==0 && tempDir.y==0)
        {
            tempDir.x = transform.eulerAngles.y==0 ? 1 : -1;
        }

        return tempDir;
    }

    bool OnGround()
    {
        Collider2D Coll= Physics2D.OverlapBox((Vector2)transform.position + PointOffset, Size, 0, GroundLayerMask);
        bool isbool = Coll != null ? true : false;
        return isbool;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + PointOffset, Size);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dangerous")
        {
            //Debug.Log("死了");
            isAbleToCtrl = false;
            isDead = true;
            Death();
        }
    }

    void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            InventoryManager.PanelClear();
            if (Bag.activeSelf == false)
            {
                Bag.SetActive(true);
            }
            else
            {
                Bag.SetActive(false);
            }
        }
    }

    void Death()
    {
        Vector3 deathPoint = transform.position;

        //重置
        CanMove = true;
        CanJump = true;
        GravityModifier = true;
        Rig.gravityScale = 1;
        Anim.SetBool("isjump", false);
        Anim.SetBool("isrunning", false);
        Rig.velocity = Vector2.zero;
        //设置位置

        Vector3 rebrithPoint = GameCtrl.gc.GetRebrithPoint(deathPoint);
        GameCtrl.gc.Warp(transform, rebrithPoint);

        isAbleToCtrl = true;
        isDead = false;


    }
}
