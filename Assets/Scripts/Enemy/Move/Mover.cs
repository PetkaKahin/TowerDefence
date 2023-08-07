using UnityEngine;
using DG.Tweening;
using System;

namespace Enemy
{
    public class Mover
    {
        private readonly Transform _transform;

        private float _speed;

        private Vector3[] _movePoints;

        private Sequence _sequence = DOTween.Sequence();

        public Mover(Vector3[] movePoints, Transform transform, float speed = 1f)
        {
            _movePoints = movePoints;
            _transform = transform;
            _speed = speed;

            CreateNewRute(_movePoints);
        }

        public void CreateNewRute(Vector3[] movePoints)
        {
            _sequence.Kill();
            _sequence = DOTween.Sequence();

            _movePoints = movePoints;

            Vector3 deltaPoint = _transform.position;

            foreach (Vector3 point in _movePoints)
            {
                float speedPoint = (point - deltaPoint).magnitude / _speed;
                _sequence.Append(_transform.DOMove(point, speedPoint).SetEase(Ease.Linear));
                deltaPoint = point;
            }
        }

        public void Reset()
        {
            CreateNewRute(_movePoints);
        }

        public void SetSpeed(float speed)
        {
            if (speed < 0f)
                throw new ArgumentOutOfRangeException(nameof(speed));

            _speed = speed;
        }

        public void BeginMove() => _sequence.Play();

        public void StopMove() => _sequence.Pause();
    }
}
