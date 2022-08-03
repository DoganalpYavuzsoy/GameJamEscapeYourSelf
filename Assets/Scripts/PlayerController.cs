using Destruction.Standard;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 100;
    public CharacterController controller;
    private float speed = 5f;
    public float gravity = -9.81f;
    Vector3 velocity;
    public Animator animator;
    public GameObject player;
    public GameObject mainPlayerModel;
    public float jumpSpeed = 8.0f;
    public Transform playerCameraParent;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    public GameObject healtbarparent;
    public GameObject Parent;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;

    [HideInInspector]
    public bool canMove = true;

    private Vector3 current_pos;
    private Vector3 last_pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /* public void TriggerDestruction(Vector3 triggerPosition, float magnitude)
     {

     }
    */


    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Trigger>().TriggerDestruction(new Vector3(1.0f, 20.0f), 3.0f);
        //Debug.Log();
        collision.gameObject.AddComponent<TrinangleExplosion>();

        StartCoroutine(collision.gameObject.GetComponent<TrinangleExplosion>().SplitMesh(true));

        Debug.Log("On Collision Enter: " + collision.collider.name);
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("On Collision stay: " + collision.collider.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        //collision.gameObject.GetComponent<Trigger>().TriggerDestruction(new Vector3(1.0f, 20.0f), 3.0f);

        Debug.Log("On Collision exit: " + collision.collider.name);
    }
    bool isAttacking = false;
    // Update is called once per frame
    void Update()
    {
   
    Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        if (health <= 0)
        {
            //Destroy(transform.gameObject);
            animator.SetBool("isDead", true);
            animator.Play("Death");
        }
        else
        {
            try
            {
                GameObject bar = GameObject.Find("Bar");
                bar.transform.localScale = new Vector3((health) / 100, 1);
            }
            catch(Exception e)
            {

            }
           
            //transform.Find("Bar").localScale = new Vector3(health, 1);
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            bool isAttack = Input.GetKey(KeyCode.Mouse0);
            bool isShiftKeyDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            bool jump = Input.GetKey(KeyCode.Space);


            if (!isShiftAttack)
            {
                Vector3 move = playerCameraParent.transform.right * x + playerCameraParent.transform.forward * z;
                controller.Move(move * speed * Time.deltaTime);
                velocity.y += gravity * Time.deltaTime;
                // Applying Jump
                if (Input.GetButtonDown("Jump") && controller.isGrounded)
                {

                    velocity.y = 5;

                }

                // Applying Gravity
                if (controller.isGrounded == false)
                {

                   // velocity.y += Physics.gravity.y * gravity;

                }


                controller.Move(velocity * Time.deltaTime);
            }
          



            // this is the important part
            current_pos = controller.transform.position;
            float speed2 = (current_pos - last_pos).magnitude / Time.deltaTime;
            //print(speed2);
            last_pos = current_pos;
            animator.SetFloat("speed", (speed2 * Time.deltaTime) * 10);
            playerCameraParent.position = player.transform.position + new Vector3(0, 0.83f, 0);


            if (speed2 > 0.2)
            {
                Transform test24 = playerCameraParent.transform; //  CinemachineCameraTarget.transform;
                Vector3 look = test24.TransformDirection(Vector3.forward);
                Debug.DrawRay(test24.position, look, Color.green, 14);
                Quaternion newPlayerTrans = mainPlayerModel.transform.rotation;
                /*Debug.Log(newPlayerTrans.y);
                Debug.Log(playerCameraParent.transform.rotation.eulerAngles.y);*/
                newPlayerTrans.y = playerCameraParent.transform.rotation.y;
                mainPlayerModel.transform.rotation = Quaternion.Euler(newPlayerTrans.x, playerCameraParent.transform.rotation.eulerAngles.y, newPlayerTrans.z);
            }



            if (true)
            {
                rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
                rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
                playerCameraParent.localRotation = Quaternion.Euler(rotation.x, rotation.y, 0);
                //transform.eulerAngles = new Vector2(0, rotation.y);
            }
            if (isAttack && isShiftKeyDown)
            {
                isShiftAttack = true;
                Debug.Log("Test Shift Attack");
                animator.Play("ShiftAttack");
                /*Vector3 move2 =  transform.forward * 4;
                controller.Move(move2 * speed * Time.deltaTime);*/
                Invoke("resetAttack", 1.5f);//this will happen after 2 seconds
            }
            else if (isAttack && !isAttacking)
            {
                isAttacking = true;
                animator.Play("Attack");

                //Debug.Log(GameObject.Find("Enemy").transform.childCount);
                for (int i = 0; i < GameObject.Find("Enemy").transform.childCount; i++)
                {
                    Transform Children = GameObject.Find("Enemy").transform.GetChild(i);
                    float distance = Vector3.Distance(controller.transform.position, Children.transform.position);

                    Debug.Log(distance);
                    if (distance < 1.9f)
                    {
                        Debug.Log("düşmana vurdun");
                        Children.GetComponent<EnemyController>().takeDamage(50);
                    }
                    else
                    {
                        // anim.SetBool("attack", false);

                    }

                }
                Invoke("resetAttack", 1.5f);//this will happen after 2 seconds


                /*Vector3 move2 =  transform.forward * 4;
                controller.Move(move2 * speed * Time.deltaTime);*/
            }

            if (jump)
            {
                animator.Play("Jump");
            }
            //healtbarparent.transform.position = mainPlayerModel.transform.position + new Vector3(0, 0.83f,0);

            //healtbarparent.transform.position = mainPlayerModel.transform.position*3;
            //Instantiate(healtbarparent, new Vector3(0,0, 0), Quaternion.identity);
        }

    }
    public void takeDamage(float damage)
    {
        //Debug.Log(damage + "hasar alındı");
        health -= damage;
    }

    bool isShiftAttack = false;
    public void resetAttack()
    {
        isAttacking = false;
        isShiftAttack = false;
    }


}