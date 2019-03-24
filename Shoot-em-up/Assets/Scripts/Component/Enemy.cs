using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int score;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().gameObject.CompareTag("Bullet_Avatar"))
        {
            Destroy(collision.GetComponent<Collider2D>().gameObject);
            EventManager.instance.Fire(new EnemyDied(score));
            Enemy_Manager.EnemyManager.GetComponent<Enemy_Manager>().Destroy_Enemy(index);

        }

        GameObject ob = collision.GetComponent<Collider2D>().gameObject;
        /*if (ob.CompareTag("Player"))
        {
            GameStateManager.ChangeGameState(GameState.Over);
            Destroy(ob);
            Destroy(gameObject);
        }*/
    }
}
