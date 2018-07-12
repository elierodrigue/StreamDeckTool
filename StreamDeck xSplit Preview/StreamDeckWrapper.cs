using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDeck_xSplit_Preview
{
    public class StreamDeckWrapper
    {
        private StreamDeckSharp.IStreamDeck deck;
        private static StreamDeckWrapper instance;
        public static StreamDeckWrapper getInstance()
        {
            if(instance == null)
            {
                instance = new StreamDeckWrapper();
            }
            return instance;
        }
        public event EventHandler<StreamDeckSharp.ConnectionEventArgs> ConnectionChanged;
        public event EventHandler<StreamDeckSharp.KeyEventArgs> KeyStateChanged;
        public StreamDeckSharp.IStreamDeck getDeck()
        {
            if(deck == null)
            {
                try
                {
                    deck = StreamDeckSharp.StreamDeck.OpenDevice();
                    deck.ConnectionStateChanged += Deck_ConnectionStateChanged;
                    deck.KeyStateChanged += Deck_KeyStateChanged;
                    if (ConnectionChanged != null)
                    {
                        ConnectionChanged(this, new StreamDeckSharp.ConnectionEventArgs(deck.IsConnected));
                    }
                }
                catch
                {

                }
            }
            return deck;
        }

        private void Deck_KeyStateChanged(object sender, StreamDeckSharp.KeyEventArgs e)
        {
            if(KeyStateChanged!=null)
            {
                KeyStateChanged(sender, e);
            }
           // throw new NotImplementedException();
        }

        private void Deck_ConnectionStateChanged(object sender, StreamDeckSharp.ConnectionEventArgs e)
        {
            if(ConnectionChanged!=null)
            {
                ConnectionChanged(sender, e);
            }
           // throw new NotImplementedException();
        }

        public Boolean IsConnected()
        {
            if(deck == null)
            {
                return false;
            }
            else
            {
                return deck.IsConnected;
            }
        }
    }
}
