using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class UserData
{
	[SerializeField] string id;
	[SerializeField] string password;
	[SerializeField] string userName;
	[SerializeField] float cash;
	[SerializeField] private float bankBalance;

	public string Id { get { return id; } set { id = value; } }
	public string Password { get { return password; } set { password = value; } }
	public string UserName { get { return userName; } set { userName = value; } }
	public float Cash { get { return cash; } set { cash = value; } }
	public float BankBalance { get { return bankBalance; } set { bankBalance = value; } }

	public UserData()
	{

	}

	public UserData(string id, string password, string userName, float cash, float bankBalance)
	{
		this.id = id;
		this.password = password;
		this.userName = userName;
		this.cash = cash;
		this.bankBalance = bankBalance;
	}
}
