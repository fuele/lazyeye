﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Threading;
using LazyEye.Views;

namespace LazyEye.Monitors
{
    public class PingMonitor
    {
        private Thread thread;
        private bool isRunning;

        //Events
        public event EventHandler<PingReplyReceivedEventArgs> PingReplyRecieved;

        private PingReply SendPing()
        {
            Ping pinger = new Ping();
            PingReply reply = pinger.Send("8.8.8.8");
            System.Diagnostics.Debug.WriteLine(reply.RoundtripTime);
            return reply;
        }

        public void Start()
        {
            thread = new Thread(Run);
            isRunning = true;
            thread.Start();
            
        }

        private void Run()
        {
            while (isRunning)
            {
                PingReply reply = SendPing();

                PingReplyReceivedEventArgs args = new PingReplyReceivedEventArgs();
                args.PingReply = reply;

                if(PingReplyRecieved != null)
                {
                    PingReplyRecieved(this, args);
                }


                Thread.Sleep(1000);
            }
        }




    }


    //Event Args
    public class PingReplyReceivedEventArgs : EventArgs
    {
        public PingReply PingReply { get; set; }
    }
}