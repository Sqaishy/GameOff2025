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

		private bool surfaceReached;

		private void Start()
		{
			//This is not smart but whatever for now

			float height = depthImage.sprite.texture.height * depthController.DepthMilestones;

			depthImage.rectTransform.sizeDelta = new Vector2(depthImage.rectTransform.sizeDelta.x,
				height);

			RectTransform sliderTransform = slider.GetComponent<RectTransform>();
			sliderTransform.sizeDelta = new Vector2(height, sliderTransform.sizeDelta.y);

			DepthController.OnReachedSurface += ReachedSurface;
		}

		private void OnDestroy()
		{
			DepthController.OnReachedSurface -= ReachedSurface;
		}

		private void Update()
		{
			if (surfaceReached)
				return;

			slider.value = depthController.GetDepthPercentage01();
			SetDepthText($"{depthController.CurrentDepth:N0}m");
		}

		private void SetDepthText(string text) => depthText.text = text;

		private void ReachedSurface()
		{
			surfaceReached = true;

			SetDepthText("0m (Surface)");
		}
	}
}