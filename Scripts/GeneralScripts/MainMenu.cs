using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

namespace BaseFramework
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject loadingImageGO;

        public void LoadLevel(int sceneIndex)
        {
            StartCoroutine(SceneProgress(sceneIndex));
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        IEnumerator SceneProgress(int sceneIndex)
        {
            AsyncOperation loadOps = SceneManager.LoadSceneAsync(sceneIndex);

			if (loadingImageGO != null)
			{
				while (!loadOps.isDone)
				{
					float progress = loadOps.progress;

					if (loadingImageGO != null)
					{
						loadingImageGO.SetActive(true);
						loadingImageGO.GetComponent<Image>().fillAmount = (progress >= .89f) ? 1 : progress;
					}

					yield return null;

				}
			}
        }


    }
}

