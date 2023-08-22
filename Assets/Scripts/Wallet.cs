using System;

namespace Assets.Scripts
{
    public class Wallet
    {
        private int _value;

        public int Value => _value;

        public void Append(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _value += amount;
        }

        public bool TrySpend(int amount)
        {
            if (_value - amount < 0)
                return false;

            _value -= amount;

            return true;
        }
    }
}
