/** 
 * This File is part of the NDI Library
 * Copyright 2009-2016 Bogdan COSTEA <bogdan@costea.us>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Ndi.Tooling.Logging;
using Ndi.Attributes;

namespace Ndi.Component.Locator
{
	class XmlLocator : ILocator
	{
		static readonly ILogger Log = LogFactory.GetLog(typeof(XmlLocator));

		

		public List<IComponent> Locate()
		{
			return Locate(AbstractApplicationContext.DEFAULT_CONTEXT_FILE_NAME);
		}

		public List<IComponent> Locate(string xmlFile)
		{
			List<IComponent> components = new List<IComponent>();

			if (Log.IsDebugEnabled())
			{
				Log.Debug("Locating components defined in XML resource " + xmlFile);
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
					ComponentField componentField = new ComponentField((string)fieldElement.Attribute("Name"), InjectMethod.Field);
					fields.Add(componentField);
				}

                IEnumerable<XElement> xmlParameterElements = componentElement.Descendants("parameter");
                foreach (XElement parameterElement in xmlParameterElements)
                {
                    ComponentField componentField = new ComponentField((string)parameterElement.Attribute("Name"), InjectMethod.Constructor);

                    string value = (string) parameterElement.Attribute("Value");
                    if (value!=null)
                    {
                        componentField.Value = value;
                    }
                    
                    fields.Add(componentField);
                }
			}

			return fields;
		}
	}
}
