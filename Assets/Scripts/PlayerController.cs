using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{

	[SerializeField] float _moveSpeed = 70;
	[SerializeField] float _movementSmoothing = 0.1f;
	[SerializeField] bool _normalizedMovement = true;
	[SerializeField] GameObject _downObject;

	private float _speed;
	private Vector2 _axisVector = Vector2.zero;
	private Vector3 _currentVelocity = Vector3.zero;
    private bool _canInteractUI = false;
    private bool _canMove = true;

	Rigidbody2D _rb2D;
	Animator _currentAnimator;
	Animator downAnimator;
    ShopKeeper _currentShopKeeper = null;

	void Start()
	{
		_rb2D = GetComponent<Rigidbody2D>();
		_downObject.SetActive(true);
		downAnimator = _downObject.GetComponent<Animator>();
		_currentAnimator = downAnimator;
	}

	void Update()
    {
        if(_canMove)
        {
            CalCulateMovement();
            SetAnimations();
        }

        if (_canInteractUI)
        {
            if (Input.GetKeyDown(KeyCode.E) && _currentShopKeeper != null)
            {
                UIManager.Instance.ActivateDialogPanel(true);
                _canMove = false;
            }
        }
    }

    void FixedUpdate()
    {
        // Move our character
        Move();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Shop Keeper"))
        {
            var keeper = other.GetComponent<ShopKeeper>();
            _canInteractUI = true;
            keeper.ActivateButton(true);
            _currentShopKeeper = keeper;
            Shop.OnCloseBuyShop += AllowPlayerMovement;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Shop Keeper"))
        {
            var keeper = other.GetComponent<ShopKeeper>();
            _canInteractUI = false;
            keeper.ActivateButton(false);
            keeper.ButtonPressed(false);
            _currentShopKeeper = null;
            Shop.OnCloseBuyShop -= AllowPlayerMovement;
        }
    }

    private void CalCulateMovement()
    {
        // get speed from the rigid body to be used for animator parameter Speed
        _speed = _rb2D.velocity.magnitude;

        // Get input axises
        _axisVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //normalize it for good topdown diagonal movement
        if (_normalizedMovement == true)
        {
            _axisVector.Normalize();
        }
    }

    private void SetAnimations()
    {
        // Set speed parameter to the animator
        _currentAnimator.SetFloat("Speed", _speed);

        // Check keys for actions and use appropiate function
        //
        if (Input.GetKey(KeyCode.Space))  // SWING ATTACK
        {
            PlayAnimation("Swing");
        }

        else if (Input.GetKey(KeyCode.C))  // THRUST ATTACK
        {
            PlayAnimation("Thrust");
        }

        else if (Input.GetKey(KeyCode.X))  // BOW ATTACK
        {
            PlayAnimation("Bow");
        }
    }

	void PlayAnimation(string animationName)
	{
		// Play given animation in the current directions animator
		_currentAnimator.Play(animationName, 0);
	}

	void Move()
	{
        if (!_canMove) return;

        // Set target velocity to smooth towards
        Vector2 targetVelocity = new Vector2(_axisVector.x * _moveSpeed * 10f, _axisVector.y * _moveSpeed * 10) * Time.fixedDeltaTime;

		// Smoothing out the movement
		_rb2D.velocity = Vector3.SmoothDamp(_rb2D.velocity, targetVelocity, ref _currentVelocity, _movementSmoothing);
	}

    public bool SetUIInteraction(bool interact)
    {
        return _canInteractUI = interact;
    }

    private void AllowPlayerMovement()
    {
        _canMove = true;
    }
}
