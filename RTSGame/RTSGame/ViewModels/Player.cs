using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTSGame.ViewModels
{
    public class Player
    {
        public Player(String name , String hash)
        {
            Name = name;
            Hash = hash;
            ID = Guid.NewGuid().ToString("d");
            Matches = new List<int>();
        }
        public String ConnectionID { get; set; }
        public String ID { get; set; }
        public String Name { get; set; }
        public String Hash { get; set; }
        public String Group { get; set; }
        public bool IsPlaying { get; set; }
        public List<int> Matches { get; set; }
    }
}