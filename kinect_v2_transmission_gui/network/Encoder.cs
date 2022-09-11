using System;
using System.Runtime.CompilerServices;
using Microsoft.Kinect;

namespace kinect_v2_transmission_gui.network
{
    public class Encoder
    {
        private Body _body;
        private int _bodyNumber;

        public Encoder(Body body, int bodyNumber)
        {
            _body = body;
            _bodyNumber = bodyNumber;
        }

        public string EncodeJoint(JointType type)
        {
            // Inverse coordinates for the Z axis of the head due to the Kinect tracking space
            // int correction = 1;
            // if (type == JointType.Head || type == JointType.HandLeft || type == JointType.HandRight)
            // {
            //     correction = -1;
            // }

            var joint = _body.Joints[type];
            return $"{_bodyNumber},{type},{joint.Position.X},{joint.Position.Y},{-joint.Position.Z}";
 
        }
    }
}