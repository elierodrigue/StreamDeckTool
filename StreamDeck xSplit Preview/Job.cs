using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace StreamDeck_xSplit_Preview
{
    public abstract class Job
    {
        [Newtonsoft.Json.JsonIgnore]
        public Bitmap theBitmap;
        public Boolean enabled = true;
        public int ButtonId;
        public void Draw(StreamDeckSharp.IStreamDeck deck)
        {
            if (theBitmap != null)
            {
                if (deck != null)
                {
                    deck.SetKeyBitmap(ButtonId, StreamDeckSharp.KeyBitmap.FromRawBitmap(StreamDeckSharp.Extensions.StreamDeckFullScreenDrawingExtension.GetSmallRgbArray(theBitmap)));
                }
            }
        }
        public abstract void Process(StreamDeckSharp.IStreamDeck deck);
        protected abstract void ProcessEvent(object sender, StreamDeckSharp.KeyEventArgs e);
        public void DeckEvent(object sender, StreamDeckSharp.KeyEventArgs e)
        {
            if (e.Key == ButtonId)
            {
                ProcessEvent(sender, e);
            }
           
        }
    }
}
