using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TransactionType
{
	Deposit,
	Withdrawal
}

public class Transactions : MonoBehaviour
{
	[Header("입금,출금")]
	public TransactionType transactionType;

	UserData data;

	public Button[] button;

	public Button inputButton;

	// Start is called before the first frame update
	void Start()
	{
		data = GameManager.Instance.accessUserData;
		Add();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Add()
	{
		if (transactionType == TransactionType.Deposit)
		{

			for (int i = 0; i < button.Length; i++)
			{
				int q = i;
				button[i].onClick.AddListener(() => Deposit(q));
			}
			inputButton.onClick.AddListener(DepositTyping);
		}
		else if (transactionType == TransactionType.Withdrawal)
		{

			for (int i = 0; i < button.Length; i++)
			{
				int q = i;
				button[i].onClick.AddListener(() => Withdrawal(q));
			}
			inputButton.onClick.AddListener(WithdrawalTyping);
		}
	}

	void Deposit(int index)
	{
		int transactions = int.Parse(button[index].gameObject.name);
		if (data.Cash - transactions > 0)
		{
			data.BankBalance += transactions;
			data.Cash -= transactions;
		}
		else
		{
			Debug.Log("현재잔액부족!");
		}
		GameManager.Instance.uIView.Refresh();

	}
	void Withdrawal(int index)
	{
		int transactions = int.Parse(button[index].gameObject.name);
		if (data.BankBalance - transactions > 0)
		{
			data.Cash += transactions;
			data.BankBalance -= transactions;
		}
		else
		{
			Debug.Log("통장잔액부족!");
		}
		GameManager.Instance.uIView.Refresh();
	}

	void DepositTyping()
	{
		TMP_InputField text = inputButton.gameObject.GetComponentInChildren<TMP_InputField>();

		if (int.TryParse(text.text, out int transactions))
		{
			if (data.Cash - transactions >= 0)
			{
				data.BankBalance += transactions;
				data.Cash -= transactions;
			}
			else
			{
				Debug.Log("현재잔액부족!");
			}

		}
		else
		{
			Debug.Log("변환안됨!");
		}
		GameManager.Instance.uIView.Refresh();

	}


	void WithdrawalTyping()
	{
		TMP_InputField text = inputButton.gameObject.GetComponentInChildren<TMP_InputField>();

		if (int.TryParse(text.text, out int transactions))
		{
			if (data.BankBalance - transactions >= 0)
			{
				data.Cash += transactions;
				data.BankBalance -= transactions;
			}
			else
			{
				Debug.Log("통장잔액부족!");
			}

		}
		else
		{
			Debug.Log("변환안됨!");
		}
		GameManager.Instance.uIView.Refresh();
	}


}
