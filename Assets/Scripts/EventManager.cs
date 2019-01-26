using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Event
{
    NONE,

    GAME_TRIGGER,
    GAME_SUBMIT,
    SELECT_ITEM,
    DESELECT_ITEM,

    STORY_TRIGGER,
    STORY_TEXT_NEXT,
    TEXT_ANIMATING_START,
    TEXT_ANIMATING_END,

    BRANCH_TRIGGER,
    BRANCH_SELECT_LEFT,
    BRANCH_SELECT_RIGHT,

    ACTION_TRIGGER,
    ACTION_CHOOSE_LEFT,
    ACTION_CHOOSE_RIGHT,
}

public class EventManager
{
    public delegate void EventHandler(object[] Args);

    private static Dictionary<Event, EventHandler> m_Dict = new Dictionary<Event, EventHandler>();

    public static Event CurrentEvent = Event.NONE;

    public static void Register(Event EventId, EventHandler Handler)
    {
        EventHandler CurrentHandler = null;
        if (m_Dict.TryGetValue(EventId, out CurrentHandler))
        {
            CurrentHandler += Handler;
        }
        else
        {
            m_Dict[EventId] = Handler;
        }
    }

    public static void Remove(Event EventId, EventHandler Handler)
    {
        EventHandler CurrentHandler = null;
        if (m_Dict.TryGetValue(EventId, out CurrentHandler))
        {
            CurrentHandler -= Handler;
        }
    }

    public static void Trigger(Event EventId, object[] Args)
    {
        CurrentEvent = EventId;
        EventHandler CurrentHandler = null;
        if (m_Dict.TryGetValue(EventId, out CurrentHandler))
        {
            CurrentHandler(Args);
        }
        CurrentEvent = Event.NONE;
    }
}
