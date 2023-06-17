using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehevior : MonoBehaviour
{
    Animator animator;
    private bool isTriggered;
    private string enemyName = "Flame";
    private Rigidbody2D rb;
    private CircleCollider2D hitBox;
    private bool isFollowing;
    public ContactFilter2D contactFilter;
    List<RaycastHit2D> hitBoxHit = new List<RaycastHit2D>();
    Coroutine followCorutine;
    private SpriteRenderer spriteRenderer;
    private bool isTakingdamage;
    Coroutine dashCorutine;
    GameObject damageDealer;
    Coroutine takeDamageCorutine;
    private bool canTakeDamage;
    private bool damagedStateInvoked;
    bool apperAnimationPlayed;

    [SerializeField] float triggerRadius;
    [SerializeField] GameObject Player;
    [SerializeField] float dashTime;
    [SerializeField] float dashDistance;
    [SerializeField] float damageOffset;
    [SerializeField] int health;

    [Header("Dash")]
    [SerializeField] float collisionOffset;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    ContactFilter2D movementFilter;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitBox = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        IdleState();
        isTriggered = false;
        isFollowing = false;
        isTakingdamage = false;
        canTakeDamage = true;
        damagedStateInvoked = false;
        apperAnimationPlayed = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = 3;
        StartCoroutine(PlayAppearAnimation());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
       // if(apperAnimationPlayed)
        {
           
            if (!isTakingdamage)
            {
                if (!isTriggered)
                {
                    if (Vector3.Distance(Player.transform.position, gameObject.transform.position) < triggerRadius)
                    {
                        TriggeredState();
                        

                    }
                }
                else
                {
                    FollowState();
                }
            }
            else
            {
                TakingdamageState();
            }
        }


    }
    void IdleState()
    {
        animator.Play("FlameIdle");

    }
    void TriggeredState()
    {

        StartCoroutine(TriggerCorutine());

    }
    IEnumerator PlayAppearAnimation()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color targetColor = sprite.color;
        targetColor.a = 0;
        sprite.color = targetColor;
        while (sprite.color.a <= 1)
        {
            targetColor.a += 0.05f;
            sprite.color = targetColor;
            yield return new WaitForSeconds(0.1f);
        }
        apperAnimationPlayed = true;
    }
    IEnumerator TriggerCorutine()
    {
        animator.Play(enemyName + "Triggered");
        yield return new WaitForSeconds(1f);
        isTriggered = true;
    }
    void FollowState()
    {
        animator.Play("FlameDash");
        isFollowing = true;
        /*followCorutine = StartCoroutine(FollowCorutine());*/
        DashTowards(Player, dashDistance, false);

    }
    void DashTowards(GameObject dashTarget, float distance, bool backWards)
    {
        float direcion = Direction(gameObject.transform.position, Player.transform.position).x;

        if (direcion > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        Vector2 dashDirection = Direction(rb.position, dashTarget.transform.position);
        if (backWards)
        {
            dashDirection *= -1;
        }
        float localDash = TryDash(dashDirection, distance);
        if(localDash == 0)
        {
            localDash = TryDash(new Vector2(dashDirection.x, 0f), distance);
            if (localDash == 0)
            {
                localDash = TryDash(new Vector2(0f, dashDirection.y), distance);
            }
        }

        if (localDash > 0)
        {
            Vector3 deltaPosition = Direction(rb.position, dashTarget.transform.position) * localDash;
            if (backWards)
            {
                deltaPosition *= -1;
            }
            rb.velocity = deltaPosition;
        }
    }
    /* IEnumerator FollowCorutine()
     {
         float direcion = Direction(gameObject.transform.position, Player.transform.position).x;

         if (direcion > 0)
         {
             spriteRenderer.flipX = false;
         }
         else
         {
             spriteRenderer.flipX = true;
         }
         while (isFollowing)
         {
             Vector2 dashDirection = Direction(gameObject.transform.position, Player.gameObject.transform.position);
             float localDash = TryDash(dashDirection, dashDistance);
             if (localDash > 0)
             {
               dashCorutine = StartCoroutine(Dash(Vector3.MoveTowards(gameObject.transform.position,
               Player.gameObject.transform.position, localDash), dashTime));
             }
             yield return new WaitForSeconds(1f);
         }
     }*/
    /* IEnumerator Dash(Vector3 position, float time)
     {

         var currentPos = gameObject.transform.position;
         var t = 0f;
         while (t < 1)
         {
             t += Time.deltaTime / time;
             transform.position = Vector3.Lerp(currentPos, position, t);
             yield return null;
         }
     }*/
    private Vector2 Direction(Vector3 from, Vector3 to)
    {
        Vector2 result = to - from;
        return result.normalized;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        {
            if (collision.gameObject.tag == "PlayerAttack")
            {
                if (canTakeDamage)
                {
                    isTakingdamage = true;
                    health -= 1;
                    if (health == 0)
                    {
                        StopAllCoroutines();
                        gameObject.SetActive(false);
                    }
                    damageDealer = collision.gameObject;
                }
            }
        }
        
    }
    void TakingdamageState()
    {
        animator.Play("FlameTookDamage");
        if (!damagedStateInvoked)
        {
            StopAllCoroutines();
            StartCoroutine(TakeDamageCorutine());
        }
        DashTowards(damageDealer, damageOffset, true);
        damagedStateInvoked = true;
    }
    IEnumerator TakeDamageCorutine()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1f);
        isTakingdamage = false;
        canTakeDamage = true;
        damagedStateInvoked = false;
    }
    private float TryDash(Vector2 direction, float dashDistance)
    {
        int count = hitBox.Cast(direction, movementFilter, castCollisions, dashDistance * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
        {
            return dashDistance;
        }
        else if (castCollisions[0].distance > 0.1)
        {

                return castCollisions[0].distance;

            
        }
        else
        {
            return 0f;
        }
    }
}
