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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Connection
    {
        private BapiHelper Broker;

        public Connection()
        {
            Broker = new BapiHelper();
        }

        public string Server 
        {
            get
            {
                return Broker.Server;
            }
        }

        public int Port
        {
            get
            {
                return Broker.Port;
            }
        }

        public string UserName
        {
            get
            {
                return Broker.UserName;
            }
        }

        public string UserSSN
        {
            get
            {
                return Broker.UserSSN;
            }
        }

        public string Division 
        { 
            get
            {
                return Broker.Division;
            }
        }

        public string SiteNumber
        {
            get
            {
                return Broker.SiteNumber;
            }
        }

        public string SiteName
        {
            get
            {
                return Broker.SiteName;
            }
        }

        public string UserDUZ
        {
            get
            {
                return Broker.UserDUZ;
            }
        }

        public string SecurityToken { get; set; }

        public Response Execute(Request request)
        {
            Response response = new Response();

            // set procedure name
            Broker.SetProperty("RemoteProcedure", request.MethodName);

            int paramIndex = 0;
            // set Parameters
            foreach(Parameter param in request.Parameters)
            {
                if (param.Type == ParameterType.List)
                {
                    Broker.SetParameter(paramIndex, (int)param.Type, "");

                    int paramItemIndex = 0;
                    foreach (string item in param.Value as List<string>)
                    {
                        Broker.SetParameterItem(paramIndex, paramItemIndex++, item);
                    }
                }
                else
                {
                    Broker.SetParameter(paramIndex, (int) param.Type, (string) param.Value);
                }

                paramIndex++;
            }

            // call remote method
            try
            {
                response.RawData = Broker.CallMethod();
            }
            catch
            {
                response.RawData = "";
            }
            return response;
        }

        public int Connect(string server, int port, string securityToken)
        {
            int ret = 0;

            SecurityToken = securityToken;

            ret = Broker.Login(server, port, securityToken);

            return ret;
        }

        public int Disconnect(string server, int port)
        {
            int ret = 0;
            ret = Broker.Logoff(server, port);
            return ret;
        }

        public void Context(string context)
        {
            Broker.ContextCreate(context);
        }
    }
}
