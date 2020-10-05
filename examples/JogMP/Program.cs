using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JogMP
{
    struct Pose {
        public double X,Y,Z,A,B,C;

        public override string ToString() {
            return $"X{X} Y{Y} Z{Z} A{A} B{B} C{C}";
        }

        public string ToPoseString() {
            return ToString().Replace(',', '.');
        }

        public static Pose operator +(Pose pose, Pose other) {
            return new Pose() {
                X = pose.X + other.X,
                Y = pose.Y + other.Y,
                Z = pose.Z+ other.Z,
                A = pose.A + other.A,
                B = pose.B + other.B,
                C = pose.C + other.C
            };
        }
    }

    class Program
    {
        private static Pose _pose;

        private static Pose ParsePose(string mrcPose) {
            var mrcPoseArray = mrcPose
                .Split(':', 'X', 'Y', 'Z', 'A', 'B', 'C')
                .Where(p => p != String.Empty)
                .Select(p => Double.Parse(
                    p.Replace('.', ',') 
                    ))
                .ToArray();
            return new Pose() {X=mrcPoseArray[0], Y=mrcPoseArray[1], Z=mrcPoseArray[2], A=mrcPoseArray[3], B=mrcPoseArray[4], C=mrcPoseArray[5]};
        }

        static void Main(string[] args)
        {
            using (var p = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One)) {
                p.Open();
                p.WriteLine(":S E XYZABC \r");
                string position = p.ReadLine();

                _pose = ParsePose(position);
                Console.WriteLine(_pose);

                MoveJoints(p);
                return;


                Move(p, new Pose() {Z=1});

                int k;
                while( (k = Console.Read()) != 'x') {
                    switch(k) {
                        case 'w': 
                            Move(p, new Pose() {X=-1});
                            break;
                        case 's':
                            Move(p, new Pose() {X=1});
                            break;
                         case 'a': 
                            Move(p, new Pose() {Y=-1});
                            break;
                        case 'd':
                            Move(p, new Pose() {Y=1});
                            break;
                         case 'q': 
                            Move(p, new Pose() {Z=-1});
                            break;
                        case 'e':
                            Move(p, new Pose() {Z=1});
                            break;
                         case 'z': 
                            Move(p, new Pose() {A=-10});
                            break;
                        case 'r':
                            Move(p, new Pose() {A=10});
                            break;
                         case 'f': 
                            Move(p, new Pose() {B=-10});
                            break;
                        case 'h':
                            Move(p, new Pose() {B=10});
                            break;
                         case 't': 
                            Move(p, new Pose() {C=-10});
                            break;
                        case 'g':
                            Move(p, new Pose() {C=10});
                            break;
                    }
                }
            }
            
        }

        private static void MoveJoints(SerialPort p)
        {
            int k = 0;

            Task.Run(() => { k = Console.Read(); });

            Stopwatch w = new Stopwatch();
            while(k != 'x') {

                w.Start();
                p.WriteLine(":S E M03 R0 10 R1 0 R2 20 R3 -30 R4 0 R5 30 \r");
                //p.ReadLine();
                w.Stop();

                Console.WriteLine($"Duration:{w.Elapsed}");

                w.Reset();
                Thread.Sleep(6);
                
                w.Start();
                p.WriteLine(":S E M03 R0 12 R1 2 R2 20 R3 -28 R4 0 R5 30 \r");
                //p.ReadLine();
                w.Stop();

                Console.WriteLine($"Duration:{w.Elapsed}");

                w.Reset();
                Thread.Sleep(6);
            }
        }

        private static void Move(SerialPort p, Pose pose)
        {
            _pose += pose;
            p.WriteLine($":S E M01 {_pose.ToPoseString()}\r");
        }
    }
}
