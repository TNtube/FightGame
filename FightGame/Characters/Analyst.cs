using System;

namespace FightGame.Characters {
    public class Analyst : Character {
        /*
         * The analyst has 3 life points, 1 attack point
         * and his special attack is able to reproduce
         * his opponent's attack by improving it.
         */
        public Analyst(string playerName) {
            
            _className = "Analyst";
            _userName = playerName;
            _lifePoints = 3;
            _totalLifePoints = _lifePoints;
            _defendPoints = 0;
            _attackPoints = 1;
            _defaultAttackPoints = _attackPoints;
            _damages = 0;
        }

        protected override void _SpecialCapacity(Character other) {
            switch (other.getClassName()) {
                case "Healer":
                    ++_lifePoints;
                    _lifePoints = Math.Min(_totalLifePoints + 1, _lifePoints);
                    break;
                case "Damager":
                    other.SetActualDamage(_damages + 1);
                    break;
                case "Tank":
                    _attackPoints += 1;
                    Attack(other);
                    break;
            }
        }
    }
}