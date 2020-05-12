using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTools
{
	public interface IPropertyDialogPage
	{
		void BeforeDeactivated(object dataObject);
		void BeforeActivated(object dataObject);
	}
}
