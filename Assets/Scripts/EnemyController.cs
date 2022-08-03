using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Animator anim;
    private bool dead;
    public float totalHealt = 100;
    public float currentHealth = 100;
    public float expGranted;
    public float attackDamage;
    public float attackSpeed;
    public float movementSpeed;

    public Transform testTarget;
    public GameObject Enemy;
    public GameObject targetPlayerObject;
    private GameObject[] players;

    private Vector3 baseLocation;


    private bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        currentHealth = totalHealt;
        _agent = GetComponent<NavMeshAgent>();
        baseLocation = transform.position;
    }
    bool isDead = false;
    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0 && !isDead)
        {
            isDead = true;
            anim.Play("Dead");
            Invoke("DestroyEnemy",2f);//this will happen after 2 seconds

        }
        else if(currentHealth > 0)
        {

            float distance = Vector3.Distance(transform.position, testTarget.transform.position);

            //Debug.Log("Distance : " + distance);

            if (distance < 9.0f)
            {
                Vector3 dirToPlater = transform.position - testTarget.transform.position;
                Vector3 newPos = transform.position - dirToPlater;
                _agent.SetDestination(newPos);
            }
            else
            {
                _agent.SetDestination(baseLocation);
            }

            if (distance < 1.9f && isAttacking == false)
            {
                isAttacking = true;
                Invoke("resetAttacking", 3);//this will happen after 2 seconds

                anim.SetBool("attack", true);
                targetPlayerObject.GetComponent<PlayerController>().takeDamage(10);
            }
            else
            {
                anim.SetBool("attack", false);

            }

            /*var targetrotation = Quaternion.LookRotation(testTarget.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, Time.deltaTime * 15);
            Enemy.transform.LookAt(testTarget);
            Enemy.transform.position = Vector3.Lerp(
                Enemy.transform.position, testTarget.position, Time.deltaTime * 1);*/
            anim.SetFloat("speed", (_agent.velocity.magnitude * Time.deltaTime) * 100);
        }

    }
    public void DestroyEnemy()
    {
        Destroy(transform.gameObject);
    }
    public void takeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void resetAttacking()
    {
        isAttacking = false;
    }
}
