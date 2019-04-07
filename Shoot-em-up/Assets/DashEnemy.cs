using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class DashEnemy : MonoBehaviour
{
    public int HP;
    public float speed;
    public float DashSpeed;

    private Tree<DashEnemy> Btree;

    private GameObject Player;
    private float DashIntervalTimeCount;
    private bool Dashing;


    private const int MaxHP = 10;
    private const float DashDistance = 6;
    private const float DetectDistance = 10;
    private const float DashInterval = 1.5f;
    private const float DashInitDelay = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Avatar").gameObject;
        Btree = new Tree<DashEnemy>(new Selector<DashEnemy>(
            new IsDashing(),
            new Sequence<DashEnemy>(
                new IsInDashRange(),
                new Dash()
            ),
            new Sequence<DashEnemy>(
                new IsInDetectRange(),
                new Attack()
            ),
            new Idle()


        ));
    }

    // Update is called once per frame
    void Update()
    {
        Btree.Update(this);
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void DoDash()
    {
        MoveTowardsPlayer();
        DashIntervalTimeCount += Time.deltaTime;
        if (DashIntervalTimeCount >= DashInterval)
        {
            DashIntervalTimeCount = 0;
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            StartCoroutine(PerformDash(direction));
        }
    }

    private IEnumerator PerformDash(Vector3 Direction)
    {
        Dashing = true;
        yield return new WaitForSeconds(DashInitDelay);
        float Dis = 0;
        while (Dis < DashDistance)
        {
            Dis += DashSpeed * Time.deltaTime;
            transform.position += Direction * DashSpeed * Time.deltaTime;
            yield return null;

        }
        Dashing = false;
    }

    private class Attack : Node<DashEnemy>
    {
        public override bool Update(DashEnemy enemy)
        {
            enemy.MoveTowardsPlayer();
            return true;
        }
    }

    private class Dash : Node<DashEnemy>
    {
        public override bool Update(DashEnemy enemy)
        {
            enemy.DoDash();
            return true;
        }
    }

    private class IsDashing : Node<DashEnemy>
    {
        public override bool Update(DashEnemy enemy)
        {
            return enemy.Dashing;
        }
    }

    private class IsInDetectRange : Node<DashEnemy>
    {
        public override bool Update(DashEnemy enemy)
        {
            return (enemy.transform.position - enemy.Player.transform.position).magnitude <= DetectDistance;
        }
    }

    private class IsInDashRange : Node<DashEnemy>
    {
        public override bool Update(DashEnemy enemy)
        {
            if((enemy.transform.position - enemy.Player.transform.position).magnitude <= DashDistance)
            {
                return true;
            }
            else
            {
                enemy.DashIntervalTimeCount = 0;
                return false;
            }
        }
    }

    private class IsInDanger : Node<DashEnemy>
    {
        public override bool Update(DashEnemy enemy)
        {

            return enemy.HP <= MaxHP * 0.3f;
        }
    }

    private class Idle : Node<DashEnemy>
    {
        public override bool Update(DashEnemy enemy)
        {
            return true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().gameObject.CompareTag("Bullet_Avatar"))
        {
            HP--;
            Destroy(collision.GetComponent<Collider2D>().gameObject);
        }
    }

}
