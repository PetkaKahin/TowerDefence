using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Towers
{
    public class PointTowerHeandler : MonoBehaviour
    {
        [SerializeField] private Button _buttonOption;

        private List<Button> _buttonOptions = new List<Button>();

        private TowerHeandlerUI _heandlerUI;

        private TowerFactory _factory;
        private BaseTower _tower;

        public void Construct(TowerFactory factory, TowerHeandlerUI heandlerUI)
        {
            _factory = factory;
            _heandlerUI = heandlerUI;

            SetTowerCreateButtons();
        }

        public void CreateTower(int towerID)
        {
            if (_tower == null)
                _tower = _factory.Get(transform, towerID);

            _heandlerUI.AnimationCompleted += SetTowerHeandlerButtons;
        }

        public void UpgradeTower()
        {
            Debug.Log("Upgrade tower");
        }

        private void SetTowerHeandlerButtons()
        {
            _heandlerUI.AnimationCompleted -= SetTowerHeandlerButtons;

            foreach (Button button in _buttonOptions)
                Destroy(button.gameObject);

            _buttonOptions.Clear();

            for (int i = 0; i < 4; i++) // временная затычка
            {
                _buttonOptions.Add(Instantiate(_buttonOption, transform.position, Quaternion.identity, _heandlerUI.transform));

                int index = i;

                _buttonOptions[index].onClick.AddListener(UpgradeTower);
            }

            _heandlerUI.Construct(_buttonOptions);
        }

        private void SetTowerCreateButtons()
        {
            for (int i = 0; i < _factory.TowersCount; i++)
            {
                _buttonOptions.Add(Instantiate(_buttonOption, transform.position, Quaternion.identity, _heandlerUI.transform));

                int index = i; // колхоз, но я хз как лучше с лямбдой на 30 строке работать

                _buttonOptions[i].onClick.AddListener(() => CreateTower(index));
                _buttonOptions[i].onClick.AddListener(_heandlerUI.Enter);
            }

            _heandlerUI.Construct(_buttonOptions);
        } 
    }
}
