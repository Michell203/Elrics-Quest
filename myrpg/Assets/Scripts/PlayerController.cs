using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;

    public bool isMoving;

    private Vector2 input;

    private Animator animator;

    public LayerMask solidObjectsLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPosition = transform.position;
                targetPosition.x += input.x;
                targetPosition.y += input.y;

                if(IsWalkable(targetPosition))
                {
                    StartCoroutine(Move(targetPosition));
                }

            }
        }

        animator.SetBool("isMoving",isMoving);
    }

    IEnumerator Move(Vector3 targetPosition)
    {
        isMoving = true;
        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPosition)
    {
        if(Physics2D.OverlapCircle(targetPosition, 0.2f, solidObjectsLayer) != null){
            return false;
        }
        return true;
    }

    

}
