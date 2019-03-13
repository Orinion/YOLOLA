using System.Threading;
using System.Windows.Forms;

namespace FutureNNAimbot
{
    public  class MainApp
    {

        private Settings settings;
        private GameProcess gp;
        private gController gc;
        private TrainingApp ta;

        public void Start()
        {
            settings = Settings.ReadSettings();
            var nNet = NeuralNet.Create(settings.Game);

            gp = GameProcess.Create(settings);
            gc = new gController(settings);
            var dh = new DrawHelper(settings);

            ta = new TrainingApp(gp, gc, nNet, dh);

            if (nNet == null)
            {
                MessageBox.Show($"Neural net not found");
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                return;
            }

            System.IO.Directory.CreateDirectory("img/");

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                
                while (true)
                {
                    ta.ReadInput();
                    Thread.Sleep(10);
                }
            }).Start();

            while (true)
            {
                ta.Run();
            }
        }
        
    }
}
