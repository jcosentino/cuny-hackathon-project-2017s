﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Vuforia;

namespace Merge
{		
	public class MergeCubeSDK : MonoBehaviour 
	{

		public static MergeCubeSDK instance;

		void Awake()
		{
			if (instance == null)
				instance = this;
			else if (instance != this)
				DestroyImmediate(this.gameObject);
		}


		public enum ViewMode { HEADSET, FULLSCREEN };

		public ViewMode viewMode = ViewMode.FULLSCREEN;

		private Transform arCameraRef;

		private VideoBackgroundBehaviour leftVidBackBehaviour;
		private VideoBackgroundBehaviour rightVidBackBehaviour;

		public GameObject headsetViewSetup;
		public RenderTexture headsetViewRenderTexture;

		bool isActive = false;

		public UnityEngine.UI.Image viewSwitchButton;
		public UnityEngine.UI.Image viewSwitchGraphic;
		public Sprite fullscreenSprite;
		public Sprite headsetViewSprite;
		public Sprite disabledSprite;

		public Animator mainPanelAnimator;
		bool menuIsOpen = false;

		public delegate void ViewModeSwapEvent(bool swappedToHeadsetView);
		public ViewModeSwapEvent OnViewModeSwap;

		void Start()
		{
			arCameraRef = Camera.main.transform;
			if (viewMode == ViewMode.HEADSET)
			{
				Camera.main.targetTexture = headsetViewRenderTexture;
			}
		}

		void OnValidate()
		{
			if (viewMode == ViewMode.HEADSET && headsetViewRenderTexture != null)
			{
				Camera.main.targetTexture = headsetViewRenderTexture;
				headsetViewSetup.SetActive(true);
			}
			else
			{
				if(Camera.main != null)
					Camera.main.targetTexture = null;
				
				headsetViewSetup.SetActive(false);
			}
		}

		public void ToggleMenu()
		{
			if (menuIsOpen)
			{
				mainPanelAnimator.Play("Close");
			}
			else
			{
				mainPanelAnimator.Play("Open");
			}

			menuIsOpen = !menuIsOpen;
		}

		public void SwitchView()
		{
			if (viewMode == ViewMode.HEADSET)
			{
				viewMode = ViewMode.FULLSCREEN;
			}
			else
			{
				viewMode = ViewMode.HEADSET;
			}

			if (viewMode == ViewMode.HEADSET)
			{
				SetToHeadsetView ();
				viewSwitchGraphic.sprite = headsetViewSprite;
			} 
			else 
			{
				SetToFullscreenView ();
				viewSwitchGraphic.sprite = fullscreenSprite;
			}

			viewSwitchButton.gameObject.SetActive (false);

			if (OnViewModeSwap != null)
			{
				OnViewModeSwap.Invoke((viewMode == ViewMode.HEADSET));
			}

			Invoke ("EnableViewChangeBtn", 0.5f);
		}

		void EnableViewChangeBtn()
		{
			viewSwitchButton.gameObject.SetActive (true);
		}



		void SetToFullscreenView()
		{
			Camera.main.targetTexture = null;
			headsetViewSetup.SetActive(false);
		}

		void SetToHeadsetView()
		{
			Camera.main.targetTexture = headsetViewRenderTexture;
			headsetViewSetup.SetActive(true);
		}


		public void SwapCameraFacingDirection()
		{
			Vuforia.CameraDevice.Instance.Stop();
			Vuforia.CameraDevice.Instance.Deinit();

			if (Vuforia.CameraDevice.Instance.GetCameraDirection() == Vuforia.CameraDevice.CameraDirection.CAMERA_BACK)
			{
				viewSwitchButton.raycastTarget = false;
				viewSwitchGraphic.sprite = disabledSprite;

				Vuforia.CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_FRONT);

				SetToFullscreenView();

				Debug.Log("Should be front: " + Vuforia.CameraDevice.Instance.GetCameraDirection());
			}
			else
			{
				viewSwitchButton.raycastTarget = true;
				Vuforia.CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_BACK);

				if (viewMode == ViewMode.HEADSET)
				{
					viewSwitchGraphic.sprite = headsetViewSprite;
					SetToHeadsetView();
				}
				else
				{
					viewSwitchGraphic.sprite = fullscreenSprite;
					SetToFullscreenView();
				}

				Debug.Log("Should be back: " + Vuforia.CameraDevice.Instance.GetCameraDirection());
			}



			Vuforia.CameraDevice.Instance.Start();
		}

	//FlashLight
		bool isFlashOn = false;

		public void SwitchFlashLight()
		{
			isFlashOn = !isFlashOn;

			if (isFlashOn) 
			{
				TurnFlashOn ();
			} 
			else 
			{
				TurnFlashOff ();
			}
		}

		void TurnFlashOff()
		{
			Vuforia.CameraDevice.Instance.SetFlashTorchMode (false);
		}

		void TurnFlashOn()
		{
			Vuforia.CameraDevice.Instance.SetFlashTorchMode (true);
		}

	}
}