using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //�錾
    public AudioClip sound;
    AudioSource audioSource;
    bool isCheck1 = false;
    bool isCheck2 = false;

    public float jumpPower = 240f;
    public float speed;          //����
    private const float SPEED_MAX = 7.0f;   //�����̌��E�l
    public float rotationSpeed;
    Transform beforeParent;

    public Vector3 startPos;

    private Rigidbody rb;
    private bool isGrounded;
    private int PrecedeJump_;   //�W�����v�̐�s����

    void Start()
    {
        beforeParent = transform.parent;
        startPos = this.transform.position;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //�J�����̌����ɍ��킹�ăv���C���[����]������
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        transform.rotation = Quaternion.LookRotation(cameraForward);

        //�H�ɃR���W�������������@�\���Ȃ����Ƃ�����̂Ń��C�L���X�g�ōĐR
        CheckGrounded();

        //�W�����v�̐�s����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrecedeJump_ = 50;
        }

        if (isGrounded)
        {
            //�ړ�
            //���ړ��̍ō����x�ȉ��Ȃ�Α�����󂯕t����
            if (rb.velocity.magnitude < SPEED_MAX)
            {
                // W�L�[�i�O���ړ��j
                if (Input.GetKey(KeyCode.W))
                {
                    rb.AddForce(transform.TransformDirection(new Vector3(0, 0, speed)));
                }

                // S�L�[�i����ړ��j
                if (Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(transform.TransformDirection(new Vector3(0, 0, -speed)));
                }

                // D�L�[�i�E�ړ��j
                if (Input.GetKey(KeyCode.D))
                {
                    rb.AddForce(transform.TransformDirection(new Vector3(speed, 0, 0)));
                }

                // A�L�[�i���ړ��j
                if (Input.GetKey(KeyCode.A))
                {
                    rb.AddForce(transform.TransformDirection(new Vector3(-speed, 0, 0)));
                }
            }
             
            //��s���͒l��1�ȏ�ŃW�����v
            if (PrecedeJump_ > 0)
            {
                isGrounded = false;
                Vector3 vec = rb.velocity;
                vec.y = jumpPower;
                rb.velocity = new Vector3(vec.x, 0, vec.z);
                rb.AddForce(vec);
                PrecedeJump_ = 0;
            }
        }
        
        //��]
        Rotation();

        //��������
        if(transform.position.y < -15.0f)
        {
            Debug.Log("�������I");
         //�V�[���ς���
            transform.position = startPos;
            //������͂�0�ɂ���
            rb.velocity = Vector3.zero;
            //�e��������
            this.gameObject.transform.parent = beforeParent;
        }

        PrecedeJump_--;
    }

    //��]
    void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, mouseX * rotationSpeed, Space.World);

    }

    void CheckGrounded()
    {
        //����true�Ȃ炻�̂܂ܕԂ�
        if (isGrounded)
            return;
        
        //�������̏����ʒu�Ǝp��
        var ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        //�����̋���(����J�v�Z���I�u�W�F�N�g�ɐݒ肷��̂�Height/2 + 0.1�ȏ��ݒ�)
        var distance = 0.7f;
        //Raycast��hit���邩�ǂ����Ŕ��背�C���[���w�肷�邱�Ƃ��\
        if(Physics.Raycast(ray, distance))
            isGrounded = true;
    }

    //�R���C�_�[�ƐG�ꂽ�u�Ԃ�
    void OnCollisionStay(Collision collision)
    {
        //�������݂̂ɐڒn�����t�^
        if(Vector3.Dot((collision.contacts[0].point - transform.position).normalized, Vector3.down) > Mathf.Cos(1))
        {
            isGrounded = true;
        }
        
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("MoveFloor"))
        {
            Debug.Log("aaaaaaa");
            //�e�ɐݒ�
            this.gameObject.transform.parent = collision.gameObject.transform;
        }
        else if(collision.gameObject.CompareTag("Goal"))
        {
            //�e�ɐݒ�
            this.gameObject.transform.parent = beforeParent;
            SceneManager.LoadScene("Result");
        }
        else
        {
            //�e�ɐݒ�
            this.gameObject.transform.parent = beforeParent;
        }

        if (collision.gameObject.CompareTag("CheckPointFloor1"))
        {
            Debug.Log("1");
            startPos = new Vector3(-8, -3, 32);
            if (isCheck1 == false)
            { 
                isCheck1 = true;
                audioSource.PlayOneShot(sound);
            }
        }
        if (collision.gameObject.CompareTag("CheckPointFloor2"))
        {
            Debug.Log("2");
            startPos = new Vector3(20, -3, 70);
            if (isCheck2 == false)
            {
                isCheck2 = true;
                audioSource.PlayOneShot(sound);
            }
        }
    }

    private void FixedUpdate()
    {
        //�ڒn����������I��false�ɂ���
        isGrounded = false;
    }
}

