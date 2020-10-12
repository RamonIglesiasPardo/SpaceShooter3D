﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace SlimUI.ModernMenu{
	public class MainMenuNew : MonoBehaviour {

		Animator CameraObject;
		GameObject PlayerShip;
		private AudioSource audioFont;
		[Header("Theme Audios")]
		public AudioClip engineStarting;
		public AudioClip engineStart;
		[Header("Loaded Scene")]
		[Tooltip("The name of the scene in the build settings that will load")]
		public string sceneName = ""; 

		public enum Theme {custom1, custom2, custom3};
		[Header("Theme Settings")]
		public Theme theme;
		public int themeIndex;
		public FlexibleUIData themeController;

		[Header("Panels")]
		[Tooltip("The UI Panel parenting all sub menus")]
		public GameObject mainCanvas;
		[Tooltip("The UI Panel that holds the CONTROLS window tab")]
		public GameObject PanelControls;
		[Tooltip("The UI Panel that holds the VIDEO window tab")]
		public GameObject PanelVideo;
		[Tooltip("The UI Panel that holds the GAME window tab")]
		public GameObject PanelGame;
		[Tooltip("The UI Panel that holds the KEY BINDINGS window tab")]
		public GameObject PanelKeyBindings;
		[Tooltip("The UI Sub-Panel under KEY BINDINGS for MOVEMENT")]
		public GameObject PanelMovement;
		[Tooltip("The UI Sub-Panel under KEY BINDINGS for COMBAT")]
		public GameObject PanelCombat;
		[Tooltip("The UI Sub-Panel under KEY BINDINGS for GENERAL")]
		public GameObject PanelGeneral;

		[Header("SFX")]
		[Tooltip("The GameObject holding the Audio Source component for the HOVER SOUND")]
		public GameObject hoverSound;
		[Tooltip("The GameObject holding the Audio Source component for the AUDIO SLIDER")]
		public GameObject sliderSound;
		[Tooltip("The GameObject holding the Audio Source component for the SWOOSH SOUND when switching to the Settings Screen")]
		public GameObject swooshSound;

		// campaign button sub menu
		[Header("Menus")]
		[Tooltip("The Menu for when the MAIN menu buttons")]
		public GameObject mainMenu;
		[Tooltip("THe first list of buttons")]
		public GameObject firstMenu;
		[Tooltip("The Menu for when the PLAY button is clicked")]
		public GameObject playMenu;
		[Tooltip("Optional 4th Menu")]
		public GameObject extrasMenu;

		// highlights
		[Header("Highlight Effects")]
		[Tooltip("Highlight Image for when GAME Tab is selected in Settings")]
		public GameObject lineGame;
		[Tooltip("Highlight Image for when VIDEO Tab is selected in Settings")]
		public GameObject lineVideo;
		[Tooltip("Highlight Image for when CONTROLS Tab is selected in Settings")]
		public GameObject lineControls;
		[Tooltip("Highlight Image for when KEY BINDINGS Tab is selected in Settings")]
		public GameObject lineKeyBindings;
		[Tooltip("Highlight Image for when MOVEMENT Sub-Tab is selected in KEY BINDINGS")]
		public GameObject lineMovement;
		[Tooltip("Highlight Image for when COMBAT Sub-Tab is selected in KEY BINDINGS")]
		public GameObject lineCombat;
		[Tooltip("Highlight Image for when GENERAL Sub-Tab is selected in KEY BINDINGS")]
		public GameObject lineGeneral;

		[Header("LOADING SCREEN")]
		public GameObject loadingMenu;
		public Slider loadBar;
		public TMP_Text finishedLoadingText;

		void Start(){
			audioFont = GetComponent<AudioSource>();
			CameraObject = transform.GetComponent<Animator>();
			playMenu.SetActive(false);
			if(extrasMenu) extrasMenu.SetActive(false);
			firstMenu.SetActive(true);
			mainMenu.SetActive(true);
			PlayerShip = GameObject.FindGameObjectWithTag("PlayerShip");
		}

		void Update(){
			SetThemeColors();
		}

		void SetThemeColors(){
			if(theme == Theme.custom1){
				themeController.currentColor = themeController.custom1.graphic1;
				themeController.textColor = themeController.custom1.text1;
				themeIndex = 0;
			}else if(theme == Theme.custom2){
				themeController.currentColor = themeController.custom2.graphic2;
				themeController.textColor = themeController.custom2.text2;
				themeIndex = 1;
			}else if(theme == Theme.custom3){
				themeController.currentColor = themeController.custom3.graphic3;
				themeController.textColor = themeController.custom3.text3;
				themeIndex = 2;
			}
		}

		public void  PlayCampaign (){
			if(extrasMenu) extrasMenu.SetActive(false);
			playMenu.SetActive(true);
		}
		
		public void  PlayCampaignMobile (){
			if(extrasMenu) extrasMenu.SetActive(false);
			playMenu.SetActive(true);
			mainMenu.SetActive(false);
		}

		public void  ReturnMenu (){
			playMenu.SetActive(false);
			if(extrasMenu) extrasMenu.SetActive(false);
			mainMenu.SetActive(true);
		}

		public void NewGame(){
			if(sceneName != ""){
				StartCoroutine(LoadWithAnimation(sceneName));
				//StartCoroutine(LoadAsynchronously(sceneName));
				//SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
			}
		}

		public void  DisablePlayCampaign (){
			playMenu.SetActive(false);
		}

		public void  Position2 (){
			DisablePlayCampaign();
			CameraObject.SetFloat("SettingsAnimation",1);
		}

		public void  Position1 (){
			CameraObject.SetFloat("SettingsAnimation", 0);
		}

		public void  GamePanel (){
			PanelControls.SetActive(false);
			PanelVideo.SetActive(false);
			PanelGame.SetActive(true);
			PanelKeyBindings.SetActive(false);

			lineGame.SetActive(true);
			lineControls.SetActive(false);
			lineVideo.SetActive(false);
			lineKeyBindings.SetActive(false);
		}

		public void  VideoPanel (){
			PanelControls.SetActive(false);
			PanelVideo.SetActive(true);
			PanelGame.SetActive(false);
			PanelKeyBindings.SetActive(false);

			lineGame.SetActive(false);
			lineControls.SetActive(false);
			lineVideo.SetActive(true);
			lineKeyBindings.SetActive(false);
		}

		public void  ControlsPanel (){
			PanelControls.SetActive(true);
			PanelVideo.SetActive(false);
			PanelGame.SetActive(false);
			PanelKeyBindings.SetActive(false);

			lineGame.SetActive(false);
			lineControls.SetActive(true);
			lineVideo.SetActive(false);
			lineKeyBindings.SetActive(false);
		}

		public void  KeyBindingsPanel (){
			PanelControls.SetActive(false);
			PanelVideo.SetActive(false);
			PanelGame.SetActive(false);
			PanelKeyBindings.SetActive(true);

			lineGame.SetActive(false);
			lineControls.SetActive(false);
			lineVideo.SetActive(true);
			lineKeyBindings.SetActive(true);
		}

		public void  MovementPanel (){
			PanelMovement.SetActive(true);
			PanelCombat.SetActive(false);
			PanelGeneral.SetActive(false);

			lineMovement.SetActive(true);
			lineCombat.SetActive(false);
			lineGeneral.SetActive(false);
		}

		public void CombatPanel (){
			PanelMovement.SetActive(false);
			PanelCombat.SetActive(true);
			PanelGeneral.SetActive(false);

			lineMovement.SetActive(false);
			lineCombat.SetActive(true);
			lineGeneral.SetActive(false);
		}

		public void GeneralPanel (){
			PanelMovement.SetActive(false);
			PanelCombat.SetActive(false);
			PanelGeneral.SetActive(true);

			lineMovement.SetActive(false);
			lineCombat.SetActive(false);
			lineGeneral.SetActive(true);
		}

		public void PlayHover (){
			hoverSound.GetComponent<AudioSource>().Play();
		}

		public void PlaySFXHover (){
			sliderSound.GetComponent<AudioSource>().Play();
		}

		public void PlaySwoosh (){
			swooshSound.GetComponent<AudioSource>().Play();
		}

        public void Quit()
        {
			Application.Quit();
		}

		public void ScoreToMainMenu()
		{
			CameraObject.SetBool("MainMenuToScore", false);
			CameraObject.SetBool("ScoreToMainMenu", true);			
		}

		public void MainMenuToScore()
		{
			CameraObject.SetBool("MainMenuToScore", true);
			CameraObject.SetBool("ScoreToMainMenu", false);
						
		}

		public void  AreYouSureMobile (){
			if(extrasMenu) extrasMenu.SetActive(false);
			mainMenu.SetActive(false);
			DisablePlayCampaign();
		}



		IEnumerator LoadAsynchronously (string sceneName){ // scene name is just the name of the current scene being loaded
			AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
			operation.allowSceneActivation = false;
			mainCanvas.SetActive(false);
			loadingMenu.SetActive(true);
			while (!operation.isDone){
				float progress = Mathf.Clamp01(operation.progress / .9f);
				loadBar.value = progress;
				if(operation.progress >= 0.9f){
					finishedLoadingText.gameObject.SetActive(true);

					if(Input.anyKeyDown){
						operation.allowSceneActivation = true;
					}
				}				
				yield return null;
			}
		}
		IEnumerator LoadWithAnimation(string sceneName){ 
			AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
			operation.allowSceneActivation = false;
			mainCanvas.SetActive(false);			
			CameraObject.SetBool("PlayerShipAnimation", true);
			StartCoroutine(PlayerShip.GetComponent<Shake>().cShake(200f, 6f, PlayerShip.transform.position));
			InvokeRepeating("StartEngineFX", 0.5f, 0.2f);
			audioFont.clip = engineStarting;
			audioFont.loop = true;
			audioFont.Play();
			yield return new WaitForSeconds(2);
			while (!operation.isDone)
			{
				audioFont.clip = engineStart;
				audioFont.loop = false;
				audioFont.Play();
				yield return new WaitForSeconds(5);
				Vector3 ShipVector = PlayerShip.GetComponent<Transform>().TransformDirection(Vector3.forward);
				PlayerShip.GetComponent<Rigidbody>().AddForce(ShipVector * 5000f, ForceMode.Acceleration);
				yield return new WaitForSeconds(0.5f);
				operation.allowSceneActivation = true;
				yield return null;
			}
		}	
		private void StartEngineFX()
        {
			ParticleSystem ps = PlayerShip.GetComponentInChildren<ParticleSystem>();
			var fx = ps.main;
			var value = Random.value;
			if (value < 0.2)
            {
				fx.startSize = 100;
				fx.simulationSpeed = 10;				
			} else if (value > 0.2 && value < 0.6)
            {
				fx.startSize = 5;
				fx.simulationSpeed = 5;
			} else
            {
				fx.startSize = 50;
				fx.simulationSpeed = 8;
			}			
        }
	}
}