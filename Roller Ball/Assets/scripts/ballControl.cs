using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballControl : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    private bool isMoving;
    private Vector3 moveDirection;    
    private Vector3 collisionPoz;     //position where collide happend.

    private int swipePower = 500;     //for understand user swipe is real swipe or not.
    private Vector2 swipeLastPos;     //the position where ball is stand.
    private Vector2 swipeCurrentPos;  //the position where mouse clicked.
    private Vector2 currentSwipe;     //the position which we want to ball go.

    private Color rollingColor;






    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rollingColor = Random.ColorHSV();//create a random color
        GetComponent<MeshRenderer>().material.color = rollingColor;//set ball material color  to rolling color.
         
    }

    
    void FixedUpdate()
    {

        if(isMoving)
        {
            rb.velocity = speed * moveDirection;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position - (Vector3.up / 2), 0.2f); //store ball collides tile under position/2 and 0.5 radius
        int i = 0;
        while(i<hitColliders.Length)
        {
            GroundControl ground = hitColliders[i].transform.GetComponent<GroundControl>();
            if(ground && !ground.isÝtRolling)
            {
                ground.ChangeColor(rollingColor);
                Debug.Log("deðdi");
            }
            i++;

        }

        if(collisionPoz != Vector3.zero)
        {
            if (Vector3.Distance(transform.position,collisionPoz)<1)
            {
                isMoving = false;
                moveDirection = Vector3.zero;
                collisionPoz = Vector3.zero;
            }
        }

        if (isMoving)
            return;

        //swipe mechanics code start
        if(Input.GetMouseButton(0))
        {
            swipeCurrentPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //swipeLastPos = new Vector2(transform.position.x, transform.position.y);
            if(swipeLastPos != Vector2.zero)
            {
                currentSwipe = swipeCurrentPos - swipeLastPos;

                if(currentSwipe.sqrMagnitude<swipePower) //if your finger swipe is less than 5 mm(swipepower) dont do anythink because its not real swipe
                {
                    return;
                }

                currentSwipe.Normalize();

                // up/down
                if(currentSwipe.x>-0.5f && currentSwipe.x<0.5f)
                {
                    SetWayPoint(currentSwipe.y > 0 ? Vector3.forward : Vector3.back); //if currentswipe.y is bigger than 0 if it case go vector3.forward or vector3.back
                }

                // right/left
                if(currentSwipe.y>-0.5f && currentSwipe.y<0.5f)
                {
                    SetWayPoint(currentSwipe.x > 0 ? Vector3.right : Vector3.left);
                }

            }
            swipeLastPos = swipeCurrentPos;
          
        }  
        
        if(Input.GetMouseButtonUp(0)) //if you release your finger on screen or mouse thats what mousebuttonup do.
        {
            swipeLastPos = Vector2.zero;
            currentSwipe = Vector2.zero;
        }
        //swipe mechanics code done
    }
    
    //store the hitting point for understand ball hit wall and stop
    private void SetWayPoint(Vector3 goPoz)
    {
        moveDirection = goPoz;
        RaycastHit hit;

        if(Physics.Raycast(transform.position,goPoz,out hit,100f))
        {
            collisionPoz = hit.point;
        }

        isMoving = true;


    }
}
