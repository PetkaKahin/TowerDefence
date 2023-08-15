using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

namespace UI
{
    public class TowerHeandlerUI : MonoBehaviour
    {
        private const float AlphaTransparent = 0;
        private const float AplhaVisible = 1;

        [Space, Header("Settings show options")]
        [SerializeField, Range(30f, 90f)] private float _angleCreate;
        [SerializeField, Range(-180f, 180f)] private float _offset;
        [SerializeField, Range(0.1f, 5f)] private float _distance;
        [SerializeField, Range(0f, 5f)] private float _speedAnimation;

        private List<Image> _imageOptions = new List<Image>();
        private List<Button> _options = new List<Button>();

        private float _offsetDeg2Rad;
        private float _angleCreateDeg2Rad;

        private bool _isShow = false;

        private Tweener _tweener;

        public event Action AnimationCompleted;

        public void Construct(List<Button> options)
        { 
            SetOption(options);

            for (int i = 0; i < _options.Count; i++)
            {
                _imageOptions.Add(_options[i].GetComponent<Image>());

                _options[i].gameObject.SetActive(false);

                Color white = _imageOptions[i].color;
                white.a = AlphaTransparent;
                _imageOptions[i].color = white;
            }

            _offsetDeg2Rad = _offset * Mathf.Deg2Rad;
            _angleCreateDeg2Rad = _angleCreate * Mathf.Deg2Rad;
        }

        public void Enter()
        {
            if (_isShow == false)
                ShowOptions();
            else
                CloseOptions();

            _isShow = !_isShow;
        }

        public void SetOption(List<Button> options)
        {
            _imageOptions.Clear();
            _options = options;
        }

        private void ShowOptions()
        {
            for (int i = 0; i < _options.Count; i++)
            {
                Vector3 position = transform.position + (new Vector3(Mathf.Sin((i * _angleCreateDeg2Rad) + _offsetDeg2Rad), 
                    Mathf.Cos((i * _angleCreateDeg2Rad) + _offsetDeg2Rad), 0f) * _distance);

                _options[i].gameObject.SetActive(true);
                _tweener = _options[i].transform.DOMove(position, _speedAnimation);
                _imageOptions[i].DOFade(AplhaVisible, _speedAnimation);
            }

            _tweener.onComplete += AnimationCmplate;
        }

        private void CloseOptions()
        {
            for (int i = 0; i < _options.Count; i++)
            {
                _tweener = _options[i].transform.DOMove(transform.position, _speedAnimation);
                _imageOptions[i].DOFade(AlphaTransparent, _speedAnimation);
            }

            _tweener.onComplete += HideOptions;
        }

        private void HideOptions()
        {
            _tweener.onComplete -= HideOptions;

            for (int i = 0; i < _options.Count; i++)
                _options[i].gameObject.SetActive(false);

            AnimationCmplate();
        }

        private void AnimationCmplate()
        {
            AnimationCompleted?.Invoke();
        }
    }
}
