// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Illisian.PandoraJs.Utils.Extension;
using Illisian.PandoraJs.Utils;
using Illisian.PandoraJs.Utils.Extension.jQueryUI;

namespace Illisian.PandoraJs.Controls.jQueryUI
{

	public class jqButton : Button
	{
		private bool _enabled = true;

		public override bool Enabled
		{
			get { return _enabled; }
			set
			{
				_enabled = value;

				if (_enabled)
				{
					RemoveAttribute("disabled");
					jQueryExtension.Select<jQueryUIObject>("#" + ControlId).RemoveClass("ui-state-disabled");
				}
				else
				{
					SetAttribute("disabled", "disabled");
					jQueryExtension.Select<jQueryUIObject>("#" + ControlId).AddClass("ui-state-disabled");
				}
			}
		}

		protected override void Control_Render()
		{
			base.Control_Render();
			jQueryExtension.Select<jQueryUIObject>("#" + ControlId).Button();
			Enabled = _enabled;
		}



	}
}
