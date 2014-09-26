using RTSGame.ViewModels;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using RTSGame.Hubs;
using System.Security.Cryptography;
using System.Text;

namespace RTSGame
{
    public class GameState
    {
        private static readonly Lazy<GameState> _instance = new Lazy<GameState>(
            () => new GameState(
                GlobalHost.ConnectionManager.GetHubContext<GameHub>()));

        private readonly ConcurrentDictionary<String, Player> _players = new ConcurrentDictionary<string, Player>(StringComparer.OrdinalIgnoreCase);

        private readonly ConcurrentDictionary<String, Game> _games = new ConcurrentDictionary<string, Game>(StringComparer.OrdinalIgnoreCase);

        public GameState(IHubContext context)
        {
            Clients = context.Clients;
            Groups = context.Groups;
        }
        public static GameState Instance
        {
            get { return _instance.Value; }
        }

        public IHubConnectionContext<dynamic> Clients { get; set; }
        public IGroupManager Groups { get; set; }

        public Player CreatePlayer(String userName)
        {
            var player = new Player(userName, GetMD5Hash(userName));
            _players[userName] = player;
            return player;
        }

        private string GetMD5Hash(string userName)
        {
            return String.Join("", MD5.Create()
                .ComputeHash(Encoding.Default.GetBytes(userName))
                .Select(b => b.ToString("x2")));
        }
       
        public Player GetPlayer(String userName)
        {
            return _players.Values.FirstOrDefault(uName => uName.Name == userName);
        }

        public Player GetNewOpponent(Player player)
        {
            return _players.Values.FirstOrDefault(uName => !uName.IsPlaying && uName.ID != player.ID);
        }

        public Player GetOpponent(Player player , Game game)
        {
            if(game.Player1.ID == player.ID)
            {
                return game.Player2;
            }

            return game.Player1;
        }

        public Game CreateGame(Player player1 , Player player2)
        {
            var game = new Game()
            {
                Player1 = player1,
                Player2 = player2,
                Board = new Board()
            };

            var group = Guid.NewGuid().ToString("d");
            _games[group] = game;

            player1.IsPlaying = true;
            player1.Group = group;

            player2.IsPlaying = true;
            player2.Group = group;

            Groups.Add(player1.ConnectionID, group);
            Groups.Add(player2.ConnectionID, group);

            return game;

        }

        public Game FindGame (Player player , out Player opponent)
        {
            opponent = null;
            if(player.Group == null)
            {
                return null;
            }

            Game game;
            _games.TryGetValue(player.Group, out game);

            if(game != null)
            {
                if(player.ID == game.Player1.ID)
                {
                    opponent = game.Player2;
                    return game;
                }

                opponent = game.Player1;
                return game;
            }

            return null;
        }

        public void ResetGame(Game game)
        {
            var groupName = game.Player1.Group;
            var player1Name = game.Player1.Name;
            var player2Name = game.Player2.Name;

            Groups.Remove(game.Player1.ConnectionID, groupName);
            Groups.Remove(game.Player2.ConnectionID, groupName);

            Player player1;
            _players.TryRemove(player1Name, out player1);

            Player player2;
            _players.TryRemove(player2Name, out player2);

            Game g;
            _games.TryRemove(groupName, out g);
        }
    }
}