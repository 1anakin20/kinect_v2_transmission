using kinect_v2_transmission_gui.network;

namespace kinect_v2_transmission_gui.kinect
{

    using Microsoft.Kinect;

    public class Kinect
    {
        private KinectSensor _sensor;
        private BodyFrameReader _reader;
        private KinectSkeletonTransmissionProtocol _KSTP;

        public Kinect(KinectSkeletonTransmissionProtocol kstp)
        {
            LoadKinect();
            _KSTP = kstp;
        }

        public void Close()
        {
            _sensor.Close();
        }

        private void LoadKinect()
        {
            _sensor = KinectSensor.GetDefault();
            _reader = _sensor.BodyFrameSource.OpenReader();
            _reader.FrameArrived += KinectSkeletonFrameReady;
            _sensor.Open();
        }

        private void KinectSkeletonFrameReady(object sender, BodyFrameArrivedEventArgs e)
        {
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    var bodies = new Body[frame.BodyCount];
                    frame.GetAndRefreshBodyData(bodies);
                    for (int i = 0; i < bodies.Length; i++)
                    {
                        var body = bodies[i];
                        if (body != null)
                        {
                            if (body.IsTracked)
                            {
                                _KSTP.send(body, i);
                            }
                        }
                    }
                }
            }
        }
    }
}