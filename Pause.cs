using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private static bool _onPause = false;

    public static bool OnPause{ get => _onPause; }

    [SerializeField] private Animator _pauseScreenAnimator;

    public void SetToPause() {
        if (!_onPause) {
            Time.timeScale = 0;
            _onPause = true;
            _pauseScreenAnimator.gameObject.SetActive(true);
            _pauseScreenAnimator.SetTrigger("Appear");
        }
    }

    public void ExitFromPause() {
        if (_onPause) {
            Time.timeScale = 1;
            _onPause = false;
            _pauseScreenAnimator.SetTrigger("Disappear");
            StartCoroutine(PauseMenuDisableDelay());
        }
    }

    public IEnumerator PauseMenuDisableDelay() {
        yield return new WaitForSeconds(1f); _pauseScreenAnimator.gameObject.SetActive(false);
    }
}
