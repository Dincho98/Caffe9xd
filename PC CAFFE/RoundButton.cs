using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    internal class RoundButton : Button
    {
        public int cornerRound { get; set; }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            GraphicsPath gpath = new GraphicsPath();
            //gpath.AddPie(
            base.OnPaint(pevent);
        }
    }
}