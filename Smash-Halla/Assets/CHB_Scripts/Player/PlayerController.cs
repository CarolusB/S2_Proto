using System;
using System.Collections;
using System.Collections.Generic;
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
		public bool IsCharging { get { return chargedInput; } }
		
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

		[SerializeField] int maxChargeTime = 60;
		[SerializeField] int chargeMaxHoldTime = 100;

		[Header("Attacks")]
		[SerializeField] AttackBehavior neutralAttack;
		[SerializeField] AttackBehavior upAttack;
		[SerializeField] AttackBehavior neutralAir;
		[SerializeField] AttackBehavior forwardAir;
		[SerializeField] AttackBehavior backAir;
		[SerializeField] AttackBehavior upAir;
		[SerializeField] AttackBehavior downAir;
		[SerializeField] AttackBehavior forwardCharged;
		[SerializeField] AttackBehavior upCharged;

		[HideInInspector] public AttackBehavior currentAttack;

		[SerializeField] List<GameObject> attackParts;
		#endregion

		// Start is called before the first frame update
		void Start()
		{
			foreach (GameObject attackPart in attackParts)
			{
				attackPart.SetActive(false);
			}

			extraJumpsReserve = extraJumps;
			currentAttack = null;
        }

		#region Input Events
		public void OnMove(InputAction.CallbackContext context)
        {
			vectorInput = context.ReadValue<Vector2>();
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

			if (!hitStun || canAttack)
            {
				MovementInput();
				JumpInput();
				AttackCommand();
			}

			CheckAttackEnding();
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

		#region Jump Methods
		void JumpInput()
        {
			if (jumpAction && !inHopDecision && !onJumpDelay && !longHopIntention && !chargedInput)
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

			while (checkHopframeCount < 3)
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

			while (jumpDelayFramecount < 2)
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
        #endregion

        #region Attack Methods
        void AttackCommand()
        {
			if (currentAttack != null)
				return;
			
			if (attackInput)
            {
                if (!inAir)
                {
					if(stickDirection == StickDirection.Up)
                    {
						currentAttack = upAttack.StartAttack(facingRight);
                    }
                    else
                    {
						currentAttack = neutralAttack.StartAttack(facingRight);
					}

					canAttack = false;
                }
                else
                {
                    switch (stickDirection)
                    {
						case StickDirection.Neutral:
							currentAttack = neutralAir.StartAttack(facingRight);
							break;

						case StickDirection.Right:
                            if (facingRight)
                            {
								currentAttack = forwardAir.StartAttack(facingRight);
							}
                            else
                            {
								currentAttack = backAir.StartAttack(facingRight);
							}
							break;

						case StickDirection.Left:
                            if (!facingRight)
                            {
								currentAttack = forwardAir.StartAttack(facingRight);
							}
                            else
                            {
								currentAttack = backAir.StartAttack(facingRight);
							}
							break;

						case StickDirection.Up:
							currentAttack = upAir.StartAttack(facingRight);
							break;

						case StickDirection.Down:
							currentAttack = downAir.StartAttack(facingRight);
							break;
                    }
                }
            }
			else if (chargedInput && !inAir)
            {
                if(stickDirection == StickDirection.Up)
                {
					currentAttack = upCharged.StartAttack(facingRight);
				}
                else
                {
					currentAttack = forwardCharged.StartAttack(facingRight);
				}

				canAttack = false;
				StartCoroutine(ChargingAttack());
			}
        }

		private void CheckAttackEnding()
		{
            if (currentAttack != null && !currentAttack.Ongoing)
            {
				currentAttack = null;
				canAttack = true;
            }
		}

		int chargingFrameCount;
		float addingDamage;
		IEnumerator ChargingAttack()
        {
			chargingFrameCount = 0;
			addingDamage = 0f;

            while (chargedInput && chargingFrameCount < chargeMaxHoldTime)
            {
                yield return new WaitForFixedUpdate();
                chargingFrameCount++;
            }

			addingDamage = 0.0029f * Mathf.Pow(Mathf.Min(chargingFrameCount, maxChargeTime), 2);

			foreach(HitboxInfo hitbox in currentAttack.hitboxes)
            {
				hitbox.values.damageInput += addingDamage; //koneri mais oki
            }
			yield return null;
        }
        #endregion
    }
}

