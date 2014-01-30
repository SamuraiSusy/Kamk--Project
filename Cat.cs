using UnityEngine;
using System.Collections;

enum EnemyState
{
    chase
};

[RequireComponent(typeof(Rigidbody))]
public class Cat : MonoBehaviour
{
    private Vector3 destination;
    private float distanceToReachDestination = 1.0f;
    private float movementSpeed = 2.0f;

    private Transform player;
    private Health playerHealth;

    EnemyState currentState;

    private int damage = 2;
    private float range = 3.0f;
    private float attackCheck = 0.0f;
    private float interval = 0.5f;
    private float viewDistance = 5.0f;
    private float chaseCheck = 0.0f;


    private int playerLayer;
    private int obstacleLayer;

	// Use this for initialization
	void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<Health>();

        playerLayer = 1 << LayerMask.NameToLayer("Player");
        obstacleLayer = 11 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Obstacle");

        currentState = EnemyState.chase;
	}
	
	// Update is called once per frame
	void Update()
    {
	    switch(currentState)
        {
            case EnemyState.chase:
                destination = player.position;
                AttackCheck();
                break;

                // patrol

            default:
                Debug.Log("Heello there");
                break;
        }

        ChaseCheck();
	}

    void FixedUpdate()
    {
        Move();
    }

    void AttackCheck()
    {
        if(attackCheck < interval)
            attackCheck += Time.deltaTime;
        else
        {
            attackCheck = 0;
            float distance = Vector3.Distance(transform.position, player.position);

            if(distance <= range)
                playerHealth.TakeDamage(damage);
        }
    }

    void ChaseCheck()
    {
        if(chaseCheck < interval)
            chaseCheck += Time.deltaTime;
        else if(chaseCheck >= interval)
        {
            chaseCheck = 0.0f;

            if(Physics.CheckSphere(transform.position, viewDistance, playerLayer))
            {
                Debug.Log("Cat's near");

                RaycastHit hitInfo;

                if(Physics.Raycast(transform.position, player.position - transform.position,
                    out hitInfo, viewDistance, obstacleLayer))
                {
                    Debug.Log("hit: " + hitInfo.transform.name, this);

                    if(hitInfo.collider.gameObject.tag == "Player")
                    {
                        attackCheck = 0;
                        currentState = EnemyState.chase;
                    }
                }
            }
            else
            {
                //patrol
            }
        }
    }

    void Move()
    {
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
        Vector3 positionWithoutY = new Vector3(transform.position.x, 0, transform.position.z);

        if(Vector3.Distance(positionWithoutY, destination) > distanceToReachDestination)
        {
            Vector3 lookPos = destination - transform.position;
            lookPos.y = 0;

            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = rotation;

            rigidbody.AddForce(transform.forward * movementSpeed, ForceMode.VelocityChange);
        }
        //else if current state = patrol
    }
}
