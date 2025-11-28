using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
	[Header("로그인인풋필드")]
	public TMP_InputField id;
	public TMP_InputField password;

	[Header("회원가입인풋필드")]
	public TMP_InputField signUpId;
	public TMP_InputField signUpName;
	public TMP_InputField signUpPS;
	public TMP_InputField signUpConfirmPS;


	[Header("회원가입UI")]
	public GameObject SignUpUI;
	public TextMeshProUGUI errorText;
	public Button cancleBtn;
	public Button signUpBtn;

	private void Start()
	{
		GameManager.Instance.uIView.login.onClick.AddListener(TryLogin);
		GameManager.Instance.uIView.signUp.onClick.AddListener(ClickSignUp);
		signUpBtn.onClick.AddListener(TrySignUp);
		GameManager.Instance.uIView.loginErrorBtn.onClick.AddListener(LoginError);
		cancleBtn.onClick.AddListener(CancleSignUp);

	}
	public void TryLogin()
	{
		foreach (UserData use in GameManager.Instance.userData)
		{
			if (use.Id == id.text && use.Password == password.text)
			{
				GameManager.Instance.accessUserData = use;
				Debug.Log("로그인 성공!");
				GameManager.Instance.uIView.LoginSuccese();
				return;

			}

		}

		GameManager.Instance.uIView.LoginError.gameObject.SetActive(true);
		GameManager.Instance.uIView.loginErrorMassage.text = "아이디와 비밀번호가 맞지않습니다.";

	}
	public void ClickSignUp()
	{
		SignUpUI.SetActive(true);
		errorText.text = string.Empty;
	}
	public void TrySignUp()
	{
		string inputId = signUpId.text;
		string inputPs = signUpPS.text;
		string inputConfirmPs = signUpConfirmPS.text;
		string inputName = signUpName.text;
		if (inputId != "")
		{
			if (inputName != "")
			{
				if (inputPs != "")
				{
					if (inputConfirmPs != "")
					{
						if (inputConfirmPs == inputPs)
						{
							UserData newUserData = new UserData(inputId, inputPs, inputName, 85000, 115000);
							GameManager.Instance.userData.Add(newUserData);
							GameManager.Instance.SaveUserData();
							SignUpUI.SetActive(false);
						}
						else
						{
							errorText.text = "ConfirmPassword와 Password가 맞지 않습니다";
						}
					}
					else
					{
						errorText.text = "ConfirmPassword를 확인해주세요";
					}
				}
				else
				{
					errorText.text = "Password를 확인해주세요";
				}
			}
			else
			{
				errorText.text = "Name을 확인해주세요";
			}
		}
		else
		{
			errorText.text = "ID를 확인해주세요";
		}
	}

	public void LoginError()
	{
		GameManager.Instance.uIView.LoginError.gameObject.SetActive(false);
		id.text = "";
		password.text = "";
	}
	public void CancleSignUp()
	{
		signUpId.text = "";
		signUpName.text = "";
		signUpPS.text = "";
		signUpConfirmPS.text = "";
		SignUpUI.SetActive(false);
	}


}
