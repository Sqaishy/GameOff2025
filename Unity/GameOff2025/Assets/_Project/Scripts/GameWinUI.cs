using System;
using UnityEngine;

namespace SubHorror
{
	public class GameWinUI : MonoBehaviour
	{
		[SerializeField] private GameObject winPanel;

		private void Awake()
		{
			winPanel.SetActive(false);
		}

		public void ShowWinUI()
		{
			winPanel.SetActive(true);
		}
	}
}