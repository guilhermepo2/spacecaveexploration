﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHurtable {
    public enum EPlayerState {
        EDummy,
        ENormal,
        EJumping
    }

    // ground velocity
    [Header("Ground Velocity")]
    public float RunSpeed = 3f;
    public float GroundDamping = 5f;
    public float InAirDamping = 2f;

    [Header("Jumping")]
    public float JumpPeakHeight = 2f;
    public float HorizontalDistanceToJumpPeak = 2f;
    public float JumpCutValue = 0.35f;
    public float TerminalVelocity = -25.0f;
    public float DownGravityMultiplier = 3f;
    private const float JUMP_PRESSED_REMEMBER_TIME = 0.25f;
    private const float GROUNDED_REMEMBER_TIME = 0.25f;

    private float GoingUpGravity;
    private float GoingDownGravity;
    private float JumpInitialVelocity;

    private float m_Gravity;
    private float m_JumpPressedRemember;
    private float m_GroundedRemember;
    private Mover m_CharacterMover;
    private Shooter m_CharacterShooter;
    private HealthComponent m_CharacterHealth;

    private float m_NormalizedHorizontalSpeed = 0.0f;
    private EPlayerState m_CurrentPlayerState = EPlayerState.ENormal;

    // ANIMATION NAMES
    private string IdleAnimation    = "Idle";
    private string StandAnimation   = "Stand";
    private string RunAnimation     = "Run";
    private string JumpAnimation    = "Jump";

    // footstep sounds
    private float m_IntervalBetweenFootsteps = 0.35f;
    private float m_TimeElapsedSinceLastFootstep = 0.0f;

    // UI stuff
    public event Action OnItemCollected;
    private int m_GreenJarAmount = 0;
    public int GreenJarCount { get { return m_GreenJarAmount; } }
    private int m_BlueJarAmount = 0;
    public int BlueJarCount { get { return m_BlueJarAmount; } }
    private int m_RedJarAmount = 0;
    public int RedJarCount { get { return m_RedJarAmount; } }
    private int m_ChestAmount = 0;
    public int ChestCount { get { return m_ChestAmount; } }

    private void Awake() {
        GoingUpGravity = (-(2 * JumpPeakHeight * RunSpeed * RunSpeed)) / (HorizontalDistanceToJumpPeak * HorizontalDistanceToJumpPeak);
        GoingDownGravity = GoingUpGravity * DownGravityMultiplier;
        JumpInitialVelocity = ((2 * JumpPeakHeight * RunSpeed) / HorizontalDistanceToJumpPeak);
        m_CharacterMover = GetComponent<Mover>();
        m_CharacterShooter = GetComponent<Shooter>();
        m_CharacterHealth = GetComponent<HealthComponent>();

        m_CharacterMover.OnControllerCollidedEvent += OnControllerCollider;
        m_CharacterMover.OnTriggerEnterEvent += EnteredTrigger;
    }

    private void Start() {
        m_Gravity = GoingUpGravity;
    }

    void EnteredTrigger(Collider2D other) {
        if(other.tag == "Enemy") {
            m_CharacterHealth.DealDamage(1);
        }
    }

    void OnControllerCollider(RaycastHit2D hit) {
        if (hit.normal.y == 1.0f) {
            return;
        }
    }

    private void Update() {
        m_GroundedRemember -= Time.deltaTime;
        m_JumpPressedRemember -= Time.deltaTime;
        m_NormalizedHorizontalSpeed = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space)) {
            m_JumpPressedRemember = JUMP_PRESSED_REMEMBER_TIME;
        }

        if(m_CharacterMover.IsGrounded) {
            m_GroundedRemember = GROUNDED_REMEMBER_TIME;
            m_CharacterMover.Velocity.y = 0.0f;
        }

        // playing footstep sounds
        m_TimeElapsedSinceLastFootstep += Time.deltaTime;
        if ( (Mathf.Abs(m_CharacterMover.Velocity.x) > 0.1f) && m_TimeElapsedSinceLastFootstep >= m_IntervalBetweenFootsteps && m_CurrentPlayerState != EPlayerState.EJumping) {
            m_TimeElapsedSinceLastFootstep = 0.0f;
            int RandomSound = UnityEngine.Random.Range(0, SoundBank.instance.PlayerFootsteps.Length);
            SoundManager.instance.PlayEffect(SoundBank.instance.PlayerFootsteps[RandomSound]);
        }

        switch(m_CurrentPlayerState) {
            case EPlayerState.ENormal:
                if(m_JumpPressedRemember >= 0.0f && m_GroundedRemember >= 0.0f) {
                    m_JumpPressedRemember = 0;
                    m_GroundedRemember = 0;

                    m_Gravity = GoingUpGravity;
                    m_CharacterMover.Velocity.y = JumpInitialVelocity;
                    m_CurrentPlayerState = EPlayerState.EJumping;
                    SoundManager.instance.PlayEffect(SoundBank.instance.PlayerJump);
                }
                break;
            case EPlayerState.EJumping:
                if(Input.GetKeyUp(KeyCode.Space) && m_CharacterMover.Velocity.y > 0) {
                    m_CharacterMover.Velocity.y *= JumpCutValue;
                }

                if(m_CharacterMover.IsGrounded) {
                    m_CurrentPlayerState = EPlayerState.ENormal;
                }
                break;
        }

        ProcessSpriteScale();
        ProcessAnimation();

        // Shooting has to happen after handling sprite scale
        if (Input.GetMouseButton(0)) {
            Vector3 Direction = new Vector3(
                Mathf.Sign(transform.localScale.x),
                0.0f, // we can shoot up (for now?!)
                0.0f
                );

            m_CharacterShooter.Shoot(Direction);
        }

        if (m_CharacterMover.Velocity.y < 0.0f) {
            m_Gravity = GoingDownGravity;
            m_CurrentPlayerState = EPlayerState.EJumping;
        }

        float SmoothedMovementFactor = m_CharacterMover.IsGrounded ? GroundDamping : InAirDamping;

        // force removing momentum when player is not pressing any key
        if(m_NormalizedHorizontalSpeed == 0) {
            SmoothedMovementFactor *= 2.5f;
        }

        m_CharacterMover.Velocity.x = Mathf.Lerp(m_CharacterMover.Velocity.x, m_NormalizedHorizontalSpeed * RunSpeed, SmoothedMovementFactor * Time.deltaTime);
        m_CharacterMover.Velocity.y = Mathf.Max(TerminalVelocity, (m_CharacterMover.Velocity.y + (m_Gravity * Time.deltaTime)));

        Vector2 VerletVelocity = new Vector2(m_CharacterMover.Velocity.x, m_CharacterMover.Velocity.y + (0.5f * m_Gravity * Time.deltaTime * Time.deltaTime));
        Vector2 VerletDeltaMovement = VerletVelocity * Time.deltaTime;

        m_CharacterMover.Move(VerletDeltaMovement);
    }

    private void ProcessSpriteScale() {
        if(m_NormalizedHorizontalSpeed == 0 && Mathf.Abs(m_CharacterMover.Velocity.x) < Mathf.Epsilon) {
            return;
        }

        float signal =  Mathf.Abs(m_NormalizedHorizontalSpeed) > Mathf.Abs(m_CharacterMover.Velocity.x) ?
            Mathf.Sign(m_NormalizedHorizontalSpeed) :
            Mathf.Sign(m_CharacterMover.Velocity.x);

        transform.localScale = new Vector3(signal * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    private void ProcessAnimation() {
        // cache this reference when the game begins
        Animator m_Animator = GetComponentInChildren<Animator>();

        if(m_CharacterHealth.IsInvincible) {
            m_Animator.Play("Hurt");
        } else if(Mathf.Abs(m_CharacterMover.Velocity.y) > Mathf.Epsilon) {
            m_Animator.Play(JumpAnimation);
        } else if(Mathf.Abs(m_NormalizedHorizontalSpeed) > 0.1f) {
            m_Animator.Play(RunAnimation);
        } else {
            m_Animator.Play(StandAnimation);
        }
    }

    public bool IsMoving() {
        return (m_CharacterMover.Velocity.magnitude > 0.1f);
    }

    // Hurtable
    void IHurtable.Hit() {
        int RandomSound = UnityEngine.Random.Range(0, SoundBank.instance.PlayerHit.Length);
        SoundManager.instance.PlayEffect(SoundBank.instance.PlayerHit[RandomSound]);
    }

    void IHurtable.Die() {
        SoundManager.instance.PlayEffect(SoundBank.instance.PlayerDie);
    }

    public void GiveItem(Item.EPossibleItems ItemType) {
        Debug.Log($"Player getting item: {ItemType}");

        switch(ItemType) {
            case Item.EPossibleItems.Health:
                GetComponent<HealthComponent>().Heal(2);
                break;
            case Item.EPossibleItems.BlueJar:
                m_BlueJarAmount++;
                break;
            case Item.EPossibleItems.GreenJar:
                m_GreenJarAmount++;
                break;
            case Item.EPossibleItems.RedJar:
                m_RedJarAmount++;
                break;
            case Item.EPossibleItems.Chest:
                m_ChestAmount++;
                break;
            case Item.EPossibleItems.Eskar:
                FindObjectOfType<EndLevelUI>().GotChest();
                break;
            default:
                break;
        }

        OnItemCollected?.Invoke();
    }
}
