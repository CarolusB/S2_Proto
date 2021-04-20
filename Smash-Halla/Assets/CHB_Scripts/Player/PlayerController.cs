using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
	public enum StickDirection { Up, Down, Left, Right, Neutral}
	public class PlayerController : MonoBehaviour
	{
		#region Variables
		//PlayerManager manager;
		public bool hitStun = false;
		public bool inAir = false;
		public bool canAttack = true;
		
		bool jumpAction = false;
		bool longHopIntention = false;
		bool attackInput = false;
		bool chargedInput = false;
		
		bool facingRight = false;

		public Rigidbody2D playerRb;
		public int extraJumps = 1;
		[SerializeField]int extraJumpsReserve;

		//float horizontalInput;
		Vector2 vectorInput = Vector2.zero;
		StickDirection stickDirection = StickDirection.Right;
		//float vectorInputMag;
		public float speed;
		public float jumpForce;

		[Header("Attacks")]
		[SerializeField] GameObject attacksGroup;
		//Transform attacksParent;
		[SerializeField] AttackBehavior neutralAttack;
		[SerializeField] AttackBehavior forwardAttack;
		[SerializeField] AttackBehavior upAttack;
		[SerializeField] AttackBehavior neutralAir;
		[SerializeField] AttackBehavior forwardAir;
		[SerializeField] AttackBehavior backAir;
		[SerializeField] AttackBehavior upAir;
		[SerializeField] AttackBehavior downAir;
		[SerializeField] AttackBehavior forwardCharged;
		[SerializeField] AttackBehavior upCharged;
		#endregion

		// Start is called before the first frame update
		void Start()
		{
			extraJumpsReserve = extraJumps;

    //        for (int i = 0; i < attacksGroup.transform.childCount; i++)
    //        {
				//attacksGroup.transform.GetChild(i).gameObject.SetActive(false);
    //        }
		}

        #region Input Events
        public void OnMove(InputAction.CallbackContext context)
        {
			vectorInput = context.ReadValue<Vector2>();
			//horizontalInput = context.ReadValue<float>();
			//vectorInputMag = vectorInput.magnitude;
        }

		public void OnJump(InputAction.CallbackContext context)
        {
			jumpAction = context.action.triggered;
        }

		public void KeepJumpPressed(InputAction.CallbackContext context)
        {
			longHopIntention = context.action.triggered;
        }

		public void OnAttack(InputAction.CallbackContext context)
        {
			attackInput = context.action.triggered;
		}

		public void OnCharged(InputAction.CallbackContext context)
        {
			chargedInput = context.action.triggered;
		}
		#endregion

		// Update is called once per frame
		void Update()
		{
			GetDirectionFromStick();

			if (hitStun)
				return;
			
			MovementInput();
			JumpInput();
		}
		
		void GetDirectionFromStick()
        {
			if (vectorInput.magnitude > 0.15)
			{
				if (Mathf.Abs(vectorInput.y) > Mathf.Abs(vectorInput.x))
				{
					switch (vectorInput.y)
					{
						case float value when (value > 0):
							stickDirection = StickDirection.Up;
							break;
						default:
							stickDirection = StickDirection.Down;
							break;
					}
				}
				else
				{
					switch (vectorInput.x)
					{
						case float value when (value > 0):
							stickDirection = StickDirection.Right;
							break;
						default:
							stickDirection = StickDirection.Left;
							break;
					}
				}
			}
			else stickDirection = StickDirection.Neutral;

			//Debug.Log(vectorInput.x + " | " + vectorInput.y + " | " + stickDirection);
        }

		Vector2 vectorValue;
        private void MovementInput()
        {
			//vecHorizontal = vectorInput.x;

			if (vectorInput.x > 0.15 || vectorInput.x < -0.15)
			{
				vectorValue = new Vector2(vectorInput.x, 0).normalized;

				if (!inAir)
                {
					if (vectorInput.x > 0)
						facingRight = true;
					else
						facingRight = false;
                }
			}
			else
			{
				vectorValue.x = 0f;
			}

			playerRb.velocity = new Vector2(vectorValue.x * speed, playerRb.velocity.y);
		}

		void JumpInput()
        {
			if (jumpAction && !inHopDecision && !onJumpDelay && !longHopIntention)
            {
				StartCoroutine(JumpAgainDelay());
				if (inAir)
                {
					if(extraJumpsReserve > 0)
                    {
						playerRb.velocity = new Vector2(playerRb.velocity.x, 0f);
						playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
						extraJumpsReserve--;
					}
                }
                else
                {
					inHopDecision = true;
					StartCoroutine(CheckShortHop());
				}
            }
        }

		void AttackCommand()
        {

        }

		bool inHopDecision;
		int checkHopframeCount;
		IEnumerator CheckShortHop()
        {
			checkHopframeCount = 0;
			
			while(checkHopframeCount < 3)
            {
				yield return new WaitForSeconds(0.015f);
				checkHopframeCount++;
            }

			if (longHopIntention)
			{
				playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			}
			else
			{
				playerRb.AddForce(Vector2.up * jumpForce * 0.58f, ForceMode2D.Impulse);
			}

			inAir = true;
			inHopDecision = false;
		}

		bool onJumpDelay;
		int jumpDelayFramecount;
		public IEnumerator JumpAgainDelay()
        {
			onJumpDelay = true;
			jumpDelayFramecount = 0;

			while(jumpDelayFramecount < 2)
            {
				yield return new WaitForFixedUpdate();
				jumpDelayFramecount++;
			}
			

			onJumpDelay = false;
        }

		public void ResetJumps()
        {
			extraJumpsReserve = extraJumps;
        }
    }
}

