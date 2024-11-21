using Game.Gameplay;
using Game.Gameplay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Battlefield
{
    public class BattleCyclic
    {
        private readonly Pawnfield pawnfield;
        private List<BasePawn> pawnQueue;
        private CancellationTokenSource cts;

        public event Action<Team> WinnerTeam;

        public BattleCyclic(Pawnfield pawnfield)
        {
            this.pawnfield = pawnfield;
        }

        public async void Run()
        {
            pawnQueue = pawnfield.CalculatePawnQueue();

            Team losingTeam = null;

            do
            {
                for (int i = 0; i < pawnQueue.Count; i++)
                {
                    var pawn = pawnQueue[i];
                    pawn.Run();
                    Debug.Log("1");
                    cts = new CancellationTokenSource();
                    await GetPawnFromCurrentTurn(cts.Token);
                    Debug.Log("2");

                    losingTeam = pawnfield.Teams.FirstOrDefault(t => t.CurrentTeamSize == 0);
                    Debug.Log(losingTeam == null);
                }
            }
            while (losingTeam == null);

            GameOver(losingTeam);
        }

        public async Task GetPawnFromCurrentTurn(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Yield();
            }
        }

        public void EndTurn(InputAction.CallbackContext context)
        {
            Debug.Log("3");
            cts.Cancel();
        }

        private void GameOver(Team team)
        {
            WinnerTeam?.Invoke(team);
        }
    }
}