using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private bool isRun = false;
    private bool isGround = true;
    public Animator walk;

    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float applySpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float lookSensitivity;  
    [SerializeField]
    private float cameraRotationLimit;  
    private float currentCameraRotationX; 
    [SerializeField]
    private Camera theCamera; 
    private Rigidbody myRigid;
    private CapsuleCollider capsuleCollider;

    void Start()
    {
        walk = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
    }

    void Update()  
    {
        Move();
        IsGround();
        TryJump();
        TryRun();
        CameraRotation();
    }

    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal"); 
        float moveDirZ = Input.GetAxisRaw("Vertical"); 
        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * moveDirZ; 
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * applySpeed; 
        myRigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }
    private void Jump()
    {

        myRigid.velocity = transform.up * jumpForce;
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }
    private void Running()
    {

        isRun = true;
        applySpeed = runSpeed;
    }
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    private void CameraRotation() 
    {
        float xRotation = Input.GetAxisRaw("Mouse Y"); 
        float cameraRotationX = xRotation * lookSensitivity;
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    void FixedUpdate()
    {
    }
}
