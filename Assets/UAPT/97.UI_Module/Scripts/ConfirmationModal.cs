using System;
using UnityEngine;
using UnityEngine.UIElements; //UIToolkit ����
public class ConfirmationModal : MonoBehaviour
{
    //UI�� ����� �ִ� UIDocument
    private UIDocument _doc;
    //Ŭ������ �����ߴ� ������ UI
    private VisualElement _confirmationModal;
    //Ÿ��Ʋ ���̺�
    private Label _titleLabel;
    //���� ���̺�
    private Label _descriptionLabel;
    //��� ��ư
    private Button _cancelBtn;
    //Ȯ�� ��ư
    private Button _checkBtn;
    //Ŭ�� �̺�Ʈ
    private Action _currentCheckBtnClickEvent;

    private void Awake()
    {
        //��ũ��Ʈ�� �پ��ִ� ������Ʈ���� UIDocument�� ��������
        _doc = gameObject.GetComponent<UIDocument>();

        //UI���� ���� ���� �����ϴ� UI VisualElement�� ��������
        VisualElement root = _doc.rootVisualElement;

        //<> �ȿ��� ������ UI�� Ÿ��()�ȿ��� ������ UI�� �̸�


        //rootUI �ȿ� �ִ� confirmation_modal_root_visual_contain-box ��� �̸��� VisualElement������ UI�� _titleLabel�� �־��ش�.
        _confirmationModal = root.Q<VisualElement>("confirmation_modal_root_visual_contain-box");

        //rootUI �ȿ� �ִ� confirmation_modal_title-label��� �̸��� Label������ UI�� _titleLabel�� �־��ش�.
        _titleLabel = root.Q<Label>("confirmation_modal_title-label");
        //rootUI �ȿ� �ִ� confirmation_modal_description-label��� �̸��� Label������ UI�� _descriptionLabel�� �־��ش�.
        _descriptionLabel = root.Q<Label>("confirmation_modal_description-label");

        //rootUI �ȿ� �ִ� confirmation_modal_cancel-btn��� �̸��� Button������ UI�� _cancelBtn �־��ش�.
        _cancelBtn = root.Q<Button>("confirmation_modal_cancel-btn");
        //rootUI �ȿ� �ִ� confirmation_modal_check-btn��� �̸��� Button������ UI�� _checkBtn �־��ش�.
        _checkBtn = root.Q<Button>("confirmation_modal_check-btn");

        //��� ��ư�� Ŭ������ ��
        _cancelBtn.RegisterCallback<ClickEvent>(evt => ToggleUI()); //UI����
        //Ȯ�� ��ư�� ������ ��
        _checkBtn.RegisterCallback<ClickEvent>(evt =>
        {
            if (_currentCheckBtnClickEvent != null)
            {
                //Ȯ�� �̺�Ʈ ���� 
                _currentCheckBtnClickEvent?.Invoke();
                _currentCheckBtnClickEvent = null;
            }
            //UI����
            ToggleUI();
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ShowConfirmationModal("������ Ÿ��Ʋ", "�̰� ������ �������� �ƹ� ���� �����ϴ�.", () => Debug.Log("Ȯ�� ����"));
    }

    /// <summary>
    /// Ȯ�� ��ȭâ ȭ�鿡 ���̱�
    /// </summary>
    /// <param name="title">Ÿ��Ʋ�� ���� ����</param>
    /// <param name="description">Ȯ�� ��ȭâ ����</param>
    /// <returns></returns>
    public void ShowConfirmationModal(string title, string description, Action checkBtnCliekEvent)
    {
        //UI Ű��
        ToggleUI();

        //title�� text�� �ֱ�
        _titleLabel.text = title;
        //_descriptionLabel text�� �ֱ�
        _descriptionLabel.text = description;
        //cliekEvent ������Ʈ
        _currentCheckBtnClickEvent = checkBtnCliekEvent; 

    }

    private void ToggleUI()
    {
        //Style Class�� �پ��ִٸ� ���� �Ⱥپ� �ִٸ� ����
        _confirmationModal.ToggleInClassList("on"); 
    }
}
