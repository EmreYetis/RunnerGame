using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float gravity = -20;

    private int desiredLane = 1;
    private int laneDistance = 3;

    private Vector3 startPos = new Vector3(0, 0.6f, -40f);
    private Vector3 direction;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        transform.position = startPos;
    }

    void Update()
    {
        direction.z = speed;
       
        Movement();

        if (controller.isGrounded)
        {
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
            
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }
        
        

                                                 
    }
    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);                         
    }

    private void Movement()
    {
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
               
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        Vector3 targetPos = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPos += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPos += Vector3.right * laneDistance;
        }

        //transform.position = targetPos;
        //transform.position = Vector3.Lerp(transform.position, targetPos, 70*Time.fixedDeltaTime); Smootlaþtýran kod alttaki;
        if (transform.position == targetPos) return;
        Vector3 diff = targetPos - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }
        
        
    }
    private void Jump()
    {
        direction.y = jumpForce;              
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag=="Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
}
