using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphView : GraphView
{
    private readonly Vector2 defaultNodeSize = new Vector2(150,200);

    public DialogueGraphView()
    {
        this.AddManipulator(new ContentDragger());//������ �巹��
        this.AddManipulator(new SelectionDragger());//���� �巡��
        this.AddManipulator(new RectangleSelector());//�簢�� ����

        AddElement(GenerateEntryPointNode()); // ��� ����
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();

        ports.ForEach(port => {
            if (startPort != port && startPort.node != port.node)
                compatiblePorts.Add(port);
        });
        return compatiblePorts;

    }

    private Port GeneratePort(DialogueNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single) //Port.Capacity.Multi �� �ϸ� ���� ��带 �ش� ��Ʈ�� ���� �� �� �ִ�.
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));//�־��� �ŰԺ����� ��Ʈ �޼ҵ��
    }
    private DialogueNode GenerateEntryPointNode()
    {
        var node = new DialogueNode
        {
            title = "START",
            GUID = Guid.NewGuid().ToString(),
            DialogueText = "ENTRYPOINT",
            EntryPoint = true
        };

        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);

        node.RefreshExpandedState();//���ΰ�ħ Ȯ�� ����
        node.RefreshPorts();//���ΰ�ħ ��Ʈ ȣ�� 

        node.SetPosition(new Rect(100, 200, 100, 150));//��� ��ġ ����
        return node;
    }

    public void CreateNode(string nodeName)
    {
        AddElement(CreateDialogueNode(nodeName));
    }

    public DialogueNode CreateDialogueNode(string nodeName)
    {
        var dialogueNode = new DialogueNode
        {
            title = nodeName,
            DialogueText = nodeName,
            GUID = Guid.NewGuid().ToString()
        };

        var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        dialogueNode.inputContainer.Add(inputPort);
        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
        dialogueNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));

        return dialogueNode;

    }
}
