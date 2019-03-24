using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{
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
        GameObject ob = collision.GetComponent<Collider2D>().gameObject;
        /*if (ob.CompareTag("Player"))
        {
            GameStateManager.ChangeGameState(GameState.Over);
            Destroy(ob);
            Destroy(gameObject);
        }*/
    }
}
