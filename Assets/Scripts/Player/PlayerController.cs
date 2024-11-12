using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float sensitivity;

    [SerializeField]
    Transform hand;

    [SerializeField]
    InteractWithItem _interact;

    [SerializeField]
    Rigidbody _rb;

    [SerializeField]
    CharacterController _characterController;

    [SerializeField]
    Camera _camera;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction interactAction;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");

    }

    public void Move(Vector2 direction)
    {
        Vector3 move = (transform.right * direction.x + transform.forward * direction.y);
        _characterController.Move(move * speed * Time.deltaTime);
    }
    public void Look(Vector2 deltaPointer)
    {
        _camera.transform.eulerAngles += new Vector3(Mathf.Clamp(-deltaPointer.y, -90, 90), 0, 0) * sensitivity * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, deltaPointer.x, 0) * sensitivity * Time.deltaTime;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        _interact.Action(hand);
    }



    void Update()
    {
        Move(moveAction.ReadValue<Vector2>());
        Look(lookAction.ReadValue<Vector2>());

    }
}
