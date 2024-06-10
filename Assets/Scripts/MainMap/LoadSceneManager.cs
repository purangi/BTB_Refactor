using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanageManager : MonoBehaviour
{
    public static int nextScene;

    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI tipTxt;

    public static void LoadScene(int sceneId)
    {
        nextScene = sceneId;
        SceneManager.LoadScene(sceneId);
    }

    IEnumerator LoadScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        ShowRandomTip();

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            if (op.progress < 0.7f)
            {
                progressBar.fillAmount = op.progress;
            } else
            {
                timer += Time.unscaledDeltaTime; //loading scene must play always
                progressBar.fillAmount = Mathf.Lerp(0.7f, 1f, timer);

                if(progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private void ShowRandomTip()
    {
        string[] tips =
        {
            "Tip 1",
            "Tip 2",
            "Tip 3",
        };

        int rndIndex = Random.RandomRange(0, tips.Length);
        if(tipTxt != null )
        {
            tipTxt.text = tips[rndIndex];
        }
    }
}
