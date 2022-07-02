using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NewMainController  : MonoBehaviour
{
    Rigidbody2D rigid;
    public float moveSpeed = 40f;
    float currentMove;
    //bool isMove = false;

    public bool useForce = false;

    public float jumpSpeed = 10f;
    float jumpMove;

    SpriteRenderer Srenderer;

    Animator anim;
    //attack c?a main character
    //public Transform attackPoint;
    //public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public LayerMask interactLayers;
    //L?y game object v? ??n có s?n trong prefab ?? ?i?u khi?n vi?c b?n ??n
    //public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        //Kh?i t?o l?y các thành ph?n tr??c
        rigid = GetComponent<Rigidbody2D>();

        //Gán giá tr? v? t?c ?? ?i và nh?y ban ??u là 0
        currentMove = 0;
        jumpMove = 0;

        Srenderer = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {

    }
    // L?u ý: x? lý input thì ? update, x? lý v?t lý ? fixed update
    public void Update()
    {
        //Tr??ng h?p b?n ??n, khi nh?n phím A trên bàn phím s? b?n viên ??n v?i v? trí t?a ??:
        //v? trí t?a ?? x hi?n t?i + 1, y và z gi? nguyên, góc quay gi? nguyên, xài theo h? Quaternion thay vì Euler
        //if (Input.GetKeyDown(KeyCode.A))
        //    Instantiate(bullet, new Vector3(transform.position.x + 1f,
        //        transform.position.y, transform.position.z), Quaternion.identity);



        //Gán t?c ?? nh?y ch? ko x? tr?c ti?p ? update, x? lý v?t lý ? fixed update
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpMove = jumpSpeed;
        }

        //Gán t?c ?? ?i ch? ko x? tr?c ti?p ? update, x? lý v?t lý ? fixed update
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //?i trái thì h??ng ph?i ng??c l?i, do ?ó có giá tr? âm
            currentMove = -moveSpeed;

            //Xoay hình sang bên trái
            Srenderer.flipX = true;

            //G?i animation walking ch?y 1 l?n
            anim.Play("moving");
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //?i ph?i thì h??ng cùng chi?u, do ?ó có giá tr? d??ng
                currentMove = moveSpeed;

                //Xoay hình sang bên trái
                Srenderer.flipX = false;

                //G?i animation walking ch?y 1 l?n
                anim.Play("moving");
            }
            else
            {
                //N?u không di chuy?n (không nh?n nút nào), gán giá tr? t?c ?? = 0 (??ng yên)
                currentMove = 0;
            }
        }
        if (Input.GetKey("z"))
        {
            Interact();
        }    

    }
    void Interact() {
        var facingDir = new Vector2(2f, 2f);
        var interactPos = rigid.position + facingDir;
        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactLayers);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }
    //X? lý v?t lý ? fixed update
    private void FixedUpdate()
    {
        //Làm ?óng b?ng góc xoay ?? tránh b? tác ??ng c?a l?c v?t lý làm nhân v?t l?n l?n
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

        //2 cách ?? di chuy?n nh?n v?t: 
        // Cách 1: thay ??i v?n t?c ??u, giá tr? v?n t?c ?ã c?p nh?t trên Update()
        if (!useForce)
            rigid.velocity = new Vector2(currentMove, rigid.velocity.y + jumpMove);

        //Cách 2: Tác ??ng 1 l?c theo chi?u x (di chuy?n trái ph?i) và theo chi?u y (nh?y)
        //Tác ??ng l?c theo lu?t Newton, do ?ó l?c s? gi?m d?n và nhân v?t có th? b? tr??t
        //T?ng kh?i l??ng (mass), t?ng linear drag (l?c c?n theo ph??ng ngang) ?? gi?m ?? tr??t
        if (useForce)
            rigid.AddForce(new Vector2(currentMove, jumpMove));

        //Sau khi x? lý v?t lý xong, gán l?i t?c ?? nh?y b?ng 0 (?? cho r?t xu?ng theo tr?ng l?c)
        jumpMove = 0;
    }

        //Ph??ng th?c s? ki?n khi 2 collider b?t ??u ??ng nhau (không có obj nào có isTrigger)
        /* private void OnCollisionEnter2D(Collision2D collision)
         {
             //Ki?m tra collider còn l?i thông qua tag ho?c name
             if (collision.transform.tag.Equals("Ground"))
                 Debug.Log("Enter collider");
         }

         //Ph??ng th?c s? ki?n khi 2 collider b?t ??u tách ra nhau (không có obj nào có isTrigger)
         private void OnCollisionExit2D(Collision2D collision)
         {
             Debug.Log("Exit collider");
         }

         //Ph??ng th?c s? ki?n khi 1 collider ??ng v?i 1 collider khác có isTrigger (dùng ?? kích ho?t b?y tàng hình ch?ng h?n)
         private void OnTriggerEnter2D(Collider2D collision)
         {
             Debug.Log("Enter trigger");
         }*/
    }