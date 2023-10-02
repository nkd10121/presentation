using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    private float jumpForce = 390.0f;   //�W�����v��
    private float walkForce = 10.0f;    //��������
    private float maxWalkSpeed = 3.0f;  //���������̐���
    private bool isRightFlag = false;
    private bool isLeftFlag = false;
    
    private bool isButtonDownFlag = false;    //�{�^����������Ă��邩

    private float buttonDownTimeSpan = 1.0f;    //1�b�Ԃ��v������p
    private float currentTime = 0f;
    private float whileButtonDownTime = 0;    //����1�b�Ԃ��v��������

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

        //�{�^�����������u��
        if (Input.GetMouseButtonDown(0) && this.rigid2D.velocity.y == 0)
        {
            isButtonDownFlag = true;
        }

        //�{�^���𗣂����u��
        if (Input.GetMouseButtonUp(0) && this.rigid2D.velocity.y == 0)
        {
            if(isButtonDownFlag)
            {          
                //���������Ԃ�4�b�ȏ�̎��̓����_���ȃW�����v�͂�
                //4�b�ȓ��̎��͉��������Ԃɉ����ăW�����v�͂��ς��
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

        //������
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
            //�E ����
            if (Input.GetKey(KeyCode.D))
            {
                isRightFlag = true;
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                isRightFlag = false;
            }

            //���E�����������Ƃ��ړ����Ȃ�
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                rigid2D.velocity = Vector2.zero;
            }

            //�ړ����Ȃ��Ƃ��ƃW�����v�{�^���������Ă���Ԃ͈ړ����Ȃ�
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || isButtonDownFlag)
            {
                rigid2D.velocity = Vector2.zero;
            }
        }
        
    }

    void FixedUpdate()
    {
        Vector2 posi = this.transform.position;

        //���E�ړ�
        int key = 0;
        if (!isButtonDownFlag)
        {
            //��
            if (isLeftFlag)
            {
                if (this.rigid2D.position.x < -10)
                {
                    posi.x = -10;
                    transform.position = posi;
                }
                key -= 1;

            }

            //�E
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
        //�v���C���[�̑��x
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        //�X�s�[�h����
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        //�v���C���̑��x�ɉ����ăA�j���[�V�������x��ς���
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
