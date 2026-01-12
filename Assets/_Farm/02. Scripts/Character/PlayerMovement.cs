using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController cc;
    private Animator anim;

    private Vector3 moveInput;
    private Vector3 moveVector;
    private Vector3 verticalVelocity;

    private float currSpeed;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;

    [SerializeField] private float rotSpeed = 10f;

    [SerializeField] private float gravity = -30f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Turn();
        SetAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<ITriggerEvent>();

        if (interactable != null)
            interactable.InteractionEnter();
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<ITriggerEvent>();

        if (interactable != null)
            interactable.InteractionExit();
    }

    private void Move()
    {
        if (moveInput.magnitude >= 0.1f) // 움직일 때
        {
            bool isRun = Input.GetKey(KeyCode.LeftShift);
            currSpeed = isRun ? runSpeed : walkSpeed;

            if (cc.isGrounded && verticalVelocity.y < 0) // 바닥에 닿은 상태일 때, 중력 초기화
                verticalVelocity.y = -1f;

            verticalVelocity.y += gravity * Time.deltaTime;

            moveVector = moveInput.normalized * currSpeed;
            
            Vector3 finalMovement = (moveVector + verticalVelocity) * Time.deltaTime;
            cc.Move(finalMovement); // 이동 기능
        }
        else // 안 움직일 때
        {
            currSpeed = 0f;
            cc.Move(verticalVelocity * Time.deltaTime);
        }
    }

    private void Turn()
    {
        if (moveInput.magnitude >= 0.1f) // 회전 기능
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        }
    }

    private void SetAnimation()
    {
        // anim.SetFloat("Speed", currSpeed); // 즉시 변경
        anim.SetFloat("Speed", currSpeed, 0.1f, Time.deltaTime); // 부드럽게 변경
    }

    private void OnMove(InputValue value) // New Input System에서 데이터를 가져오는 기능
    {
        Vector2 inputDir = value.Get<Vector2>();
        moveInput = new Vector3(inputDir.x, 0f, inputDir.y);
    }
}