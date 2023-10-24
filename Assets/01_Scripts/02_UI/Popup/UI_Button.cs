using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    enum Buttons
    {
        Login_btn
    }
    enum Texts
    {
        LoginBtn_txt,
        Login_txt,
    }
    enum Images
    {
        Login_img
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetText((int)Texts.Login_txt).text = $"Press Q to Login";
        GetText((int)Texts.LoginBtn_txt).text = "Login";

        GetButton((int)Buttons.Login_btn).gameObject.AddUIEvent(OnButtonClicked);
    }

    public void OnButtonClicked(PointerEventData pEventData)
    {
        Managers.Scene.LoadScene(Define.Scene.Game);
    }
}
