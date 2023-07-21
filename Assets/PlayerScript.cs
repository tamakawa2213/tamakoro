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
    public AudioClip sound;
    AudioSource audioSource;
    bool isCheck1 = false;
    bool isCheck2 = false;

    public float jumpPower = 240f;
    public float speed;          //速さ
    private const float SPEED_MAX = 7.0f;   //速さの限界値
    public float rotationSpeed;
    Transform beforeParent;

    public Vector3 startPos;

    private Rigidbody rb;
    private bool isGrounded;
    private int PrecedeJump_;   //ジャンプの先行入力

    void Start()
    {
        beforeParent = transform.parent;
        startPos = this.transform.position;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //カメラの向きに合わせてプレイヤーを回転させる
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        transform.rotation = Quaternion.LookRotation(cameraForward);

        //稀にコリジョンが正しく機能しないことがあるのでレイキャストで再審
        CheckGrounded();

        //ジャンプの先行入力
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrecedeJump_ = 50;
        }

        if (isGrounded)
        {
            //移動
            //横移動の最高速度以下ならば操作を受け付ける
            if (rb.velocity.magnitude < SPEED_MAX)
            {
                // Wキー（前方移動）
                if (Input.GetKey(KeyCode.W))
                {
                    rb.AddForce(transform.TransformDirection(new Vector3(0, 0, speed)));
                }

                // Sキー（後方移動）
                if (Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(transform.TransformDirection(new Vector3(0, 0, -speed)));
                }

                // Dキー（右移動）
                if (Input.GetKey(KeyCode.D))
                {
                    rb.AddForce(transform.TransformDirection(new Vector3(speed, 0, 0)));
                }

                // Aキー（左移動）
                if (Input.GetKey(KeyCode.A))
                {
                    rb.AddForce(transform.TransformDirection(new Vector3(-speed, 0, 0)));
                }
            }
             
            //先行入力値が1以上でジャンプ
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

        PrecedeJump_--;
    }

    //回転
    void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, mouseX * rotationSpeed, Space.World);

    }

    void CheckGrounded()
    {
        //既にtrueならそのまま返す
        if (isGrounded)
            return;
        
        //放つ光線の初期位置と姿勢
        var ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        //光線の距離(今回カプセルオブジェクトに設定するのでHeight/2 + 0.1以上を設定)
        var distance = 0.7f;
        //Raycastがhitするかどうかで判定レイヤーを指定することも可能
        if(Physics.Raycast(ray, distance))
            isGrounded = true;
    }

    //コライダーと触れた瞬間に
    void OnCollisionStay(Collision collision)
    {
        //下方向のみに接地判定を付与
        if(Vector3.Dot((collision.contacts[0].point - transform.position).normalized, Vector3.down) > Mathf.Cos(1))
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
            SceneManager.LoadScene("Result");
        }
        else
        {
            //親に設定
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
        //接地判定を強制的にfalseにする
        isGrounded = false;
    }
}

