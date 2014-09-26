using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTSGame.ViewModels
{
    public class Board
    {
        private List<Card> _pieces = new List<Card>();
        public List<Card> Pieces
        {
            get {  return _pieces ;}
            set { _pieces = value ;}
        }

        public Board()
        {
            int imgIndex = 1;
            for(int i=1 ; i<=30 ; i++)
            {
                if(i%2 != 0)
                {
                    _pieces.Add(new Card()
                        {
                            ID = i,
                            pair = i + 1,
                            Name = "card-" + i.ToString(),
                            Image = "CardIndex_" + imgIndex
                        });
                }
                else
                {
                    _pieces.Add(new Card()
                    {
                        ID = i,
                        pair = i - 1,
                        Name = "card-" + i.ToString(),
                        Image = "CardIndex_" + imgIndex
                    });
                }
                imgIndex++;
            }
            _pieces.Shuffle();
        }
    }
}