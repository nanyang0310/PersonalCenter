﻿using UIWidgets;
using UnityEngine.UI;
using UnityEngine;

namespace UIWidgetsSamples
{
	/// <summary>
	/// TreeViewSample component country.
	/// </summary>
	public class TreeViewSampleComponentCountry : ComponentPool<TreeViewSampleComponentCountry>, ITreeViewSampleMultipleComponent
	{
		/// <summary>
		/// Icon.
		/// </summary>
		public Image Icon;

		/// <summary>
		/// Text.
		/// </summary>
		public Text Text;

		/// <summary>
		/// Set icon natize size.
		/// </summary>
		public bool SetNativeSize;

		/// <summary>
		/// Create component instance.
		/// </summary>
		/// <param name="parent">New parent.</param>
		/// <returns>GroupedListViewComponent instance.</returns>
		public ITreeViewSampleMultipleComponent IInstance(Transform parent)
		{
			return Instance(parent);
		}

		/// <summary>
		/// Set data.
		/// </summary>
		/// <param name="item">Item.</param>
		public virtual void SetData(ITreeViewSampleItem item)
		{
			SetData(item as TreeViewSampleItemCountry);
		}

		/// <summary>
		/// Set data.
		/// </summary>
		/// <param name="item">Item.</param>
		public virtual void SetData(TreeViewSampleItemCountry item)
		{
			Icon.sprite = item.Icon;
			Text.text = item.Name;
			
			if (SetNativeSize)
			{
				Icon.SetNativeSize();
			}
			
			Icon.color = (Icon.sprite==null) ? Color.clear : Color.white;
		}

		/// <summary>
		/// Is colors setted at least once?
		/// </summary>
		protected bool GraphicsColorSetted;

		/// <summary>
		/// Set graphics colors.
		/// </summary>
		/// <param name="foregroundColor">Foreground color.</param>
		/// <param name="backgroundColor">Background color.</param>
		/// <param name="fadeDuration">Fade duration.</param>
		public virtual void GraphicsColoring(Color foregroundColor, Color backgroundColor, float fadeDuration)
		{
			// reset default color to white, otherwise it will look darker than specified color,
			// because actual color = Text.color * Text.CrossFadeColor
			if (!GraphicsColorSetted)
			{
				Text.color = Color.white;
			}

			// change color instantly for first time
			Text.CrossFadeColor(foregroundColor, GraphicsColorSetted ? fadeDuration : 0f, true, true);

			GraphicsColorSetted = true;
		}
	}
}