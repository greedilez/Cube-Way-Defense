using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinVisualising : MonoBehaviour
{
    [SerializeField] private GameObject _confetti;

    private GameObject _player;

    [SerializeField] private Animator _blackScreenAnimator;

    private bool _isFollowingPlayer = false;

    public void VisualizeWining() {
        _player = FindObjectOfType<Player>().gameObject;
        _confetti.SetActive(true);
        _isFollowingPlayer = true;
        _blackScreenAnimator.gameObject.SetActive(true);
        _blackScreenAnimator.SetTrigger("Appear");
        StartCoroutine(NewSceneLoadDelay());
    }

    private IEnumerator NewSceneLoadDelay() {
        yield return new WaitForSeconds(2f); SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } 

    private void FixedUpdate() {
        if(_isFollowingPlayer) _confetti.transform.position = _player.transform.position;
    }
}
