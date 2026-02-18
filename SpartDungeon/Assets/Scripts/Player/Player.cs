using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
	public PlayerCondition condition;

	public ItemData itemData;
	public Action addItem;
	public Equipment equip;

	public Transform dropPosition;
	public Action Init;

	private void Awake()
	{
		CharacterManager.Instance.Player = this;
		controller = this.GetComponent<PlayerController>();
		condition = this.GetComponent<PlayerCondition>();
		equip = GetComponent<Equipment>();

	}
}
