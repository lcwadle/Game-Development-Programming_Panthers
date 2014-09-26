using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using RTSGame.ViewModels;

namespace RTSGame.Hubs
{
    public class GameHub : Hub
    {
        public bool Join(String userName)
        {
            var player = GameState.Instance.GetPlayer(userName);

            if( player != null)
            {
                Clients.Caller.playerExists();
                    return true;
            }

            player = GameState.Instance.CreatePlayer(userName);
            player.ConnectionID = Context.ConnectionId;
            Clients.Caller.name = player.Name;
            Clients.Caller.hash = player.Hash;
            Clients.Caller.ID = player.ID;

            Clients.Caller.playerJoined(player);
            return StartGame(player);
        }

        public bool StartGame(Player player)
        {
                if(player != null)
                {
                Player player2;
                var game = GameState.Instance.FindGame(player, out player2);
                if(game != null)
                {
                    Clients.Group(player.Group).BuildBoard(game);
                    return true;
                }

                player2 = GameState.Instance.GetNewOpponent(player);
                if(player2 == null)
                {
                    Clients.Caller.waitingList();
                    return true;
                }

                game = GameState.Instance.CreateGame(player, player2);
                game.WhosTurn = player.ID;

                Clients.Group(player.Group).buildBoard(game);
                return true;
                }
        return false;
        }
     
        public bool Flip(String cardName)
        {
            var userName = Clients.Caller.name;
            var player = GameState.Instance.GetPlayer(userName);
            if(player != null)
            {
                Player playerOpponent;
                var game = GameState.Instance.FindGame(player, out playerOpponent);

                if(game != null)
                {
                    if(!String.IsNullOrEmpty(game.WhosTurn) && game.WhosTurn != player.ID)
                    {
                        return true;
                    }

                    var card = FindCard(game, cardName);
                    Clients.Group(player.group).flipCard(card);
                    return true;
                }
            }

            return false;
        }

        private Card FindCard(Game game, string cardName)
        {
            return game.Board.Pieces.FirstOrDefault(c => c.Name == cardName);
        }

        public bool checkCard(String cardName)
        {
            var userName = Clients.Caller.name;
            Player player = GameState.Instance.GetPlayer(userName);
            if(player != null)
            {
                Player playerOpponent;
                Game game = GameState.Instance.FindGame(player, out playerOpponent);
                if(game != null)
                {
                    if(!String.IsNullOrEmpty(game.WhosTurn) && game.WhosTurn != player.ID)
                    {
                        return true;
                    }

                    Card card = FindCard(game, cardName);

                    if(game.LastCard == null)
                    {
                        game.WhosTurn = player.ID;
                        game.LastCard = card;
                        return true;
                    }

                    //Second Flip.

                    bool isMatch = IsMatch(game, card);
                    if(isMatch)
                    {
                        StoreMatch(player, card);
                        game.LastCard = null;
                        Clients.Group(player.Group).showMatch(card, userName);

                        if(player.Matches.Count >= 16)
                        {
                            Clients.Group(player.Group).winner(card, userName);
                            GameState.Instance.ResetGame(game);
                            return true;
                        }

                        return true;
                    }

                    Player opponent = GameState.Instance.GetOpponent(player, game);
                    //Shift to other player.
                    game.WhosTurn = opponent.ID;

                    Clients.Group(player.Group).resetFlip(game.LastCard, card);
                    game.LastCard = null;
                    return true;
                }
            }

            return false;
        }

        private void StoreMatch(Player player, Card card)
        {
            player.Matches.Add(card.ID);
            player.Matches.Add(card.pair);
        }

        private bool IsMatch(Game game, Card card)
        {
            if(card == null)
            {
                return false;
            }

            if(game.LastCard != null)
            {
                if(game.LastCard.pair == card.ID)
                {
                    return true;
                }
                return false;
            }

            return false;
        }
    }
}