using Game.Battlefield.Core;
using Game.Battlefield.Pawnfields;
using Game.Battlefield.Pawnfields.Teams;
using Game.Gameplay.Gamefields;
using Game.Gameplay.Pawnfields.Factories;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Game.Gameplay
{
    public readonly struct CharacterInitiative
    {
        public readonly Character Character;
        public readonly int Value;

        public CharacterInitiative(Character character, int value)
        {
            this.Character = character;
            this.Value = value;
        }
    }

    public sealed class CharacterField
    {
        private readonly Gamefield gamefield;

        private readonly Team playerTeam;
        private readonly Team enemyTeam;

        public CharacterField(Gamefield gamefield, SceneConfig sceneConfig)
        {
            this.gamefield = gamefield;

            var characterConfigs = sceneConfig.PlayerTeamConfig.CharacterConfigs;

            var playerCharacters = PlacementCharacters(characterConfigs, playerTeam, Character.OwnerType.Player);
            var enemyCharacters = PlacementCharacters(characterConfigs, enemyTeam, Character.OwnerType.PC);

            playerTeam = CreateTeam(playerCharacters);
            enemyTeam = CreateTeam(enemyCharacters);
        }

        public bool TrySetPlayerCharacterPosition(int index, Vector3 position)
        {
            var character = playerTeam.Members[index];

            var node = gamefield.GetNodeAtWorldPosition(position);

            if (node == null || node.IsWalkable == false) return false;

            character.View.gameObject.SetActive(true);
            character.View.position = node.CenterPosition;
            node.SetContent(character);
            return true;
        }

        public void SetEnemyCharacterPosition()
        {
            var nodes = gamefield.GetRandomNodesAtRightGridSideOnEmptyNodes(enemyTeam.Members.Count);

            for (int i = 0; i < nodes.Length; i++)
            {
                var character = enemyTeam.Members[i];
                character.View.gameObject.SetActive(true);
                character.View.position = nodes[i].CenterPosition;
                nodes[i].SetContent(character);
            }
        }

        public List<Character> CalculatePawnQueue()
        {
            var allPawns = new List<Character>();

            allPawns.AddRange(playerTeam.Members);
            allPawns.AddRange(enemyTeam.Members);

            var initiative = new CharacterInitiative[allPawns.Count];

            for (int i = 0; i < initiative.Length; i++)
            {
                initiative[i] = new CharacterInitiative(allPawns[i], Dice.GetValueAt(6, 6, 6));
            }

            return initiative.OrderByDescending(i => i.Value).Select(i => i.Character).ToList();
        }

        private Character[] PlacementCharacters(CharacterConfig[] characterConfigs, Team team, Character.OwnerType ownerType)
        {
            var playerCharacters = new Character[characterConfigs.Length];

            for (int i = 0; i < characterConfigs.Length; i++)
            {
                var playerCharacter = new CharacterFactory().Create(characterConfigs[i]);
                playerCharacter.Team = team;
                playerCharacter.Owner = ownerType;
                playerCharacters[i] = playerCharacter;
            }

            return playerCharacters;
        }

        private Team CreateTeam(Character[] playerCharacters)
        {
            return new TeamFactory().Create(playerCharacters);
        }
    }
}