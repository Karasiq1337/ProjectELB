using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerMovement : MonoBehaviour
{
    Vector2 movementInput;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    PlayerDamageTaking playerDamageTaking;

    [Header("Sounds")]
    [SerializeField] AudioSource dashSound;
    [SerializeField] AudioSource attackSound;
    [SerializeField] AudioSource walkSound;
    [SerializeField] AudioSource walkSound2;



    bool playingAttack;
    Coroutine attackPlaying;
    bool playingWalk;
    Coroutine walkPlaying;
    bool playingDash = false;
    Coroutine dashPlaying;

    [Header("Attack")]
    [SerializeField] float attackOffset;
    [SerializeField] GameObject attackSplash;
    [SerializeField] float atackTime;
    private bool isAttacking;
    private bool isAttackAnimation;

    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;
    public bool isDashing;
    private float localMoveSpeed;
    private string currentAnimaton;
    private Vector3 mousePosition;

    public ContactFilter2D dashFilter;
    List<RaycastHit2D> dashCollisions = new List<RaycastHit2D>();

    private Coroutine currentDash = null;
    private Vector2 playerDirection;
    private Vector2 unConitiusDerection;
    private bool canDash;
    private bool canAttack;
    private string animationString;

    [Header("Dash")]
    [SerializeField] float dashDistance;
    [SerializeField] float dashTime;
    [SerializeField] int dashFrames;
    [SerializeField] SceneSettings sceneSettings;
    [SerializeField] float dashReloadTime;
    PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        playerState = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isDashing = false;
        localMoveSpeed = moveSpeed;
        if (sceneSettings.canDashScene)
        {
            canDash = sceneSettings.canDashScene;
        }
        boxCollider = GetComponent<BoxCollider2D>();
        isAttacking = false;
        atackTime = 0.5f;
        isAttackAnimation = false;
        if (sceneSettings.isActionScean)
        {
            attackSplash.SetActive(false);
        }

        playerDamageTaking = GetComponent<PlayerDamageTaking>();

    }
    private void Update()
    {

    }

    private void FixedUpdate()
    {
        canDash = sceneSettings.canDashScene;
        canAttack = sceneSettings.isActionScean;
        if (playerState.canMove)
        {
            Vector2 finalMove = Vector2.zero;

            if (isDashing)
            {
                if (currentDash == null)
                {
                    currentDash = StartCoroutine(StartDash());
                }
                float localDistance = TryDash(playerDirection);
                finalMove += new Vector2((playerDirection.x * localDistance), (playerDirection.y * localDistance));

            }
            if (unConitiusDerection != Vector2.zero)
            {
                bool isSucces = TryMove(unConitiusDerection);
                if (isSucces)
                {
                    finalMove += WASDMoveVector(unConitiusDerection);
                }
                else
                {
                    isSucces = TryMove(new Vector2(unConitiusDerection.x, 0));
                    if (isSucces)
                    {
                        finalMove += WASDMoveVector(new Vector2(unConitiusDerection.x, 0));
                    }
                    else
                    {
                        isSucces = TryMove(new Vector2(0, unConitiusDerection.y));
                        if (isSucces)
                        {
                            finalMove += WASDMoveVector(new Vector2(0, unConitiusDerection.y));
                        }
                    }
                }
            }

            if (isAttackAnimation)
            {
                ChangeAnimationState("Attack" + SetAttackAnimationDirection(attackSplash.transform.rotation));
                if(attackPlaying == null)
                {
                    attackPlaying = StartCoroutine(PlatyAttack());
                }
                if(walkPlaying != null)
                {
                    StopCoroutine(walkPlaying);
                    playingWalk = false;
                    walkPlaying = null;
                }
               // StartCoroutine(PlayAttackSound());
            }
            else if (isDashing)
            {
                ChangeAnimationState("Dash" + SetAnimationDirectionString(playerDirection));
                if (dashPlaying == null)
                {
                    dashPlaying = StartCoroutine(PlayDashSound());
                    if (walkPlaying != null)
                    {
                        StopCoroutine(walkPlaying);
                        playingWalk = false;
                        walkPlaying = null;
                    }
                }
               
            }
            else if (unConitiusDerection != Vector2.zero)
            {
                ChangeAnimationState(("Walk" + SetAnimationDirectionString(unConitiusDerection)));
                if(walkPlaying == null)
                {
                    walkPlaying = StartCoroutine(PlayWalk());
                }
               
            }
            else
            {
                if (walkPlaying != null)
                {
                    StopCoroutine(walkPlaying);
                    playingWalk = false;
                    walkPlaying = null;
                }
                ChangeAnimationState("Idle");
            }


            //rb.MovePosition(rb.position + finalMove * Time.fixedDeltaTime);
            rb.velocity = finalMove;
        }

    }
   /* IEnumerator PlayAttackSound()
    {
        if (!playingAttack)
        {
            playingAttack = true;
            attackSound.Play();
        }
        yield return new WaitUntil();
        playingAttack = false;
        
    }*/
   /* IEnumerator PlayWalkSound()
    {
        if (!playingWalk)
        {
            playingWalk = true;
            walkSound.Play();
        }
        yield return new WaitForSeconds(walkSound.clip.length);
        playingWalk = false;

    }*/
    IEnumerator PlayDashSound()
    {
        if (playingDash == false)
        {
            playingDash=true;
            dashSound.Play();
        }
        yield return new WaitForSeconds(dashTime);
        Debug.Log("seted dash");
        playingDash = false;
        dashPlaying = null;

    }
    IEnumerator PlatyAttack()
    {
        if (playingAttack == false)
        {
            playingAttack = true;
            attackSound.Play();
        }
        yield return new WaitForSeconds(atackTime);
        Debug.Log("seted dash");
        playingAttack = false;
        attackPlaying = null;

    }
    IEnumerator PlayWalk()
    {
        if (playingWalk == false)
        {
            playingWalk = true;
            walkSound.Play();
            yield return new WaitForSeconds(0.15f);
            walkSound2.Play();
            yield return new WaitForSeconds(0.15f);
            walkSound.Play();
            yield return new WaitForSeconds(0.15f);
            walkSound.Play();
            yield return new WaitForSeconds(0.15f);
            playingWalk = false;
            walkPlaying = null;
        }
        else
        {
            yield return null;
        }
        

    }
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation, 0);
        currentAnimaton = newAnimation;
    }
    private float TryDash(Vector2 direction)
    {
        int count = rb.Cast(direction, dashFilter, dashCollisions, dashDistance * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
        {
            return dashDistance;
        }
        else
        {
            bool set = false;
            int i = 0;
            foreach (RaycastHit2D hit in dashCollisions)
            {

                if (hit.collider.gameObject.tag == "CantDash")
                {
                    if (!set)
                    {
                        set = true;
                        return dashCollisions[i].distance;
                    }

                }
                else if (hit.collider.gameObject.tag == "Damaging")
                {
                    Debug.Log("dashed trow enemy");
                    return dashCollisions[i].distance + 10f;

                }
                i++;
            }
            return 0;
        }

    }
    IEnumerator StartDash()
    {
        animationString = "Dash" + SetAnimationDirectionString(playerDirection);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        currentDash = null;
    }
    private Vector2 WASDMoveVector(Vector2 direction)
    {
        return new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }

    private bool TryMove(Vector2 direction)
    {

        int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);
        // Debug.DrawRay(rb.transform.position, direction * moveSpeed * Time.fixedDeltaTime,Color.red,5);
        /*if (count != 0)
        {
           // Debug.Log(castCollisions[0].collider.gameObject.name);
        }*/

        if (count == 0)
        {

            return true;
        }
        else
        {
            return false;
        }
    }
    void OnMove(InputValue movementValue)
    {

        movementInput = movementValue.Get<Vector2>();
        unConitiusDerection = SetDirection(movementValue.Get<Vector2>());
        if (unConitiusDerection != Vector2.zero)
        {
            playerDirection = SetDirection(movementValue.Get<Vector2>());
        }

    }
    private Vector2 SetDirection(Vector2 imputValue)
    {
        return new Vector2(Math.Sign(imputValue.x), Math.Sign(imputValue.y));

    }

    void OnDash()
    {
        if (canDash)
        {
            isDashing = true;
            canDash = false;
            StartCoroutine(DashReload());
        }


    }
    IEnumerator DashReload()
    {
        yield return new WaitForSeconds(dashReloadTime);
        canDash = true;
    }
    private string SetAnimationDirectionString(Vector2 mInput)
    {

        if (mInput.y < 0)
        {
            return "Down";
        }
        else if (mInput.y > 0)
        {
            return "Up";
        }
        else if (mInput.x > 0)
        {
            return "Right";

        }
        else
        {
            return "Left";
        }
    }

    void OnAttack()
    {
        if (playerState.canMove && canAttack)
        {
            isAttacking = true;
            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

            //Get the Screen position of the mouse
            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Get the angle between the points
            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

            //Ta Daaa
            Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            rotation *= Quaternion.Euler(0, 180f, 0);
            attackSplash.SetActive(true);
            attackSplash.transform.rotation = rotation;

            Vector3 offset = Vector3.MoveTowards(gameObject.transform.position, worldPosition, attackOffset);
            attackSplash.transform.position = offset;
            StartCoroutine(StartAttack());
            StartCoroutine(StartAttackAnimation());
        }

    }
    IEnumerator StartAttack()
    {

        yield return new WaitForSeconds(atackTime);
        attackSplash.SetActive(false);
        isAttacking = false;
    }
    IEnumerator StartAttackAnimation()
    {
        isAttackAnimation = true;
        yield return new WaitForSeconds(0.5f);
        isAttackAnimation = false;
    }
    private void MousePosition(InputValue inputValue)
    {
        mousePosition = inputValue.Get<Vector2>();
    }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    private string SetAttackAnimationDirection(Quaternion rotation)
    {
        Vector3 eulersRotation = rotation.eulerAngles;

        if (eulersRotation.z > 90 && eulersRotation.z < 270)
        {
            return "Right";
        }
        else
        {
            return "Left";
        }
    }
}
