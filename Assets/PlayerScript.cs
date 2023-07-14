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

    public float jumpPower = 240f;
    public float speed;          //����
    private const float SPEED_MAX = 7.0f;   //�����̌��E�l
    public float rotationSpeed;
    Transform beforeParent;

    public Vector3 startPos;

    private Rigidbody rb;
    private bool isGrounded; 

    void Start()
    {
        beforeParent = transform.parent;
        startPos = this.transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //�J�����̌����ɍ��킹�ăv���C���[����]������
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        transform.rotation = Quaternion.LookRotation(cameraForward);

        if (isGrounded)
        {
            //�ړ�
            // W�L�[�i�O���ړ��j
            if (Input.GetKey(KeyCode.W))
            {
                if (rb.velocity.magnitude < SPEED_MAX)
                    rb.AddForce(transform.TransformDirection(new Vector3(0, 0, speed)));
            }

            // S�L�[�i����ړ��j
            if (Input.GetKey(KeyCode.S))
            {
                if (rb.velocity.magnitude < SPEED_MAX)
                    rb.AddForce(transform.TransformDirection(new Vector3(0, 0, -speed)));
            }

            // D�L�[�i�E�ړ��j
            if (Input.GetKey(KeyCode.D))
            {
                if (rb.velocity.magnitude < SPEED_MAX)
                    rb.AddForce(transform.TransformDirection(new Vector3(speed, 0, 0)));
            }

            // A�L�[�i���ړ��j
            if (Input.GetKey(KeyCode.A))
            {
                if (rb.velocity.magnitude < SPEED_MAX)
                    rb.AddForce(transform.TransformDirection(new Vector3(-speed, 0, 0)));
            }
            //�X�y�[�X�L�[���^�b�v�����ۂ̓���
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                rb.AddForce(Vector3.up * jumpPower);
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
    }

    //��]
    void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, mouseX * rotationSpeed, Space.World);

    }

    //�R���C�_�[�ƐG�ꂽ�u�Ԃ�
    void OnCollisionStay(Collision collision)
    {
        //�������݂̂ɐڒn�����t�^
        if(Vector3.Dot((collision.contacts[0].point - transform.position).normalized, Vector3.down) > Mathf.Cos(0.5f))
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
            SceneManager.LoadScene("GoalScene");
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
        }
        if (collision.gameObject.CompareTag("CheckPointFloor2"))
        {
            Debug.Log("2");
            startPos = new Vector3(20, -3, 70);
        }
    }

    private void FixedUpdate()
    {
        //�ڒn����������I��false�ɂ���
        isGrounded = false;
    }
}

