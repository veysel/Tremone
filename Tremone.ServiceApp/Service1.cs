﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Tremone.ServiceApp
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        TremoneOperation tremone = new TremoneOperation();

        protected override void OnStart(string[] args)
        {
            tremone.Start();
        }

        protected override void OnStop()
        {
            tremone.Stop();
        }
    }
}
