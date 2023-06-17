using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamageTaking : MonoBehaviour
{
    [SerializeField] int healthPoints;
    [SerializeField] float damageOffset;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Rigidbody2D rb;
    [SerializeField] float castSize;
    List<RaycastHit2D> hits = new List<RaycastHit2D>();
    public ContactFilter2D contactFilter;
    Animator animator;
    BoxCollider2D boxCollider;
    GameObject damageDealer;
    public bool isTakingdamage;
    bool canTakeDamage;
    bool damagedStateInvoked;
    [SerializeField] float collisionOffset;
    PlayerMovement playerMovement;
    [SerializeField] Image healthBar;
    [SerializeField] GameObject healthBarCanvas;
    [SerializeField] SceneSettings sceneSettings;
    Loader.Scene m_scene;
    int hStart;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
       
        animator = GetComponent<Animator>();
        isTakingdamage = false;
        canTakeDamage = true;
        damagedStateInvoked = false;
        boxCollider = GetComponent<BoxCollider2D>();
        playerMovement = GetComponent<PlayerMovement>();
        healthBarCanvas.SetActive(false);
        hStart = healthPoints;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (sceneSettings.canDashScene)
        {
            healthBarCanvas.SetActive(true);
        }
        bool isDashing = playerMovement.isDashing;
        if (canTakeDamage && !isDashing)
        {
            if (isTakingdamage)
            {
                TakingdamageState();
            }
            else
            {
                rb.Cast(Vector2.up, contactFilter, hits, castSize);
                if (hits.Count > 0)
                {
                    bool set = false;
                    int i = 0;
                    foreach (RaycastHit2D hit in hits)
                    {
                        
                        if (hit.collider.gameObject.tag == "Damaging")
                        {
                            if (!set)
                            {
                                damageDealer = hits[i].collider.gameObject;
                                set = true;
                            }
                           
                        }
                        i++;
                    }
                    if (set)
                    {
                        Debug.Log(damageDealer.name + " нанёс урон");
                        if (damageDealer != null)
                        {
                            isTakingdamage = true;
                        }

                        healthPoints -= 1;
                        if(healthPoints == 0)
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        }
                        UnpateHearthBar(healthPoints);
                    }
                }
            }
        }

    }
    void UnpateHearthBar(int health)
    {
        float fill = (float)health / hStart;
        healthBar.fillAmount = fill;
        Debug.Log(healthBar.fillAmount);
    }
    void TakingdamageState()
    {

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
        StartCoroutine(DamageAnimation());
        canTakeDamage = false;
        yield return new WaitForSeconds(1f);
        isTakingdamage = false;
        canTakeDamage = true;
        damagedStateInvoked = false;
    }
    IEnumerator DamageAnimation()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color color = Color.white;
        Color colorA = color;
        colorA.a = 0;
        for (int i = 0; i < 5; i++)
        {
            if (i % 2 == 0)
            {
                sprite.color = colorA;
            }
            else
            {
                sprite.color = color;
            }
            yield return new WaitForSeconds(0.1f);
        }
        sprite.color = Color.white;
    }
    private float TryDash(Vector2 direction, float dashDistance)
    {
        int count = boxCollider.Cast(direction, contactFilter, castCollisions, dashDistance * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
        {
            return dashDistance;
        }
        else
        {
            return castCollisions[0].distance;
        }
    }
    void DashTowards(GameObject dashTarget, float distance, bool backWards)
    {
        Vector2 dashDirection = Direction(rb.position, dashTarget.transform.position);
        if (backWards)
        {
            dashDirection *= -1;
        }
        float localDash = TryDash(dashDirection, distance);

        if (localDash > 0)
        {
            Vector3 deltaPosition = Direction(rb.position, dashTarget.transform.position) * localDash;
            if (backWards)
            {
                deltaPosition *= -1;
            }
            rb.MovePosition(rb.transform.position + deltaPosition);
        }
    }
    private Vector2 Direction(Vector3 from, Vector3 to)
    {
        Vector2 result = to - from;
        return result.normalized;

    }
}
