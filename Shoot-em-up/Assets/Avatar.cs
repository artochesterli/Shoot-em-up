using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    private FSM<Avatar> _fsm;
    // Start is called before the first frame update
    void Start()
    {
        _fsm = new FSM<Avatar>(this);
        _fsm.TransitionTo<Normal>();
    }

    // Update is called once per frame
    void Update()
    {
        _fsm.Update();
    }

    private class Normal : FSM<Avatar>.State
    {
        private List<float> EnemyKillTime;
        private float timecount;

        public override void Init()
        {
            EnemyKillTime = new List<float>();
            EventManager.instance.AddHandler<EnemyDied>(OnEnemyDied);
        }

        public override void OnEnter()
        {
            timecount = 0;
            EnemyKillTime = new List<float>();
            Context.gameObject.GetComponent<Avatar_Shoot>().bullet_interval = 0.1f;
            Context.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            EventManager.instance.AddHandler<EnemyDied>(OnEnemyDied);
        }

        public override void Update()
        {
            if (EnemyKillTime.Count >= 3)
            {
                TransitionTo<Rage>();
            }
            if(EnemyKillTime.Count > 0)
            {
                if(Time.time - EnemyKillTime[0] > 3)
                {
                    EnemyKillTime.RemoveAt(0);
                }
            }
            else
            {
                timecount += Time.deltaTime;
                if (timecount > 3)
                {
                    TransitionTo<Weak>();
                }
            }
        }

        public override void OnExit()
        {
            EnemyKillTime.Clear();
            EventManager.instance.RemoveHandler<EnemyDied>(OnEnemyDied);
        }

        public override void CleanUp()
        {
            EnemyKillTime.Clear();
            EventManager.instance.RemoveHandler<EnemyDied>(OnEnemyDied);
        }

        private void OnEnemyDied(EnemyDied e)
        {
            EnemyKillTime.Add(Time.time);

        }

    }


    private class Rage : FSM<Avatar>.State
    {
        private float timecount;

        public override void OnEnter()
        {
            timecount = 0;
            Context.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            Context.gameObject.GetComponent<Avatar_Shoot>().bullet_interval = 0.05f;
        }

        public override void Update()
        {
            timecount += Time.deltaTime;
            if (timecount > 5)
            {
                TransitionTo<Normal>();
            }
        }
    }

    private class Weak : FSM<Avatar>.State
    {
        private float timecount;

        public override void OnEnter()
        {
            timecount = 0;
            Context.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            Context.gameObject.GetComponent<Avatar_Shoot>().bullet_interval = 0.2f;
        }

        public override void Update()
        {
            timecount += Time.deltaTime;
            if (timecount > 5)
            {
                TransitionTo<Normal>();
            }
        }
    }
}


