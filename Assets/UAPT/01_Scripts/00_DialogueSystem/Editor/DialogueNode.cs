using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DialogueNode : Node
{
    public string GUID;//������ �����ϱ� ���� ���̵�
    public string DialogueText; //��ȭ ����
    public bool EntryPoint = false; //������ 
}
