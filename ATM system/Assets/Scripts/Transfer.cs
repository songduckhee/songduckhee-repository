using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Transfer : MonoBehaviour
{
	public TMP_InputField recipient;
	public TMP_InputField amount;

	public TextMeshProUGUI transferErrorMSG;
	public TextMeshProUGUI transferSuccessMSG;

	public Button transferButton;
	public Button transferErrorButton;
	public Button transferSucceseButton;

	private string a;

	public GameObject transferErrorObject;
	public GameObject transferSuccessObject;
	void Start()
	{
		transferButton.onClick.AddListener(TryTransfer);
		transferErrorButton.onClick.AddListener(TransferErrorBtn);
		transferSucceseButton.onClick.AddListener(TransferSucceseBtn);
	}

	public void TryTransfer()
	{
		if (recipient.text != "" || amount.text != "")
		{
			if (GameManager.Instance.userData != null)
			{
				foreach (UserData use in GameManager.Instance.userData)
				{
					if (use.UserName == recipient.text)
					{
						if (int.TryParse(amount.text, out int recipientAmount))
						{
							int recipiAmount = recipientAmount;

							if (GameManager.Instance.accessUserData.BankBalance - recipiAmount >= 0)
							{
								use.BankBalance += recipiAmount;
								GameManager.Instance.accessUserData.BankBalance -= recipiAmount;
								GameManager.Instance.SaveUserData();
								GameManager.Instance.uIView.Refresh();
								TransferSuccese();
								return;
							}
							else
							{
								a = "잔액이 부족합니다";
								TransferError(a);
								Debug.Log("잔액이 부족합니다");
								return;
							}
						}
						else
						{
							a = "보내는 금액이 숫자가 아닙니다.";
							TransferError(a);
							Debug.Log("보내는 금액이 숫자가 아닙니다.");
							return;
						}
					}
				}
				a = "해당하신 이름이 없습니다";
				TransferError (a);
				Debug.Log("해당하신 이름이 없습니다");
				return;
			}
			else
			{
				a = "유저데이터가 없음!";
				TransferError(a);
				Debug.Log("유저데이터가 없음!");
				return;
			}
		}
		else
		{
			a = "입력 정보를 확인해주세요";

			TransferError(a);
			Debug.Log("입력 정보를 확인해주세요");
			return;
		}
	}

	public void TransferError(string a)
	{
		transferErrorObject.SetActive(true);
		transferErrorMSG.text = a;

	}
	public void TransferErrorBtn()
	{
		transferErrorObject.SetActive(false);
		transferErrorMSG.text = "";
	}
	public void TransferSuccese()
	{
		transferSuccessObject.SetActive(true);
		transferSuccessMSG.text = "송금이 완료되었습니다";
		recipient.text = "";
		amount.text = "";
	}
	public void TransferSucceseBtn()
	{
		transferSuccessObject.SetActive(false);
	}
}
