/*
 * LiveInformationBox 2.0
 * by Jayson Ragasa aka Nullstring
 * Baguio City, Philippines
 * -
 * 
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace LiveInformationBox
{
    public class XMLInformationReader
    {
        string _xmlInfoFile = string.Empty;

        public XMLInformationReader(string XMLInfoFile)
        {
            this._xmlInfoFile = XMLInfoFile;
        }

        public InformationDetails Read(string ControlName)
        {
            InformationDetails infoDet = null;

            XmlDocument d = new XmlDocument();

            d.Load(_xmlInfoFile);

            string xpath = "/ControlInformations/control[@name=\"{0}\"]";
            xpath = string.Format(xpath, ControlName);

            XmlNode thisNode = d.SelectSingleNode(xpath);

            if (thisNode != null)
            {
                infoDet = new InformationDetails();

                XmlNode node;

                node = thisNode.SelectSingleNode("./title/text()");
                if (node != null)
                {
                    infoDet.Title = node.Value;
                }

                node = thisNode.SelectSingleNode("./short_description/text()");
                if (node != null)
                {
                    infoDet.ShortDescription = node.Value;
                }

                node = thisNode.SelectSingleNode("./infos/text()");
                if (node != null)
                {
                    infoDet.Information = node.Value; 
                }
            }

            d = null;

            return infoDet;
        }
    }

    public class InformationDetails
    {
        string _title = string.Empty;
        string _short_description = string.Empty;
        string _infos = string.Empty;

        public InformationDetails()
        {
        }

        public string Title
        {
            set
            {
                this._title = value;
            }
            get
            {
                return this._title;
            }
        }

        public string ShortDescription
        {
            set
            {
                this._short_description = value;
            }
            get
            {
                return this._short_description;
            }
        }

        public string Information
        {
            set
            {
                this._infos = value;
            }
            get
            {
                return this._infos;
            }
        }
    }
}
