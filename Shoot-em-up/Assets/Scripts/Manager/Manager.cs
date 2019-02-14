using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Manager<T>
{
    protected List<T> Managed_Objects=new List<T>();
    protected abstract T CreateObject();
    protected virtual void DestroyObject(T obj) { }
    public T Create()
    {
        var obj = CreateObject();
        Managed_Objects.Add(obj);
        return obj;
    }

    public void Destroy(T obj)
    {
        DestroyObject(obj);
        Managed_Objects.Remove(obj);
    }
    public T Find(Predicate<T> predicate)
    {
        return Managed_Objects.Find(predicate);
    }

    public List<T> FindAll(Predicate<T> predicate)
    {
        return Managed_Objects.FindAll(predicate);
    }
}
