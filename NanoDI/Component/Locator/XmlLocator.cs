using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Ndi.Tooling.Logging;

namespace Ndi.Component.Locator
{
	class XmlLocator : ILocator
	{
		static ILogger log = LogFactory.GetLog(typeof(XmlLocator));

		public static readonly string DEFAULT_CONTEXT_FILE_NAME = @"components.xml";

		public List<IComponent> Locate()
		{
			return Locate(DEFAULT_CONTEXT_FILE_NAME);
		}

		public List<IComponent> Locate(string xmlFile)
		{
			List<IComponent> components = new List<IComponent>();

			if (log.IsDebugEnabled())
			{
				log.Debug("Locating components defined in XML resource " + xmlFile);
			}

			XDocument feedXML = XDocument.Load(xmlFile);

			IEnumerable<XElement> xmlComponentElements = from component
															 in feedXML.Descendants("component")
														 select component;

			foreach (XElement componentElement in xmlComponentElements)
			{
				Component component = new Component((string)componentElement.Attribute("Name"),
													UtilityToolbox.GetType((string)componentElement.Attribute("Type")),
													UtilityToolbox.GetScope((string)componentElement.Attribute("Scope")));

				component.Fields = getFields(componentElement);

				components.Add(component);
			}


			return components;
		}

		List<ComponentField> getFields(XElement componentElement)
		{
			List<ComponentField> fields = new List<ComponentField>();

			if (componentElement.HasElements)
			{
				IEnumerable<XElement> xmlFieldElements = componentElement.Descendants("field");
				foreach (XElement fieldElement in xmlFieldElements)
				{
					ComponentField componentField = new ComponentField((string)fieldElement.Attribute("Name"));
					fields.Add(componentField);
				}
			}

			return fields;
		}
	}
}
