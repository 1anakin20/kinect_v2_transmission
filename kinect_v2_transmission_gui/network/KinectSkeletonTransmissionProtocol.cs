using System;
using System.Net.Sockets;
using System.Text;
using Microsoft.Kinect;

namespace kinect_v2_transmission_gui.network
{
    public class KinectSkeletonTransmissionProtocol
    {
        private readonly UdpClient _udpClient;

        public KinectSkeletonTransmissionProtocol(string ip, int port)
        {
            _udpClient = new UdpClient(ip, port);
        }

        public void send(Body body, int bodyNumber)
        {
            var encoded = Encode(body, bodyNumber);
            Console.WriteLine(encoded);
            var bytes = Encoding.ASCII.GetBytes(encoded);

            _udpClient.Send(bytes, bytes.Length);
        }

        private string Encode(Body body, int bodyNumber)
        {
            /*
             * Format:
             * skeleton_number,joint,x,y,z\n
             */

            var encoder = new Encoder(body, bodyNumber);

            var headEncoded = encoder.EncodeJoint(JointType.Head);
            var leftHandEncoded = encoder.EncodeJoint(JointType.HandLeft);
            var rightHandEncoded = encoder.EncodeJoint(JointType.HandRight);

            var encodedAll = $"{headEncoded}\n{leftHandEncoded}\n{rightHandEncoded}\n";

            return encodedAll;
        }
    }
}