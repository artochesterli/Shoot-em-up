using System.Collections;
using System.Collections.Generic;
using System;

public class EventManager
{
    public delegate void EventDelegate<T>(T e) where T : Event;
    private delegate void EventDelegate(Event e);

    private readonly Dictionary<Type, EventDelegate> AllDelegete = new Dictionary<Type, EventDelegate>();
    private readonly Dictionary<Delegate, EventDelegate> DelegateLookup = new Dictionary<Delegate, EventDelegate>();

    //public static EventManager instance = new EventManager();

    public void AddHandler<T>(EventDelegate<T> del) where T : Event
    {
        if (DelegateLookup.ContainsKey(del))
        {
            return;
        }

        EventDelegate InternalDel = (e)=>del((T)e);
        DelegateLookup[del] = InternalDel;

        EventDelegate tdel;
        if(AllDelegete.TryGetValue(typeof(T),out tdel))
        {
            AllDelegete[typeof(T)] = tdel += InternalDel;
        }
        else
        {
            AllDelegete[typeof(T)] = InternalDel;
        }
    }

    public void RemoveHandler<T>(EventDelegate<T> del) where T : Event
    {
        EventDelegate InternalDel;
        if(DelegateLookup.TryGetValue(del,out InternalDel))
        {
            EventDelegate tdel;
            if (AllDelegete.TryGetValue(typeof(T),out tdel))
            {
                tdel -= InternalDel;
                if (tdel == null)
                {
                    AllDelegete.Remove(typeof(T));
                }
                else
                {
                    AllDelegete[typeof(T)] = tdel;
                }
            }

            DelegateLookup.Remove(del);
        }
    }

    public void Fire(Event e)
    {
        EventDelegate del;
        if (AllDelegete.TryGetValue(e.GetType(), out del))
        {
            del.Invoke(e);
        }
    }
}
