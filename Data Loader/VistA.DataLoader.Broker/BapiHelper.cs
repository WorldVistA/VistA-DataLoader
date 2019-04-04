// VistA Data Loader 2.0
// Copyright (C) 2012 Johns Hopkins University
// 
// VistA Data Loader is provided by the Johns Hopkins University School of
// Nursing, and funded by the Department of Health and Human Services, Office
// of the National Coordinator for Health Information Technology under Award
// Number #1U24OC000013-01.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
//
// REVISION HISTORY
// ----------------
// V.1.0 JUNE 2012 made possible by JHU, School of Nursing (see above)
// V.2.0 UPDATE JUNE 2014 made possible by University of Michigan
// V.2.1 UPDATE NOV 2014 made possible by Oroville Hospital, to support QRDA.
// V.2.2 Incrimental update: bug fixes, etc.
// V.2.5 Continued incrimental updates, bug fixes (2015)
//
// DECLARATIONS
// -------------------------------
// This software package is NOT for use in any production or clinical setting.
// The software has not been designed, coded, or tested for use in any clinical
// or production setting.
// 
// This should be considered a work in progress.  If folks are interested in 
// collaborating on future versions of the utility set should please contact 
// Mike Stark (starklogic@gmail.com) or ISI GROUP, LLC, Bethesda, MD.
//

namespace DataLoader.Broker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;
    using System.ComponentModel;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal class BapiHelper
    {
        [DllImport("Bapi32.dll", EntryPoint = "RpcbCreate", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr RpcbCreate();

        [DllImport("Bapi32.dll", EntryPoint = "RpcbFree", CallingConvention = CallingConvention.StdCall)]
        public static extern void RpcbFree(IntPtr broker);

        [DllImport("Bapi32.dll", EntryPoint = "RpcbGetServerInfo", CallingConvention = CallingConvention.StdCall)]
        public static extern void RpcbGetServerInfo(IntPtr server, IntPtr port, ref int ret);

        [DllImport("Bapi32.dll", EntryPoint = "RpcbPropSet", CallingConvention = CallingConvention.StdCall)]
        public static extern void RpcbPropSet(IntPtr broker, IntPtr name, IntPtr value);

        [DllImport("Bapi32.dll", EntryPoint = "RpcbPropGet", CallingConvention = CallingConvention.StdCall)]
        public static extern void RpcbPropGet(IntPtr broker, IntPtr name, IntPtr value);

        [DllImport("Bapi32.dll", EntryPoint = "RpcbUserPropSet", CallingConvention = CallingConvention.StdCall)]
        public static extern void RpcbUserPropSet(IntPtr broker, IntPtr name, IntPtr value);

        [DllImport("Bapi32.dll", EntryPoint = "RpcbUserPropGet", CallingConvention = CallingConvention.StdCall)]
        public static extern void RpcbUserPropGet(IntPtr broker, IntPtr name, IntPtr value);

        [DllImport("Bapi32.dll", EntryPoint = "RpcbCall", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr RpcbCall(IntPtr broker, IntPtr value);

        [DllImport("Bapi32.dll", EntryPoint = "RpcbCreateContext", CallingConvention = CallingConvention.StdCall)]
        public static extern void RpcbCreateContext(IntPtr broker, IntPtr paramValue);

        [DllImport("Bapi32.dll", EntryPoint = "RpcbParamSet", CallingConvention = CallingConvention.StdCall)]
        public static extern void RpcbParamSet(IntPtr broker, int paramIndex, int paramType, IntPtr paramValue);

        [DllImport("Bapi32.dll", EntryPoint = "RpcbMultSet", CallingConvention = CallingConvention.StdCall)]
        public static extern void RpcbMultSet(IntPtr broker, int paramIndex, IntPtr paramItemIndex, IntPtr paramValue);

        private IntPtr Broker { get; set; }

        public BapiHelper()
        {
            Broker = RpcbCreate();
        }

        public string Server { get; set; }

        public int Port { get; set; }

        public string Division { get; set; }

        public string SiteNumber { get; set; }

        public string SiteName { get; set; }

        public string UserName { get; set; }

        public string UserSSN { get; set; }

        public bool Connected { get; set; }

        public string UserDUZ { get; set; }

        public void SetProperty(string name, string value)
        {
            IntPtr namePtr = (IntPtr) Marshal.StringToHGlobalUni(name);
            IntPtr valuePtr = (IntPtr) Marshal.StringToHGlobalUni(value);

            RpcbPropSet(this.Broker, namePtr, valuePtr);

            Marshal.FreeHGlobal(namePtr);
            Marshal.FreeHGlobal(valuePtr);
        }

        public string GetProperty(string name)
        {
            IntPtr namePtr = (IntPtr)Marshal.StringToHGlobalUni(name);
            IntPtr valuePtr = Marshal.AllocHGlobal(256);
            string value;

            RpcbPropGet(this.Broker, namePtr, valuePtr);
            value = Marshal.PtrToStringUni(valuePtr);

            Marshal.FreeHGlobal(namePtr);
            Marshal.FreeHGlobal(valuePtr);

            return (value);
        }

        public void SetUserProperty(string name, string value)
        {
            IntPtr namePtr = (IntPtr)Marshal.StringToHGlobalUni(name);
            IntPtr valuePtr = (IntPtr)Marshal.StringToHGlobalUni(value);

            RpcbUserPropSet(this.Broker, namePtr, valuePtr);

            Marshal.FreeHGlobal(namePtr);
            Marshal.FreeHGlobal(valuePtr);
        }

        public string GetUserProperty(string name)
        {
            IntPtr namePtr = (IntPtr)Marshal.StringToHGlobalUni(name);
            IntPtr valuePtr = Marshal.AllocHGlobal(1024);
            string value;
 
            RpcbUserPropGet(this.Broker, namePtr, valuePtr);
            value = Marshal.PtrToStringUni(valuePtr);

            Marshal.FreeHGlobal(namePtr);
            Marshal.FreeHGlobal(valuePtr);

            return value;
        }

        public void ContextCreate(string paramValue)
        {
            IntPtr paramValuePtr = (IntPtr)Marshal.StringToHGlobalUni(paramValue);
            RpcbCreateContext(this.Broker, paramValuePtr);
            Marshal.FreeHGlobal(paramValuePtr);
        }

        public void SetParameter(int paramIndex, int paramType, string paramValue)
        {
            IntPtr paramValuePtr = (IntPtr)Marshal.StringToHGlobalUni(paramValue);

            RpcbParamSet(this.Broker, paramIndex, paramType, paramValuePtr);

            Marshal.FreeHGlobal(paramValuePtr);
        }

        public void SetParameterItem(int paramIndex, int paramItemIndex, string paramValue)
        {
            IntPtr paramValuePtr = (IntPtr)Marshal.StringToHGlobalUni(paramValue);
            IntPtr paramItemIndexPtr = (IntPtr)Marshal.StringToHGlobalUni(paramItemIndex.ToString());

            RpcbMultSet(this.Broker, paramIndex, paramItemIndexPtr, paramValuePtr);

            Marshal.FreeHGlobal(paramValuePtr);
            Marshal.FreeHGlobal(paramItemIndexPtr);
        }

        public int Logoff(string server, int port)
        {
            int ret = 0;

            Server = server;
            Port = port;

            // set connection properties
            SetProperty("Server", Server);
            SetProperty("ListenerPort", Port.ToString());

            // set connected propery to TRUE to connect
            SetProperty("Connected", "0");

            //check connection property
            Connected = GetProperty("Connected") == "1";
            if (Connected == false)
            {
                ret = 1;
            }

            return ret;
        }

        public int Login(string server, int port, string securityToken)
        {
            int ret = 0;

            Server = server;
            Port = port;
            // set connection properties
            SetProperty("Server", Server);
            SetProperty("ListenerPort", Port.ToString());

            // set connected propery to TRUE to connect
            SetProperty("Connected", "1");

            // check if connection succeeded
            Connected = GetProperty("Connected") == "1";

            if (Connected)
            {
                // get properties
                UserName = GetUserProperty("NAME");
                string[] tokens = GetUserProperty("DIVISION").Split('^');
                SiteNumber = tokens[0];
                SiteName = tokens[1];
                Division = tokens[2];
                UserDUZ = GetUserProperty("DUZ");
            }
            else
                ret = 1;

            return ret;
        }

        public string CallMethod()
        {
            string response = "";
            IntPtr responsePtr = Marshal.AllocHGlobal(65536);
            try
            {

                IntPtr text = RpcbCall(this.Broker, responsePtr);
                response = Marshal.PtrToStringUni(responsePtr);
                Marshal.FreeHGlobal(responsePtr);
            }
            catch
            {
                //
            }
            return response;
        }
    }
}
