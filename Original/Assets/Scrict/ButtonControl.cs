using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public void OnClick()
    {
        //�X�e�[�W�Z���N�g��ʂ�
        if(gameObject.CompareTag("startButton"))
        {
            SceneManager.LoadScene("stageSelectScene");
        }

        //���������ʂ�
        else if (gameObject.CompareTag("sousaButton"))
        {
            SceneManager.LoadScene("sousaScene");
        }

        //�X�^�[�g��ʂ�
        else if (gameObject.CompareTag("endButton"))
        {
            SceneManager.LoadScene("startScene");
        }

        //�X�e�[�W1��
        else if (gameObject.CompareTag("stage1Button"))
        {
            SceneManager.LoadScene("stage1Scene");
        }

        //�X�e�[�W2��
        else if (gameObject.CompareTag("stage2Button"))
        {
            SceneManager.LoadScene("stage2Scene");
        }
    }
}
