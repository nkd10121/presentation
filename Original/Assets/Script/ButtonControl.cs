using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public void OnClick()
    {
        //ステージセレクト画面へ
        if(gameObject.CompareTag("startButton"))
        {
            SceneManager.LoadScene("stageSelectScene");
        }

        //操作説明画面へ
        else if (gameObject.CompareTag("sousaButton"))
        {
            SceneManager.LoadScene("sousaScene");
        }

        //スタート画面へ
        else if (gameObject.CompareTag("endButton"))
        {
            SceneManager.LoadScene("startScene");
        }

        //ステージ1へ
        else if (gameObject.CompareTag("stage1Button"))
        {
            SceneManager.LoadScene("stage1Scene");
        }

        //ステージ2へ
        else if (gameObject.CompareTag("stage2Button"))
        {
            SceneManager.LoadScene("stage2Scene");
        }
    }
}
