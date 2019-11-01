using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using moe.heing.CodeConfig;

public class Player : MonoBehaviour
{
    

    public Transform playerTransform;

    public int playerid;
    public int horDirection = 0;
    public string playerName;
    public Transform bottomTransformForStand;
    public int yForce = 350;

    [Header("侦测地板的射线起点")]
    private Transform groundCheck;
    private Transform groundCheck2;

    public bool isgroud;
    [Header("地面图册")]
    public LayerMask groundLayer;
    [Header("感应地板的距离")]
    [Range(0, 5)]
    public float distance;

    private const int m_times = 6;
    private Rigidbody2D myRigidbody2D;

    // 水平移动
    public void Move(int _direction) {
        horDirection = _direction;
    }

    public void Jump()
    {
        myRigidbody2D.velocity = Vector2.zero;
        myRigidbody2D.AddForce(Vector2.up * yForce);
    }

    private void IsGround()
    {
        bool grounded;
        bool grounded2;

        Vector2 start = groundCheck.position;
        Vector2 end = new Vector2(start.x, start.y - distance);

        grounded = Physics2D.Linecast(start, end, groundLayer);

        start = groundCheck2.position;
        end = new Vector2(start.x, start.y - distance);

        grounded2 = Physics2D.Linecast(start, end, groundLayer);

        if (grounded || grounded2)
        {
            isgroud = true;
        }
        else
        {
            isgroud = false;
        }
    }
    void Awake() {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        groundCheck = transform.Find("detect_ground_1");
        groundCheck2 = transform.Find("detect_ground_2");
    }

    // Update is called once per frame
    void Update()
    {
        IsGround();

        if (horDirection != 0) {
            transform.Translate(horDirection * m_times * Time.deltaTime, 0, 0);  
        }

        myRigidbody2D.centerOfMass = bottomTransformForStand.localPosition;
    }
}
