using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class PlayerController : MonoBehaviour
	{
		#region Variables
		//PlayerManager manager;
		public bool hitStun = false;
		public bool inAir = false;
		bool facingLeft = false;

		public Rigidbody2D playerRb;
		public int extraJumps = 1;
		int extraJumpsReserve;

		float horizontalInput;
		Vector2 vectorInput = Vector2.zero;
		public float speed;
		public float jumpForce;
		#endregion

		// Start is called before the first frame update
		void Start()
		{
			extraJumpsReserve = extraJumps;
		}

		// Update is called once per frame
		void Update()
		{
			if (hitStun)
				return;
			MovementInput();
			JumpInput();
		}

        private void MovementInput()
        {
			horizontalInput = Input.GetAxis("Horizontal");

			if (horizontalInput < -0.15 || horizontalInput > 0.15)
			{
				vectorInput = new Vector2(horizontalInput, 0);

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
				vectorInput = Vector2.zero;
			}

			playerRb.velocity = vectorInput.normalized * speed;
		}

		void JumpInput()
        {
			if (Input.GetButtonDown("Jump"))
            {
                if (inAir)
                {
					if(extraJumpsReserve > 0)
                    {
						playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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

                if (Input.GetButton("Jump"))
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

