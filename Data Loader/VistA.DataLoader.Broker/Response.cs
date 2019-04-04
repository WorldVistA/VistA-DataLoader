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
    using System.Collections;

    public class ResponseToken
    {
        public ResponseToken(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public IEnumerable<ResponseToken> Split(string delimiter)
        {
            List<ResponseToken> responseTokens = null;

            string[] tokens = Value.Split(new string[] { delimiter }, StringSplitOptions.None);
            if (tokens != null)
            {
                responseTokens = new List<ResponseToken>();
                foreach (string token in tokens)
                {
                    responseTokens.Add(new ResponseToken(token));
                }

            }

            return responseTokens;
        }
    }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Response
    {
        public Response()
        {
        }

        public Response(string rawData)
        {
            this.RawData = rawData;
        }

        public string RPCName { get; set; }

        public string RawData { get; set; }

        public IEnumerable<ResponseToken> Split(string delimiter)
        {
            return new ResponseToken(RawData).Split(delimiter);
        }
    }
}
