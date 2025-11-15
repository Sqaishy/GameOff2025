using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SubHorror.Depth
{
	public class DepthUI : MonoBehaviour
	{
		[SerializeField] private DepthController depthController;
		[SerializeField] private Slider slider;
		[SerializeField] private Image depthImage;
		[SerializeField] private TMP_Text depthText;

		private void Start()
		{
			//This is not smart but whatever for now

			float height = depthImage.sprite.texture.height * depthController.DepthMilestones;

			depthImage.rectTransform.sizeDelta = new Vector2(depthImage.rectTransform.sizeDelta.x,
				height);

			RectTransform sliderTransform = slider.GetComponent<RectTransform>();
			sliderTransform.sizeDelta = new Vector2(height, sliderTransform.sizeDelta.y);
		}

		private void Update()
		{
			slider.value = depthController.GetDepthPercentage01();
			depthText.text = $"{depthController.CurrentDepth:N0}m";
		}
	}
}