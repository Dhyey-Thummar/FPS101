using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    
    public CharacterController controller;

    public float speed = 12f;

    Vector3 velocity;

    public float gravity =  9.81f;

    public Transform groundcheck;
    public float grounddistance = 0.3f;
    public LayerMask groundmask;
    public float jumpHeight = 3f;
    bool isGrounded;


    // Update is called once per frame
    void Update()

    {
        isGrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        } 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move*speed*Time.deltaTime);

        if(Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 1f * gravity);
        }
        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
