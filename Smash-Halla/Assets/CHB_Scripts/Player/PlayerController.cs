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
		bool facingLeft = false;

		public Rigidbody2D playerRb;
		public int extraJumps = 1;
		int extraJumpsReserve;

		float horizontalInput;
		Vector2 vectorInput = Vector2.zero;
		float vectorInputMag;
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
		// Update is called once per frame
		void Update()
		{
			if (hitStun)
				return;
			MovementInput();
			JumpInput();
		}
		float vecHorizontal;
		Vector2 vectorValue;
        private void MovementInput()
        {
			vecHorizontal = vectorInput.x;

			if (vecHorizontal > 0.15 || vecHorizontal < -0.15)
			{
				vectorValue = new Vector2(vecHorizontal, 0);

				if (!inAir)
                {
					if (horizontalInput > 0)
						facingLeft = false;
					else
						facingLeft = true;
                }
			}
			else
			{
				vectorValue = Vector2.zero;
			}

			playerRb.velocity = vectorValue.normalized * speed;
		}

		void JumpInput()
        {
			if (jumpAction)
            {
                if (inAir)
                {
					if(extraJumpsReserve > 0)
                    {
						playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
						extraJumpsReserve--;
					}
                }
                else
                {
					inAir = true;
					StartCoroutine(CheckShortHop());
				}
            }
        }

		int frameCount;
		IEnumerator CheckShortHop()
        {
			frameCount = 0;

			while(frameCount < 3)
            {
				yield return new WaitForFixedUpdate();
				frameCount++;

                if (jumpAction)
                {
					playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
				}
                else
                {
					playerRb.AddForce(Vector2.up * jumpForce * 0.52f, ForceMode2D.Impulse);
				}
            }
        }
    }
}

