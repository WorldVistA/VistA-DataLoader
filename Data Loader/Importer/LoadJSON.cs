/**
 * 
 * VistA Data Loader 2.0
 *
 * Copyright (C) 2012 Johns Hopkins University
 *
 * VistA Data Loader is provided by the Johns Hopkins University School of
 * Nursing, and funded by the Department of Health and Human Services, Office
 * of the National Coordinator for Health Information Technology under Award
 * Number #1U24OC000013-01.
 *
 *Licensed under the Apache License, Version 2.0 (the "License");
 *you may not use this file except in compliance with the License.
 *You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 *Unless required by applicable law or agreed to in writing, software
 *distributed under the License is distributed on an "AS IS" BASIS,
 *WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *See the License for the specific language governing permissions and
 *limitations under the License.
 * 
 * Date Created: 5/30/2012
 * Developer:  Mike Stark
 * Description: 
 * 
 */

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using DataLoader.Common.Model;
using Newtonsoft.Json;

namespace DataLoader
{
    class LoadJSON
    {
        public static DataSet ImportJSON(string filepath)
        {
            string json;
            using (StreamReader r = new StreamReader(filepath))
            {
                json = r.ReadToEnd();
            }
            //string jsontext = json.Replace(" ", "");
            XmlDocument doc = JsonConvert.DeserializeXmlNode(json);
            dynamic array = JsonConvert.DeserializeObject(json);
            foreach (var item in array)
            {
                string node;
                string attributes;
                try
                {
                    node = item.Name;
                    attributes = item.Value.ToString();
                    attributes = attributes.Replace("\"", "");
                    string[] fields = Regex.Split(attributes, "\r\n");
                }
                catch
                {
                }
            }
            //
            DataSet ds = new DataSet();
            return ds;
        }
    }
}
