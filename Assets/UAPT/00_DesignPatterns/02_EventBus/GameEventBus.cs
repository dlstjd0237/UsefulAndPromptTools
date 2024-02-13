using System.Collections.Generic;
using UnityEngine.Events;
public class GameEventBus : MonoSingleton<GameEventBus>
{
    private static readonly IDictionary<GameEventBusType, UnityEvent> Events = new Dictionary<GameEventBusType, UnityEvent>();

    /// <summary>
    /// �̺�Ʈ ����
    /// </summary>
    /// <param name="gameEventType">� Ÿ����?</param>
    /// <param name="listener">� �޼��带?</param>
    public static void Subscribe(GameEventBusType gameEventType, UnityAction listener)
    {
        UnityEvent thisEvent;
        if (Events.TryGetValue(gameEventType, out thisEvent)) //�̺�Ʈ ��ųʸ� �ȿ� gameEventType Ű������
                                                              //������� UnityEvent�� �ִٸ� UnityEvent�� �޾ƿ� �׼� ����
        {
            thisEvent.AddListener(listener);
        }
        else // �̺�Ʈ ��ųʸ��� gameEventType�� Ű������ ������� UnityEvent�� ���ٸ� 
        {
            thisEvent = new UnityEvent(); // ���ο� UnityEvent�� ����
            thisEvent.AddListener(listener); //�������ְ�
            Events.Add(gameEventType, thisEvent); // �̺�Ʈ ��ųʸ��� �߰�
        }
    }

    /// <summary>
    /// �̺�Ʈ ���� ����
    /// </summary>
    /// <param name="gameEventType">� Ÿ����?</param>
    /// <param name="listener">� �޼��带?</param>
    public static void UnSubscribe(GameEventBusType gameEventType, UnityAction listener)
    {
        UnityEvent thisEvent;
        if (Events.TryGetValue(gameEventType, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Ÿ�� ����
    /// </summary>
    /// <param name="gameEventType">������ Ÿ��</param>
    public static void Publish(GameEventBusType gameEventType)
    {
        UnityEvent thisEvent;
        if (Events.TryGetValue(gameEventType, out thisEvent))
        {
            thisEvent?.Invoke();
        }
    }
}
