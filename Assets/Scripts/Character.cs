using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    CharacterController controller;
    Vector3 velocity;
    Animator animator;
    Collider shovelCollider;

    public float moveSpeed = 5.0f;
    public float gravity = -9.81f;
    public float throwForce = 1.0f;

    public Rock rockPrefab;

    public InputActionReference moveInput;
    public InputActionReference attackInput;
    public InputActionReference weaponSwitchInput;
    public InputActionReference dropInventoryInput;
    public InputActionReference showInventoryInput;

    [HideInInspector]
    public UnityEvent OnItemDropped;

    [HideInInspector]
    public UnityEvent<bool> OnInventoryShown;

    public Shovel shovel;
    public Rock rock;
    public GameObject armRight;

    //added = player health system
    public int health = 3;

    bool shortRangeAttack = true;
    bool startRockSpawn = false;
    float rockTimer = 0.0f;
    bool showInventory = false;

    void Awake()
    {
        if (OnItemDropped == null) OnItemDropped = new UnityEvent();

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        shovelCollider = shovel.GetComponent<Collider>();
        shovelCollider.enabled = false;

        shortRangeAttack = true;

        dropInventoryInput.action.performed += DropInventoryPerformed;

        showInventoryInput.action.performed += ShowInventoryPerformed;
        showInventoryInput.action.canceled += ShowInventoryCanceled;
    }

    private void ShowInventoryCanceled(InputAction.CallbackContext obj)
    {
        showInventory = false;
        OnInventoryShown.Invoke(showInventory);
    }
    private void ShowInventoryPerformed(InputAction.CallbackContext obj)
    {
        showInventory = true;
        OnInventoryShown.Invoke(showInventory);
    }
    private void DropInventoryPerformed(InputAction.CallbackContext obj)
    {
        OnItemDropped.Invoke();
    }
    private void Start()
    {
        UpdateWeapon();
    }
    private void Update()
    {
        bool inputEnabled = !showInventory;

        PlayerMotion(inputEnabled);

        bool attack = attackInput.action.WasPressedThisFrame();
        if (attack && inputEnabled)
        {
            if (shortRangeAttack)
            {
                animator.SetTrigger("StartAttack");
            }
            else
            {
                if (rock != null)
                {
                    ThrowRock();
                    animator.SetTrigger("StartAttack");
                }
            }
        }

        bool weaponSwitch = weaponSwitchInput.action.WasPressedThisFrame();
        if (weaponSwitch && inputEnabled)
        {
            shortRangeAttack = !shortRangeAttack;
            UpdateWeapon();
        }

        if (startRockSpawn) SpawnRockDelay();
    }
    void PlayerMotion(bool inputEnabled)
    {
        if (!inputEnabled) return;

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector2 moveDirection = moveInput.action.ReadValue<Vector2>();

        Vector3 move = Vector3.right * moveDirection.x + Vector3.forward * moveDirection.y;
        Vector3 moveVelocity = move * moveSpeed;

        velocity.y += gravity * Time.deltaTime;
        moveVelocity.y = velocity.y;

        controller.Move(moveVelocity * Time.deltaTime);

        Vector3 horizontalVelocity = new Vector3(moveVelocity.x, 0f, moveVelocity.z);
        if (horizontalVelocity.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(horizontalVelocity);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                15f * Time.deltaTime
            );
        }

        animator.SetFloat("Speed", horizontalVelocity.magnitude);
    }

    void ThrowRock()
    {
        if (rock != null)
        {
            Vector3 point1 = rock.transform.position;
            point1.y = 0f;
            Vector3 point2 = GetMouseHitPoint();
            point2.y = 0f;

            Vector3 direction = (point2 - point1).normalized;

            rock.Throw(direction, throwForce);
            rock = null;
            startRockSpawn = true;
        }
    }


    Vector3 GetMouseHitPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    void SpawnRockDelay()
    {
        rockTimer += Time.deltaTime;

        if (rockTimer >= 0.5f)
        {

            rockTimer = 0.0f;
            startRockSpawn = false;
            rock = Instantiate(rockPrefab, armRight.transform);

            if (shortRangeAttack) rock.gameObject.SetActive(false);
        }
    }

    void UpdateWeapon()
    {
        shovel.gameObject.SetActive(shortRangeAttack);

        if (rock != null) rock.gameObject.SetActive(!shortRangeAttack);

        if (shortRangeAttack)
        {
            shovel.EnableHitBox(0);
        }
        else
        {
            if (rock != null) rock.EnableHitBox(0);
        }
    }

    public void EnableHitBox(int value)
    {
        if (shortRangeAttack)
        {
            shovel.EnableHitBox(value);
        }
    }

    //added= damage system
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Player Health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    //addded = gameover 
    void Die()
    {
        Debug.Log("GAME OVER");

        Time.timeScale = 0f;
    }
}