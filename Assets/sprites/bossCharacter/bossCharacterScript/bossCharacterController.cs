using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossCharacterController : MonoBehaviour
{
    Rigidbody2D rigid;
    public float moveSpeed = 40f;
    float currentMove;
    bool isMove = false;

    public bool useForce = false;

    public float jumpSpeed = 10f;
    float jumpMove;

    SpriteRenderer Srenderer;

    Animator anim;


    //attack của main character
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    //Lấy game object về đạn có sẵn trong prefab để điều khiển việc bắn đạn
    //public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        //Khởi tạo lấy các thành phần trước
        rigid = GetComponent<Rigidbody2D>();

        //Gán giá trị về tốc độ đi và nhảy ban đầu là 0
        currentMove = 0;
        jumpMove = 0;

        Srenderer = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
    }

    // Lưu ý: xử lý input thì ở update, xử lý vật lý ở fixed update
    void Update()
    {
        //Trường hợp bắn đạn, khi nhấn phím A trên bàn phím sẽ bắn viên đạn với vị trí tọa độ:
        //vị trí tọa độ x hiện tại + 1, y và z giữ nguyên, góc quay giữ nguyên, xài theo hệ Quaternion thay vì Euler
        //if (Input.GetKeyDown(KeyCode.A))
        //    Instantiate(bullet, new Vector3(transform.position.x + 1f,
        //        transform.position.y, transform.position.z), Quaternion.identity);

        //Gán tốc độ nhảy chứ ko xử trực tiếp ở update, xử lý vật lý ở fixed update
        if (Input.GetKey("w"))
        {
            jumpMove = jumpSpeed;
        }

        //Gán tốc độ đi chứ ko xử trực tiếp ở update, xử lý vật lý ở fixed update
        if (Input.GetKey("a"))
        {
            //Đi trái thì hướng phải ngược lại, do đó có giá trị âm
            currentMove = -moveSpeed;

            //Xoay hình sang bên trái
            Srenderer.flipX = false;

            //Gọi animation walking chạy 1 lần
            anim.Play("moving");
        }
        else
        {
            if (Input.GetKey("d"))
            {
                //Đi phải thì hướng cùng chiều, do đó có giá trị dương
                currentMove = moveSpeed;

                //Xoay hình sang bên trái
                Srenderer.flipX = true;

                //Gọi animation walking chạy 1 lần
                anim.Play("moving");
            }
            else
            {
                //Nếu không di chuyển (không nhấn nút nào), gán giá trị tốc độ = 0 (đứng yên)
                currentMove = 0;
            }
        }

        /*if (Input.GetKey("c"))
        {
            //Gọi animation walking chạy 1 lần

            Attack();
        }*/
        if (Input.GetKey("j"))
        {
            //Gọi animation walking chạy 1 lần
            anim.Play("attack_1");
        }

    }

    /* void Attack()
     {
         anim.Play("attacking_2");
         Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

         foreach (Collider2D enemy in hitEnemies)
         {
             Debug.Log("we hit" + enemy.name);
             enemy.GetComponent<enemyScript>().TakeDamage(1);
         }
     }*/

    /*void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }*/

    //Xử lý vật lý ở fixed update
    private void FixedUpdate()
    {
        //Làm đóng băng góc xoay để tránh bị tác động của lực vật lý làm nhân vật lăn lộn
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

        //2 cách để di chuyển nhận vật: 
        // Cách 1: thay đổi vận tốc đều, giá trị vận tốc đã cập nhật trên Update()
        if (!useForce)
            rigid.velocity = new Vector2(currentMove, rigid.velocity.y + jumpMove);

        //Cách 2: Tác động 1 lực theo chiều x (di chuyển trái phải) và theo chiều y (nhảy)
        //Tác động lực theo luật Newton, do đó lực sẽ giảm dần và nhân vật có thể bị trượt
        //Tăng khổi lượng (mass), tăng linear drag (lực cản theo phương ngang) để giảm độ trượt
        if (useForce)
            rigid.AddForce(new Vector2(currentMove, jumpMove));

        //Sau khi xử lý vật lý xong, gán lại tốc độ nhảy bằng 0 (để cho rớt xuống theo trọng lực)
        jumpMove = 0;
    }

    //Phương thức sự kiện khi 2 collider bắt đầu đụng nhau (không có obj nào có isTrigger)
    /* private void OnCollisionEnter2D(Collision2D collision)
     {
         //Kiểm tra collider còn lại thông qua tag hoặc name
         if (collision.transform.tag.Equals("Ground"))
             Debug.Log("Enter collider");
     }

     //Phương thức sự kiện khi 2 collider bắt đầu tách ra nhau (không có obj nào có isTrigger)
     private void OnCollisionExit2D(Collision2D collision)
     {
         Debug.Log("Exit collider");
     }

     //Phương thức sự kiện khi 1 collider đụng với 1 collider khác có isTrigger (dùng để kích hoạt bẫy tàng hình chẳng hạn)
     private void OnTriggerEnter2D(Collider2D collision)
     {
         Debug.Log("Enter trigger");
     }*/
}
