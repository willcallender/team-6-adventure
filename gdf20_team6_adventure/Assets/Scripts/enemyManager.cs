using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.UI;

public class enemyManager : MonoBehaviour {
    // speed multiplier
    // public float speed;
    // the x and y component of translation
    // float x = 0, y = 0;
    // the values of x and y from the previous frame
    // float px = 0, py = 0;
    // the current cardinal direction, 0 = up, 1 = right, 2 = down, 3 = left
    int facing = 0;
    Rigidbody2D rb;
    // the animation component to control
    Animator anim;
    public Sprite downSprite;
    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite attackUpSprite;
    public Sprite attackDownSprite;
    public Sprite attackLeftSprite;
    public Sprite attackRightSprite;
    public int opponentVariant;
    string animPrefaceStr;
    public float maxHealth;
    public float health;
    public float timeUntilHealthRegen;
    public float hpRegenPerSecond;
    Coroutine regenTimerCoroutine;
    //public healthBarManager healthBar;
    public bool inCombat;
    playerManager playerManagerObject;
    GameObject playerObject;
    GameObject navTarget;
    public bool confused = false;
    // Start is called before the first frame update
    NavMeshAgent agent;
    Coroutine navCoroutine, confuseCountdownCoroutine, attackCoroutine;
    public float meleeRange;
    public float attackDamage;
    public float attackSpeed;
    bool attacking = false;
    bool takingDamage;
    public float damageTime = 0.25f;
    bool iced = false;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animPrefaceStr = "opponent_v" + opponentVariant.ToString();
        facing = 2;
        idle(2);
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerManagerObject = playerObject.GetComponent<playerManager>();
        navTarget = playerObject;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        navCoroutine = StartCoroutine(navTargetRoutine());
        attackCoroutine = StartCoroutine(attackRoutine());
    }
    
    // Update is called once per frame
    void Update() {
        if (!attacking) {
            updateAnim();
        }
    }

    void updateAnim() { 
        Vector2 vel = new Vector2(agent.velocity.x, agent.velocity.y);
        if (Mathf.Abs(vel.x) > Mathf.Abs(vel.y)) {
            if (vel.x > 0) {
                facing = 1;
            } else if (vel.x < 0) {
                facing = 3;
            }
        } else if (Mathf.Abs(vel.x) < Mathf.Abs(vel.y)) {
            if (vel.y > 0) {
                facing = 0;
            } else if (vel.y < 0) {
                facing = 2;
            }
        } else {
            idle(facing);
            return;
        }
        switch (facing) {
            case 0:
                up();
                break;
            case 1:
                right();
                break;
            case 2:
                down();
                break;
            case 3:
                left();
                break;
        }
    }

    void idle(int d) {
        anim.enabled = false;
        Sprite idleSprite;
        if (d == 0) {
            idleSprite = upSprite;
        } else if (d == 1) {
            idleSprite = rightSprite;
        } else if (d == 2) {
            idleSprite = downSprite;
        } else {
            idleSprite = leftSprite;
        }
        spriteRenderer.sprite = idleSprite;
    }

    void up() {
        anim.enabled = true;
        anim.Play(animPrefaceStr + "_run_up");
        facing = 0;
    }

    void right() {
        anim.enabled = true;
        anim.Play(animPrefaceStr + "_run_right");
        facing = 1;
    }

    void down() {
        anim.enabled = true;
        anim.Play(animPrefaceStr + "_run_down");
        facing = 2;
    }

    void left() {
        anim.enabled = true;
        anim.Play(animPrefaceStr + "_run_left");
        facing = 3;
    }

    public void damage(float pts) {
        if (takingDamage) {
            return;
        } else {
            takingDamage = true;
        }
        StartCoroutine(damageCountdown());
        changeHealth(-pts);
        if (health<=0) {
            killSelf();
        } else {
            // There's definitely some if statement that will only try to stop the coroutine if it's been run before but I'm too lazy for that.
            // I guess we'd better hope this never generates an actual error
            try {
                StopCoroutine(regenTimerCoroutine);
            } catch {
                // Ha! Jokes on you Unity! I'm just gonna completely ignore any errors that happen here
            }
            regenTimerCoroutine = StartCoroutine(regenTimer());
        }
    }

    IEnumerator damageCountdown() {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(damageTime);
        spriteRenderer.color = originalColor;
        takingDamage = false;
    }

    void setHealth(float h) {
        health = h;
    }

    void changeHealth(float h) {
        setHealth(health + h);
    }

    public void killSelf() {
        // insert code to play death animation
        Destroy(gameObject);
    }

    IEnumerator regenTimer() {
        float time = timeUntilHealthRegen;
        yield return new WaitForSeconds(time);
        float regen = hpRegenPerSecond;
        while (health < maxHealth) {
            changeHealth(regen * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        if (health > maxHealth) {
            setHealth(maxHealth);
        }
    }

    IEnumerator confuseCountdown(float timer) {
        yield return new WaitForSeconds(timer);
        StopCoroutine(navCoroutine);
        confused = false;
        navTarget = playerObject;
        navCoroutine = StartCoroutine(navTargetRoutine());
    }

    // called by other scripts to confuse the AI and target it to a non player target
    public void confuse(GameObject newTarget, float confuseForSeconds) {
        navTarget =  newTarget;
        confused = true;
        StopCoroutine(navCoroutine);

        // Little does Unity know, I'm gonna play the ultimate prank on the compiler. The classic completely ignoring exceptions trick🎃
        try {
            StopCoroutine(confuseCountdownCoroutine);
        }
        catch {}

        confuseCountdownCoroutine = StartCoroutine(confuseCountdown(confuseForSeconds));
        navCoroutine = StartCoroutine(navTargetRoutine());
    }

    IEnumerator navTargetRoutine() {
        while(true) {
            agent.SetDestination(navTarget.transform.position);
            yield return new WaitForSeconds(0.25f);
        }
    }

    float getFacingAngle(Vector2 target) {
        return 0f;
    }

    IEnumerator attackRoutine() {
        while (true) {
            bool attackedThisUpdate = false;
            if (!confused) {
                Vector2 opponentPos = gameObject.transform.position;
                Vector2 targetPos = navTarget.transform.position;
                float dist = (opponentPos - targetPos).magnitude;
                if (dist < meleeRange) {
                    attacking = true;
                    playerManagerObject.damage(attackDamage);
                    playerManagerObject.resetCombatCooldown();
                    anim.enabled = false;

                    Vector2 playerRelative = playerObject.transform.position - gameObject.transform.position;

                    attackSpriteUpdate(playerRelative);

                    attackedThisUpdate = true;
                    yield return new WaitForSeconds(attackSpeed / 2);
                    attacking = false;
                    yield return new WaitForSeconds(attackSpeed / 2);
                }
            }
            if (!attackedThisUpdate) {
                yield return new WaitForSeconds(attackSpeed);
            }
        }
    }

    void attackSpriteUpdate(Vector2 playerRelative) {
        Sprite attackSprite;

        if (Mathf.Abs(playerRelative.x) > Mathf.Abs(playerRelative.y)) {
            if (playerRelative.x > 0) {
                facing = 1;
            } else if (playerRelative.x < 0) {
                facing = 3;
            }
        } else if (Mathf.Abs(playerRelative.x) < Mathf.Abs(playerRelative.y)) {
            if (playerRelative.y > 0) {
                facing = 0;
            } else if (playerRelative.y < 0) {
                facing = 2;
            }
        }
        
        switch (facing) {
            case 0:
                attackSprite = attackUpSprite;
                break;
            case 1:
                attackSprite = attackRightSprite;
                break;
            case 2:
                attackSprite = attackDownSprite;
                break;
            case 3:
                attackSprite = attackLeftSprite;
                break;
            default:
                attackSprite = attackUpSprite;
                break;
        }

        spriteRenderer.sprite = attackSprite;
    }

    public void ice(float duration) {
        if (!iced) {
            StartCoroutine(iceRoutine(duration));
        }
    }

    IEnumerator iceRoutine(float d) {
        iced = true;
        float originalSpeed = agent.speed;
        agent.speed /= 2;
        float originalAttackSpeed = attackSpeed;
        attackSpeed *= 2;
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = new Color(0, 0, 153);
        yield return new WaitForSeconds(d);
        agent.speed = originalSpeed;
        spriteRenderer.color = originalColor;
        attackSpeed = originalAttackSpeed;
        iced = false;
    }
}