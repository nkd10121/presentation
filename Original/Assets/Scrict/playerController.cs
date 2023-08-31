using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    private float jumpForce = 390.0f;   //ジャンプ力
    private float walkForce = 10.0f;    //歩く速さ
    private float maxWalkSpeed = 3.0f;  //歩く速さの制限
    private bool isRightFlag = false;
    private bool isLeftFlag = false;
    
    private bool isButtonDownFlag = false;    //ボタンが押されているか

    private float buttonDownTimeSpan = 1.0f;    //1秒間を計測する用
    private float currentTime = 0f;
    private float whileButtonDownTime = 0;    //何回1秒間を計測したか

    public AudioClip sound1;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();

        
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        //ボタンを押した瞬間
        if (Input.GetMouseButtonDown(0) && this.rigid2D.velocity.y == 0)
        {
            isButtonDownFlag = true;
        }

        //ボタンを離した瞬間
        if (Input.GetMouseButtonUp(0) && this.rigid2D.velocity.y == 0)
        {
            if(isButtonDownFlag)
            {          
                //押した時間が4秒以上の時はランダムなジャンプ力に
                //4秒以内の時は押した時間に応じてジャンプ力が変わる
                if (whileButtonDownTime > 5)
                {
                    float rnd = Random.Range(1, 5);
                    float mag1 = jumpForce + 50 * rnd;
                    this.rigid2D.AddForce(transform.up * mag1);
                    audioSource.PlayOneShot(sound1);
                }
                else
                {
                    float mag2 = jumpForce + 50 * whileButtonDownTime;
                    this.rigid2D.AddForce(transform.up * mag2);
                    audioSource.PlayOneShot(sound1);
                }
            }
            isButtonDownFlag = false;
            whileButtonDownTime = 0;
        }

        if (currentTime > buttonDownTimeSpan && isButtonDownFlag)
        {
            whileButtonDownTime++;
            Debug.Log(whileButtonDownTime);
            currentTime = 0f;
        }

        //左入力
        if(this.rigid2D.velocity.y == 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                isLeftFlag = true;
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                isLeftFlag = false;
            }
            //右 入力
            if (Input.GetKey(KeyCode.D))
            {
                isRightFlag = true;
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                isRightFlag = false;
            }

            //左右両方押したとき移動しない
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                rigid2D.velocity = Vector2.zero;
            }

            //移動しないときとジャンプボタンを押している間は移動しない
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || isButtonDownFlag)
            {
                rigid2D.velocity = Vector2.zero;
            }
        }
        
    }

    void FixedUpdate()
    {
        Vector2 posi = this.transform.position;

        //左右移動
        int key = 0;
        if (!isButtonDownFlag)
        {
            //左
            if (isLeftFlag)
            {
                if (this.rigid2D.position.x < -10)
                {
                    posi.x = -10;
                    transform.position = posi;
                }
                key -= 1;

            }

            //右
            if (isRightFlag)
            {
                if (this.rigid2D.position.x > 10)
                {
                    posi.x = 10;
                    transform.position = posi;
                }
                key = 1;

            }
        }
        //プレイヤーの速度
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        //スピード制限
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        //プレイヤの速度に応じてアニメーション速度を変える
        if (this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 1.5f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene("kuriaScene");
    }
}
