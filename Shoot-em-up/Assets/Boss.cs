using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int HP;

    public float radius_high;
    public float radius_low;

    private bool SecondPhaseTrigger;
    private bool ThirdPhaseTrigger;

    private readonly ParallelTasks _tasks = new ParallelTasks();

    private void Start()
    {
        HP = 100;
        Add_MoveTask();
        _tasks.Add(new GenerateEnemyTask(gameObject, 1, 1, 3));
        _tasks.Add(new ShootBulletTask(gameObject, false, 2));
    }

    private void Update()
    {
        if (HP < 70&&!SecondPhaseTrigger)
        {
            SecondPhaseTrigger = true;
            Task generate = _tasks.GetTask<GenerateEnemyTask>();
            generate.Status = Task.TaskStatus.Success;
            Task bullet = _tasks.GetTask<ShootBulletTask>();
            bullet.Status = Task.TaskStatus.Success;
            _tasks.Add(new GenerateEnemyTask(gameObject, 2, 1, 3));
            _tasks.Add(new ShootBulletTask(gameObject, false, 2));

        }

        if(HP<30 && !ThirdPhaseTrigger)
        {
            ThirdPhaseTrigger = true;
            Task generate = _tasks.GetTask<GenerateEnemyTask>();
            generate.Status = Task.TaskStatus.Success;
            Task bullet = _tasks.GetTask<ShootBulletTask>();
            bullet.Status = Task.TaskStatus.Success;
            _tasks.Add(new GenerateEnemyTask(gameObject, 3, 2, 3));
            _tasks.Add(new ShootBulletTask(gameObject, true, 2));
        }

        if (!_tasks.HasTask<MoveTask>())
        {
            Add_MoveTask();
        }
        // If there's no more tasks queue up a new move sequence
        _tasks.TaskUpdate();
    }



    private void Add_MoveTask()
    {
        Vector3 offset = Random.insideUnitCircle * (radius_high - radius_low);
        offset.Normalize();
        offset += offset * radius_low;
        float bound_y = Camera.main.orthographicSize;
        float bound_x = Camera.main.orthographicSize * Camera.main.pixelWidth / Camera.main.pixelHeight;

        if (transform.position.x + offset.x > bound_x)
        {
            offset.x = bound_x - transform.position.x;
        }
        else if (transform.position.x + offset.x < -bound_x)
        {
            offset.x = -bound_x - transform.position.x;
        }
        if (transform.position.y + offset.y > bound_y)
        {
            offset.y = bound_y - transform.position.y;
        }
        else if (transform.position.y + offset.y < -bound_y)
        {
            offset.y = -bound_y - transform.position.y;
        }
        _tasks.Add(new MoveTask(gameObject, transform.position + offset, 1));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().gameObject.CompareTag("Bullet_Avatar"))
        {
            Destroy(collision.GetComponent<Collider2D>().gameObject);
            HP--;
            if (HP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
