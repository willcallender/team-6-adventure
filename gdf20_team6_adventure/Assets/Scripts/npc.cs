using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.UI;

public class npc : MonoBehaviour {
    int facing = 0;
    Rigidbody2D rb;
    // the animation component to control
    Animator anim;
    public Sprite downSprite;
    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    string animPrefaceStr;
    Vector2 navTarget;
    NavMeshAgent agent;
    Coroutine navCoroutine;
    SpriteRenderer spriteRenderer;
    public float walkRange;
    public float idleTime;
    public string elfVariant;
    bool possiblyStuck = false;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animPrefaceStr = "elf_" + elfVariant;
        facing = 2;
        idle(2);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        navTarget = getRandomTarget();
        navCoroutine = StartCoroutine(navTargetRoutine());
        //attackCoroutine = StartCoroutine(attackRoutine());
    }
    
    // Update is called once per frame
    void Update() {
        updateAnim();
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

    IEnumerator navTargetRoutine() {
        while(true) {
            agent.isStopped = false;
            agent.SetDestination(navTarget);
            yield return new WaitUntil(isAtTarget);
            agent.isStopped = true;
            navTarget = getRandomTarget();
            yield return new WaitForSeconds(idleTime);
        }
    }

    bool isAtTarget() {
        if (possiblyStuck) {
            return true;
        }
        if (agent.remainingDistance < 0.1f) {
            return true;
        }
        return false;
    }

    Vector2 getRandomTarget() {
        bool looking = true;
        Vector2 target = new Vector2();
        Random.InitState(System.DateTime.Now.Millisecond);
        while (looking) {
            float x, y;
            x = Random.Range(walkRange / 4, walkRange) * randomPlusOrMinus();
            y = Random.Range(walkRange / 4, walkRange) * randomPlusOrMinus();
            target = new Vector2(x + transform.position.x, y + transform.position.y);

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(target, path);
            if (path.status == NavMeshPathStatus.PathComplete) {
                looking = false;
            }
        }
        print(target);
        return target;
    }

    float randomPlusOrMinus() {
        if (Random.value > 0.5f) {
            return 1;
        }
        return -1;
    }
}