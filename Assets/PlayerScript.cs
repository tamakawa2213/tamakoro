using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //宣言

    public float jumpPower = 240f;
    public float speed;          //速さ
    private const float SPEED_MAX = 7.0f;   //速さの限界値
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
        //カメラの向きに合わせてプレイヤーを回転させる
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        transform.rotation = Quaternion.LookRotation(cameraForward);

        if (isGrounded)
        {
            //移動
            // Wキー（前方移動）
            if (Input.GetKey(KeyCode.W))
            {
                if (rb.velocity.magnitude < SPEED_MAX)
                    rb.AddForce(transform.TransformDirection(new Vector3(0, 0, speed)));
            }

            // Sキー（後方移動）
            if (Input.GetKey(KeyCode.S))
            {
                if (rb.velocity.magnitude < SPEED_MAX)
                    rb.AddForce(transform.TransformDirection(new Vector3(0, 0, -speed)));
            }

            // Dキー（右移動）
            if (Input.GetKey(KeyCode.D))
            {
                if (rb.velocity.magnitude < SPEED_MAX)
                    rb.AddForce(transform.TransformDirection(new Vector3(speed, 0, 0)));
            }

            // Aキー（左移動）
            if (Input.GetKey(KeyCode.A))
            {
                if (rb.velocity.magnitude < SPEED_MAX)
                    rb.AddForce(transform.TransformDirection(new Vector3(-speed, 0, 0)));
            }
            //スペースキーをタップした際の動作
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                rb.AddForce(Vector3.up * jumpPower);
            }
        }
        
        //回転
        Rotation();

        //落ちた時
        if(transform.position.y < -15.0f)
        {
            Debug.Log("おちた！");
         //シーン変える
            transform.position = startPos;
            //加える力を0にする
            rb.velocity = Vector3.zero;
            //親を初期化
            this.gameObject.transform.parent = beforeParent;
        }
    }

    //回転
    void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, mouseX * rotationSpeed, Space.World);

    }

    //コライダーと触れた瞬間に
    void OnCollisionStay(Collision collision)
    {
        //下方向のみに接地判定を付与
        if(Vector3.Dot((collision.contacts[0].point - transform.position).normalized, Vector3.down) > Mathf.Cos(0.5f))
        {
            isGrounded = true;
        }
        
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("MoveFloor"))
        {
            Debug.Log("aaaaaaa");
            //親に設定
            this.gameObject.transform.parent = collision.gameObject.transform;
        }
        else if(collision.gameObject.CompareTag("Goal"))
        {
            //親に設定
            this.gameObject.transform.parent = beforeParent;
            SceneManager.LoadScene("GoalScene");
        }
        else
        {
            //親に設定
            this.gameObject.transform.parent = beforeParent;
        }
        
    }

    private void FixedUpdate()
    {
        //接地判定を強制的にfalseにする
        isGrounded = false;
    }
}

