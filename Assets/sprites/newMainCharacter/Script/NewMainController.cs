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
    //L?y game object v? ??n c� s?n trong prefab ?? ?i?u khi?n vi?c b?n ??n
    //public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        //Kh?i t?o l?y c�c th�nh ph?n tr??c
        rigid = GetComponent<Rigidbody2D>();

        //G�n gi� tr? v? t?c ?? ?i v� nh?y ban ??u l� 0
        currentMove = 0;
        jumpMove = 0;

        Srenderer = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {

    }
    // L?u �: x? l� input th� ? update, x? l� v?t l� ? fixed update
    public void Update()
    {
        //Tr??ng h?p b?n ??n, khi nh?n ph�m A tr�n b�n ph�m s? b?n vi�n ??n v?i v? tr� t?a ??:
        //v? tr� t?a ?? x hi?n t?i + 1, y v� z gi? nguy�n, g�c quay gi? nguy�n, x�i theo h? Quaternion thay v� Euler
        //if (Input.GetKeyDown(KeyCode.A))
        //    Instantiate(bullet, new Vector3(transform.position.x + 1f,
        //        transform.position.y, transform.position.z), Quaternion.identity);



        //G�n t?c ?? nh?y ch? ko x? tr?c ti?p ? update, x? l� v?t l� ? fixed update
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpMove = jumpSpeed;
        }

        //G�n t?c ?? ?i ch? ko x? tr?c ti?p ? update, x? l� v?t l� ? fixed update
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //?i tr�i th� h??ng ph?i ng??c l?i, do ?� c� gi� tr? �m
            currentMove = -moveSpeed;

            //Xoay h�nh sang b�n tr�i
            Srenderer.flipX = true;

            //G?i animation walking ch?y 1 l?n
            anim.Play("moving");
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //?i ph?i th� h??ng c�ng chi?u, do ?� c� gi� tr? d??ng
                currentMove = moveSpeed;

                //Xoay h�nh sang b�n tr�i
                Srenderer.flipX = false;

                //G?i animation walking ch?y 1 l?n
                anim.Play("moving");
            }
            else
            {
                //N?u kh�ng di chuy?n (kh�ng nh?n n�t n�o), g�n gi� tr? t?c ?? = 0 (??ng y�n)
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
    //X? l� v?t l� ? fixed update
    private void FixedUpdate()
    {
        //L�m ?�ng b?ng g�c xoay ?? tr�nh b? t�c ??ng c?a l?c v?t l� l�m nh�n v?t l?n l?n
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

        //2 c�ch ?? di chuy?n nh?n v?t: 
        // C�ch 1: thay ??i v?n t?c ??u, gi� tr? v?n t?c ?� c?p nh?t tr�n Update()
        if (!useForce)
            rigid.velocity = new Vector2(currentMove, rigid.velocity.y + jumpMove);

        //C�ch 2: T�c ??ng 1 l?c theo chi?u x (di chuy?n tr�i ph?i) v� theo chi?u y (nh?y)
        //T�c ??ng l?c theo lu?t Newton, do ?� l?c s? gi?m d?n v� nh�n v?t c� th? b? tr??t
        //T?ng kh?i l??ng (mass), t?ng linear drag (l?c c?n theo ph??ng ngang) ?? gi?m ?? tr??t
        if (useForce)
            rigid.AddForce(new Vector2(currentMove, jumpMove));

        //Sau khi x? l� v?t l� xong, g�n l?i t?c ?? nh?y b?ng 0 (?? cho r?t xu?ng theo tr?ng l?c)
        jumpMove = 0;
    }

        //Ph??ng th?c s? ki?n khi 2 collider b?t ??u ??ng nhau (kh�ng c� obj n�o c� isTrigger)
        /* private void OnCollisionEnter2D(Collision2D collision)
         {
             //Ki?m tra collider c�n l?i th�ng qua tag ho?c name
             if (collision.transform.tag.Equals("Ground"))
                 Debug.Log("Enter collider");
         }

         //Ph??ng th?c s? ki?n khi 2 collider b?t ??u t�ch ra nhau (kh�ng c� obj n�o c� isTrigger)
         private void OnCollisionExit2D(Collision2D collision)
         {
             Debug.Log("Exit collider");
         }

         //Ph??ng th?c s? ki?n khi 1 collider ??ng v?i 1 collider kh�c c� isTrigger (d�ng ?? k�ch ho?t b?y t�ng h�nh ch?ng h?n)
         private void OnTriggerEnter2D(Collider2D collision)
         {
             Debug.Log("Enter trigger");
         }*/
    }