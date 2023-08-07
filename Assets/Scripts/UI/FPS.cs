using System.Collections;
using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private TMP_Text _fps;
    [SerializeField] private int _targetFPS;

    private WaitForSeconds _sleep;

    private void Awake()
    {
        Application.targetFrameRate = _targetFPS;

        _sleep = new WaitForSeconds(0.1f);

        StartCoroutine(UpdateFPS());
    }

    private IEnumerator UpdateFPS()
    {
        while (true)
        {
            _fps.text = $"FPS: {(int)(1 / Time.deltaTime)}";

            yield return _sleep;
        }
    }
}
