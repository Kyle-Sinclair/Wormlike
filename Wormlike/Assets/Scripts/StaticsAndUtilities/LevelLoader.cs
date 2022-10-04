using System;
using ManagerScripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StaticsAndUtilities
{
    public class LevelLoader : MonoBehaviour, IGameService
    {
        
        private Image _loadingScreen;
        [SerializeField]private TextMeshProUGUI mText;
        private void Awake()
        {
            ServiceLocator.Initialize();
            _loadingScreen = GetComponentInChildren<Image>();
        }
        private void Start()
        {
            ServiceLocator.Current.Register(this);
            _loadingScreen.enabled = false;
        }
        public void StartLoadingBattleGround(int numPlayers, int numTeams)
        {
            _loadingScreen.enabled = true;
            StartCoroutine(LoadLevel(numPlayers,numTeams));
        }

        private IEnumerator  LoadLevel(int numPlayers, int numTeams)
        {
            yield return null;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Battleground",LoadSceneMode.Additive);
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone)
            {
                mText.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";
                if (asyncOperation.progress >= 0.9f)
                {
                    mText.text = "Press the space bar to continue";
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _loadingScreen.enabled = false;
                        mText.enabled = false;
                        asyncOperation.allowSceneActivation = true;
                    }
                }
                asyncOperation.completed += (AsyncOperation asyncOperation) =>
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Battleground"));
                };
                yield return null;
            }

            StartMapGeneration(numPlayers, numTeams);
        }

        private void StartMapGeneration(int numPlayers, int numTeams)
        {
            ServiceLocator.Current.Get<MapManager>().BeginGeneratingMap(numPlayers,numTeams);
        }
    }
}
