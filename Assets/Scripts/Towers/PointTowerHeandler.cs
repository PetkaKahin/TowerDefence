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

        private Gun _gun;

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
            _gun = _tower.GetComponent<Gun>();
        }

        public void UpgradeTowerRange()
        {
            float newRange = _tower.Range + 1;

            if (newRange < _tower.MaxRange)
            {
                _tower.SetRange(newRange);
            }
            else
            {
                _tower.SetRange(_tower.MaxRange);
                print($"Максимальный радиус башни: {_tower.Range}");
            }
        }

        public void UpgradeTowerDamage()
        {
            float newDamage = _gun.Damage + 0.5f;

            if (newDamage < _gun.MaxDamage)
            {
                _gun.SetDamage(newDamage);
            }
            else
            {
                _gun.SetDamage(_gun.MaxDamage);
                print($"Максимальный урон: {_gun.Damage}");
            } 
        }

        public void UpgradeTowerCoolDown()
        {
            float newCoolDown = _gun.CoolDown - 0.1f;

            if (newCoolDown >= _gun.MinCoolDown)
            {
                _gun.SetCoolDonw(_gun.CoolDown - 0.1f);
            }
            else
            {
                _gun.SetCoolDonw(_gun.MinCoolDown);
                print($"Время перезарядки минимально: {_gun.CoolDown}  {_gun.MinCoolDown}");
            }
        }

        private void SetTowerHeandlerButtons() // временная параша)
        {
            _heandlerUI.AnimationCompleted -= SetTowerHeandlerButtons;

            foreach (Button button in _buttonOptions)
                Destroy(button.gameObject);

            _buttonOptions.Clear();

            _buttonOptions.Add(Instantiate(_buttonOption, transform.position, Quaternion.identity, _heandlerUI.transform));
            _buttonOptions[0].onClick.AddListener(UpgradeTowerRange);

            _buttonOptions.Add(Instantiate(_buttonOption, transform.position, Quaternion.identity, _heandlerUI.transform));
            _buttonOptions[1].onClick.AddListener(UpgradeTowerDamage);

            _buttonOptions.Add(Instantiate(_buttonOption, transform.position, Quaternion.identity, _heandlerUI.transform));
            _buttonOptions[2].onClick.AddListener(UpgradeTowerCoolDown);

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
