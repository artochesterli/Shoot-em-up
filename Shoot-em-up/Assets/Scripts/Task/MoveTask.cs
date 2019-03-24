using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTask : Task
{

    private readonly GameObject _obj;
    private readonly Vector3 _target;
    private readonly float _duration;

    private Vector3 _startPos;
    private float _startTime;

    public MoveTask(GameObject o, Vector3 target, float duration)
    {
        _obj = o;
        _target = target;
        _duration = duration;
    }

    protected override void Init()
    {
        _startPos = _obj.transform.position;
        _startTime = Time.time;
    }

    internal override void TaskUpdate()
    {
        float t = (Time.time - _startTime) / _duration;
        if (t > 1) t = 1;
        _obj.transform.position = Vector3.Lerp(_startPos, _target, t);
        if (t >= 1)
        {
            SetStatus(TaskStatus.Success);
        }
    }
}
