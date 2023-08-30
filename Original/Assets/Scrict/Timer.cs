using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	[SerializeField]
	private int minute;
	[SerializeField]
	private float seconds;
	//�@�O��Update�̎��̕b��
	private float oldSeconds;
	//�@�^�C�}�[�\���p�e�L�X�g
	private Text timerText;

	public static int endMinuts;
	public static int endSeconds;
	public static int endOldSeconds;
	public static Text endTimerText;

	GameObject player;

	void Start()
	{
		minute = 0;
		seconds = 0f;
		oldSeconds = 0f;
		timerText = GetComponentInChildren<Text>();

		this.player = GameObject.Find("player");
	}

	void Update()
	{
		seconds += Time.deltaTime;
		if (seconds >= 60f)
		{
			minute++;
			seconds = seconds - 60;
		}
        //�@�l���ς�����������e�L�X�gUI���X�V
        if ((int)seconds != (int)oldSeconds)
        {
            timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
        oldSeconds = seconds;

		endMinuts = minute;
		endSeconds = (int)seconds;
		endOldSeconds = (int)oldSeconds;
		endTimerText = timerText;

		Vector3 playerPos = this.player.transform.position;
		transform.position = new Vector3(
			playerPos.x + 8, playerPos.y + 5, transform.position.z);
	}
}
