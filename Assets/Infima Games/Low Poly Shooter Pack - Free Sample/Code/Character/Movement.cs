// Copyright 2021, Infima Games. All Rights Reserved.


//Additions by Finn Wiskandt:
// Crouching, Jumping, Communication Improvements

using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InfimaGames.LowPolyShooterPack
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class Movement : MovementBehaviour
    {
        #region FIELDS SERIALIZED
        
        [Header("Audio Clips")]
        
        [Tooltip("The audio clip that is played while walking.")]
        [SerializeField]
        private AudioClip audioClipWalking;

        [Tooltip("The audio clip that is played while running.")]
        [SerializeField]
        private AudioClip audioClipRunning;

        [Header("Movement-Basics")]

        [SerializeField]
        private float speedWalking = 5.0f;

        [Tooltip("How fast the player moves while running."), SerializeField]
        private float speedRunning = 9.0f;

        [Header("Jumping")]
        [Tooltip("The duration of the time in which the player is elevated into the air")]
        [SerializeField]
        private float jumpLenght=0.5f;
        
        
        [Tooltip("The amount of force which is applied to the player while he jumps up")]
        [SerializeField]
        private float jumpStrenght=1.2f;

        [Tooltip("The amount of force which is applied to the player after he was elevated into the air")]
        [SerializeField]
        private float jumpForceDown=500;
        
        
        [Header("Crouching")]
        [Tooltip("The factor subtracted by the capsule size.")]
        [SerializeField]
        private float crouchFactor=0.2f;

        [Tooltip("How fast the player moves while crouching")]
        [SerializeField]
        private float speedCrouching=2.0f;
        #endregion

        #region PROPERTIES

        //Velocity.
        private Vector3 Velocity
        {
            //Getter.
            get => rigidBody.velocity;
            //Setter.
            set => rigidBody.velocity = value;
        }

        #endregion

        #region FIELDS

        /// <summary>
        /// Attached Rigidbody.
        /// </summary>
        private Rigidbody rigidBody;
        /// <summary>
        /// Attached CapsuleCollider.
        /// </summary>
        private CapsuleCollider capsule;
        /// <summary>
        /// Attached AudioSource.
        /// </summary>
        private AudioSource audioSource;
        
        /// <summary>
        /// True if the character is currently grounded.
        /// </summary>
        private bool grounded;

        private bool crouching;
        /// <summary>
        /// Player Character.
        /// </summary>
        ///
        ///
        /// 
       [SerializeField]
        private CharacterBehaviour playerCharacter;
        
        /// <summary>
        /// The player character's equipped weapon.
        /// </summary>
        private WeaponBehaviour equippedWeapon;

        private bool jumping; 
        
        
        /// <summary>
        /// Array of RaycastHits used for ground checking.
        /// </summary>
        private readonly RaycastHit[] groundHits = new RaycastHit[8];
        
      
        
        #endregion

        #region UNITY FUNCTIONS

        /// <summary>
        /// Awake.
        /// </summary>
        protected override void Awake()
        {
            //Get Player Character.
            //playerCharacter = ServiceLocator.Current.Get<IGameModeService>().GetPlayerCharacter();
           
        }

        /// Initializes the FpsController on start.
        protected override  void Start()
        {
            //Rigidbody Setup.
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            //Cache the CapsuleCollider.
            capsule = GetComponent<CapsuleCollider>();

            //Audio Source Setup.
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioClipWalking;
            audioSource.loop = true;

            playerCharacter.OnCrouchStart += StartCrouch;
            playerCharacter.OnCrouchEnd += EndCrouch;
            playerCharacter.OnJump += StartJump;
        }

        /// Checks if the character is on the ground.
        private void OnCollisionStay()
        {
            //Bounds.
            Bounds bounds = capsule.bounds;
            //Extents.
            Vector3 extents = bounds.extents;
            //Radius.
            float radius = extents.x - 0.01f;
            
            //Cast. This checks whether there is indeed ground, or not.
            Physics.SphereCastNonAlloc(bounds.center, radius, Vector3.down,
                groundHits, extents.y - radius * 0.5f, ~0, QueryTriggerInteraction.Ignore);
            
            //We can ignore the rest if we don't have any proper hits.
            if (!groundHits.Any(hit => hit.collider != null && hit.collider != capsule)) 
                return;
            
            //Store RaycastHits.
            for (var i = 0; i < groundHits.Length; i++)
                groundHits[i] = new RaycastHit();

            //Set grounded. Now we know for sure that we're grounded.
            grounded = true;
        }
			
        protected override void FixedUpdate()
        {

            //Move.
            MoveCharacter();
            
            //Unground.
            grounded = false;
            
        }

    
        protected override  void Update()
        {
            //Get the equipped weapon!
            equippedWeapon = playerCharacter.GetInventory().GetEquipped();
            
            //Play Sounds!
            PlayFootstepSounds();
        }

        #endregion

        #region METHODS

        private void MoveCharacter()
        {
            #region Calculate Movement Velocity

            //Get Movement Input!
            Vector2 frameInput = playerCharacter.GetInputMovement();
            
            //Calculate local-space direction by using the player's input.
            var movement = new Vector3(frameInput.x, 0.0f, frameInput.y);

            if (crouching)
            {
                movement *= speedCrouching;
            }
            
            //Running speed calculation.
            else if(playerCharacter.IsRunning())
                movement *= speedRunning;
            else
            {
                //Multiply by the normal walking speed.
                movement *= speedWalking;
            }

            //World space velocity calculation. This allows us to add it to the rigidbody's velocity properly.
            movement = transform.TransformDirection(movement);

            #endregion
            
            //Update Velocity.
            Velocity = new Vector3(movement.x, Velocity.y, movement.z);
        }

        /// <summary>
        /// Plays Footstep Sounds. This code is slightly old, so may not be great, but it functions alright-y!
        /// </summary>
        private void PlayFootstepSounds()
        {
            //Check if we're moving on the ground. We don't need footsteps in the air.
            if (grounded && rigidBody.velocity.sqrMagnitude > 0.1f)
            {
                //Select the correct audio clip to play.
                audioSource.clip = playerCharacter.IsRunning() ? audioClipRunning : audioClipWalking;
                //Play it!
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
            //Pause it if we're doing something like flying, or not moving!
            else if (audioSource.isPlaying)
                audioSource.Pause();
        }
        

        private void StartCrouch()
        {
            crouching = true;
            Debug.Log("Starting Crouching.");

            capsule.height -= crouchFactor;

        }

        private void EndCrouch()
        {
            Debug.Log("Ending Crouching.");
            crouching = false;
            capsule.height += crouchFactor;
        }

        private void StartJump()
        {
            if (!grounded)
            {
                Debug.Log("Not grounded for Jump!");
                return;
            }
            
            Debug.Log("Adding Velocity for Jump!");
            
            rigidBody.AddForce(Vector3.up*jumpStrenght);
            jumping = true;
            Invoke(nameof(EndJump), jumpLenght );
        }

        private void EndJump()
        {
            rigidBody.AddForce(Vector3.down*jumpForceDown);
        }
        
        #endregion
    }
}