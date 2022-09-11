using System;
using System.Windows.Forms;
using kinect_v2_transmission_gui.kinect;
using kinect_v2_transmission_gui.network;

namespace kinect_v2_transmission_gui
{
    public partial class MainWindow : Form
    {
        private bool _isConnected = false;
        private Kinect _kinect;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {

            if (_isConnected)
            {
                _kinect.Close();
                btnConnect.Text = "Connect";
                _isConnected = false;
            }
            else
            {
                var ip = txtIP.Text;
                var port = int.Parse(txtPort.Text);
                var kstp = new KinectSkeletonTransmissionProtocol(ip, port);
                _kinect = new Kinect(kstp);
                btnConnect.Text = "Disconnect";
                _isConnected = true;
            }
        }
    }
}
