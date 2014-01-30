using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Trap : MonoBehaviour
{
    private Transform player;
    private Health playerHealth;

    private int damage = 10;
    private float range = 3.0f;
    private float attackCheck = 0.0f;
    private float interval = 10.0f;
    private float position;
    private float viewDistance = 5.0f;

    private int playerLayer;
    private int obstacleLayer;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<Health>();

        playerLayer = 1 << LayerMask.NameToLayer("Player");
        obstacleLayer = 11 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Obstacle");
    }

    // Update is called once per frame
    void Update()
    {
        AttackCheck();
    }

    void FixedUpdate()
    {
    }

    void AttackCheck()
    {
        //if (attackCheck < interval)
        //    attackCheck += Time.deltaTime;
        //else
        //{
        //    attackCheck = 0;
        //    float distance = Vector3.Distance(transform.position, player.position);

        //    if (distance <= range)
        //        playerHealth.TakeDamage(damage);
        //}

        //attackCheck = 0;
        //float distance = Vector3.Distance(transform.position, player.position);

        //if(position == distance)
        //    playerHealth.TakeDamage(damage);

        if (attackCheck < interval)
            attackCheck += Time.deltaTime;
        else
        {
            attackCheck = 0;
            float distance = Vector3.Distance(transform.position, player.position);

            if (position == distance)
                playerHealth.TakeDamage(damage);
        }
    }

    void HitCheck()
    {
        if (Physics.CheckSphere(transform.position, viewDistance, playerLayer))
        {
            Debug.Log("You are near the trap!");

            RaycastHit hitInfo;

            if (Physics.Raycast(transform.position, player.position - transform.position,
                out hitInfo, viewDistance, obstacleLayer))
            {
                Debug.Log("hit: " + hitInfo.transform.name, this);

                if (hitInfo.collider.gameObject.tag == "Player")
                {
                    AttackCheck();
                }
            }
        }
    }
}