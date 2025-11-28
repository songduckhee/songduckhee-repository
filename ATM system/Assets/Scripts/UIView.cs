using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
	public TextMeshProUGUI balance;
	public TextMeshProUGUI cash;
	public TextMeshProUGUI username;
	UserData userData;

	public TextMeshProUGUI loginErrorMassage;

	[Header("버튼")]
	public Button deposit;
	public Button withdrawal;
	public Button login;
	public Button signUp;
	public Button loginErrorBtn;
	public Button transfer;

	[Header("게임오브젝트")]
	public GameObject depositLayoutGroup;
	public GameObject withdrawalLayoutGroup;
	public GameObject popupLogin;
	public GameObject popupBank;
	public GameObject transferLayoutGroup;

	public GameObject LoginError;



	private void Start()
	{
		ShowLoginUI();

		deposit.onClick.AddListener(ShowDesposit);
		withdrawal.onClick.AddListener(ShowWithdrawal);
		transfer.onClick.AddListener(ShowTransfer);
	}

	public void Refresh()
	{
		userData = GameManager.Instance.accessUserData;
		balance.text = string.Format("{0:N0}", GameManager.Instance.accessUserData.BankBalance);
		cash.text = string.Format("{0:N0}", GameManager.Instance.accessUserData.Cash);
		username.text = GameManager.Instance.accessUserData.UserName;
	}

	public void ShowDesposit()
	{
		deposit.gameObject.SetActive(false);
		withdrawal.gameObject.SetActive(false);
		transfer.gameObject.SetActive(false);
		depositLayoutGroup.SetActive(true);
	}

	public void ShowWithdrawal()
	{
		//버튼클릭했을때 버튼에따라 나오는 ui
		deposit.gameObject.SetActive(false);
		withdrawal.gameObject.SetActive(false);
		transfer.gameObject.SetActive(false);
		withdrawalLayoutGroup.SetActive(true);
	}

	public void Return()
	{
		//레이아웃그룹false하고 게임오브젝트버튼만보이기
		depositLayoutGroup.SetActive(false);
		withdrawalLayoutGroup.SetActive(false);
		transferLayoutGroup.SetActive(false);
		GameManager.Instance.transfer.transferErrorObject.SetActive(false);
		GameManager.Instance.transfer.transferSuccessObject.SetActive(false);
		transfer.gameObject.SetActive(true);
		deposit.gameObject.SetActive(true);
		withdrawal.gameObject.SetActive(true);
	}

	public void ShowLoginUI()
	{
		popupLogin.SetActive(true);
		popupBank.SetActive(false);
		LoginError.gameObject.SetActive(false);
		GameManager.Instance.login.SignUpUI.SetActive(false);

	}
	public void LoginSuccese()
	{
		popupLogin.SetActive(false);
		popupBank.SetActive(true);
		Refresh();
		Return();

	}

	public void ShowTransfer()
	{
		deposit.gameObject.SetActive(false);
		withdrawal.gameObject.SetActive(false);
		transfer.gameObject.SetActive(false);
		transferLayoutGroup.SetActive(true);
	}




}
