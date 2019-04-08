using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class ShieldEnemy : MonoBehaviour
{

    public int HP;
    public float speed;

    private Tree<ShieldEnemy> Btree;
    private Tree<ShieldEnemy> Btree2;
    
    private GameObject Player;

    private const int MaxHP = 10;
    
    private const float CloseDistance = 5;
    private const float DetectDistance = 10;
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Shield").GetComponent<BoxCollider2D>().enabled = false;
        transform.Find("Shield").GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<HoldShield>().enabled = false;
        Player = GameObject.Find("Avatar").gameObject;
        Btree = new Tree<ShieldEnemy>(new Selector<ShieldEnemy>(
            new Sequence<ShieldEnemy>(
                new IsInDetectRange(),
                new Not<ShieldEnemy>
                (new Sequence<ShieldEnemy>(
                    new IsInRetreatRange(),
                    new Retreat()
                    )),
                
                new March()
            ),
            new Idle()


        ));

        Btree2 = new Tree<ShieldEnemy>(new Selector<ShieldEnemy>(
           new Sequence<ShieldEnemy>(
               new IsInDanger(),
               new Shield()
           ),
           new ShieldIdle()
       ));
    }

    // Update is called once per frame
    void Update()
    {
        Btree.Update(this);
        Btree2.Update(this);
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void MoveAwayPLayer()
    {
        Vector3 direction = (transform.position-Player.transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private class Retreat : Node<ShieldEnemy>
    {
        public override bool Update(ShieldEnemy enemy)
        {
            enemy.MoveAwayPLayer();
            return true;
        }
    }

    private class March : Node<ShieldEnemy>
    {
        public override bool Update(ShieldEnemy enemy)
        {
            enemy.MoveTowardsPlayer();
            return true;
        }
    }

    private class IsInRetreatRange : Node<ShieldEnemy>
    {
        public override bool Update(ShieldEnemy enemy)
        {
            return (enemy.transform.position - enemy.Player.transform.position).magnitude <= CloseDistance;
        }
    }

    private class IsInDetectRange : Node<ShieldEnemy>
    {
        public override bool Update(ShieldEnemy enemy)
        {
            return (enemy.transform.position - enemy.Player.transform.position).magnitude <= DetectDistance;
        }
    }

    private class IsInDanger : Node<ShieldEnemy>
    {
        public override bool Update(ShieldEnemy enemy)
        {
            return enemy.HP <= MaxHP * 0.3f;
        }
    }

    private class Shield : Node<ShieldEnemy>
    {
        public override bool Update(ShieldEnemy enemy)
        {
            enemy.transform.Find("Shield").GetComponent<BoxCollider2D>().enabled = true;
            enemy.transform.Find("Shield").GetComponent<SpriteRenderer>().enabled = true;
            enemy.GetComponent<HoldShield>().enabled = true;
            return true;
        }
    }

    private class Idle : Node<ShieldEnemy>
    {
        public override bool Update(ShieldEnemy enemy)
        {
            enemy.GetComponent<Emit_Bullet>().enabled = false;
            return true;
        }
    }

    private class ShieldIdle : Node<ShieldEnemy>
    {
        public override bool Update(ShieldEnemy enemy)
        {
            enemy.transform.Find("Shield").GetComponent<BoxCollider2D>().enabled = false;
            enemy.transform.Find("Shield").GetComponent<SpriteRenderer>().enabled = false;
            enemy.GetComponent<HoldShield>().enabled = false;
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
