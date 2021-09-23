﻿namespace FightGame.Characters {
    public class Healer : Character {

        public Healer() {
            _lifePoints = 4;
            _defendPoints = 0;
            _attackPoints = 1;
            _defaultAttackPoints = _attackPoints;
            _damages = 0;

        }
        
        
        protected override void _SpecialCapacity(Character other) {
            ++_lifePoints;
        }
    }
}