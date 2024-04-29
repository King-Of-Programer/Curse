using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;

    private float playerVelocityY;

    private CharacterController _characterController;

    private float gravityValue = -9.80f;

    private float jumpHeight = 1.0f;

    private bool groundedPlayer;

    private Animator _animator;

  // private AudioSource _stepsAudioSource;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        playerVelocityY = 0f;
        //_stepsAudioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        int animatorState = 0;

        bool groundedPlayer = _characterController.isGrounded;
        if (_characterController.isGrounded)
        {
            groundedPlayer = true;
        }
        if (groundedPlayer && playerVelocityY < 0)
        {
            playerVelocityY = 0f;
        }

        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");
        
        if (Mathf.Abs(dx) > 0 && Mathf.Abs(dy) > 0)
        {
            dx *= 0.707f;
            dy *= 0.707f;
            
        }
        
        if (dy > 0) //&& groundedPlayer)
        {
            
            _animator.SetInteger("State", 1); // ходьба вперед
            
        }
        else if (dy < 0) //&& groundedPlayer)
        {
            _animator.SetInteger("State", 2); // ходьба назад
            
        }
        else if (dx > 0) //&& groundedPlayer)
        {
            _animator.SetInteger("State", 4); // ходьба вправо
            
        }
        else if (dx < 0) //&& groundedPlayer)
        {
            _animator.SetInteger("State", 3); ; // ходьба влево
            
        }
        else
        {
            //animatorState = 0;  
            _animator.SetInteger("State", 0);

        }

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            //animatorState = 0;

            _animator.SetInteger("State", 0);
            groundedPlayer = false;
            playerVelocityY += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }


        Vector3 horizontalForward = Camera.main.transform.forward;

        horizontalForward.y = 0;

        horizontalForward = horizontalForward.normalized;
        float ds = Time.deltaTime / 5.0f;
        if (Input.GetKey(KeyCode.LeftShift) && (dx != 0 || dy != 0))
        {
            _animator.SetInteger("State", 1);

            if (GameState.ChacterStamina > ds)
            {
                dx *= 4f;
                dy *= 4f;
                GameState.ChacterStamina -= ds;
                
            }
            else
            {
                if (GameState.ChacterStamina > 1 - ds)
                {
                    GameState.ChacterStamina += ds;
                }
                else
                {
                    GameState.ChacterStamina = 1f;
                }
            }
        }
        
        //if (animatorState != 0)
        //{
        //    if (!_stepsAudioSource.isPlaying)
        //    {
        //        _stepsAudioSource.Play();
        //    }
        //}

        playerVelocityY += gravityValue * Time.deltaTime;

        _characterController.Move(Time.deltaTime *
        (speed * (dx * Camera.main.transform.right + dy * horizontalForward) +
         playerVelocityY * Vector3.up));

        this.transform.forward = horizontalForward;

        //if (groundedPlayer)
        //{
        //    _animator.SetInteger("State", 0);
        //}


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            groundedPlayer = true;
            _animator.SetInteger("State", 0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            groundedPlayer = false;
        }
    }
    public void OnJumpStart()
    {
        _animator.SetInteger("State", 3);
    }

}

