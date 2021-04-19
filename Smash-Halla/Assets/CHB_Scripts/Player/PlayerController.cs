using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
	public class PlayerController : MonoBehaviour
	{
		#region Variables
		//PlayerManager manager;
		public bool hitStun = false;
		public bool inAir = false;
		bool jumpAction = false;
		bool longHopIntention = false;
		bool facingLeft = false;

		public Rigidbody2D playerRb;
		public int extraJumps = 1;
		[SerializeField]int extraJumpsReserve;

		//float horizontalInput;
		Vector2 vectorInput = Vector2.zero;
		//float vectorInputMag;
		public float speed;
		public float jumpForce;
		#endregion

		// Start is called before the first frame update
		void Start()
		{
			extraJumpsReserve = extraJumps;
		}

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

		// Update is called once per frame
		void Update()
		{
			if (hitStun)
				return;
			MovementInput();
			JumpInput();
		}
		//float vecHorizontal;
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
						facingLeft = false;
					else
						facingLeft = true;
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
			if (jumpAction && !inHopDecision && !onJumpDelay)
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
		public IEnumerator JumpAgainDelay()
        {
			onJumpDelay = true;

			yield return new WaitForSeconds(0.030f);

			onJumpDelay = false;
        }

		public void ResetJumps()
        {
			extraJumpsReserve = extraJumps;
        }
    }
}

