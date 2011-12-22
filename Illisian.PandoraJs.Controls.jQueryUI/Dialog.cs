// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Illisian.PandoraJs.Utils.Extension;
using System.Collections;
using Illisian.PandoraJs.Enums;
using Illisian.PandoraJs.Utils;
using Illisian.PandoraJs.Utils.Extension.jQueryUI;
using Illisian.PandoraJs.Events;

namespace Illisian.PandoraJs.Controls.jQueryUI
{

	public class Dialog : Control
	{
		#region Private Fields
		private bool _autoOpen = true;
		private bool _disabled = false;
		private DialogButton[] _buttons = null;
		private bool _closeOnEscape = true;
		private string _closeText = "close";
		private string _dialogClass = "";
		private bool _draggable = true;
		private string _hide = null;
		private int _maxHeight = -1;
		private int _maxWidth = -1;
		private int _minHeight = 150;
		private int _minWidth = 150;
		private bool _modal = false;
		private string[] _position = new string[] { "center" };
		private bool _resizable = true;
		private string _show = null;
		private bool _stack = true;
		private string _title = "";
		private int _zIndex = 1000;
		private bool _disableCloseButton = false;
		#endregion

		#region Public Events

		public PandoraEventManager OnCreate
		{
			get
			{
				return ControlEventsManager["dialogcreate"];
			}
		}
		public PandoraEventManager OnBeforeClose
		{
			get
			{
				return ControlEventsManager["dialogbeforeclose"];
			}
		}
		public PandoraEventManager OnOpen {
			get
			{
				return ControlEventsManager["dialogopen"];
			}
		}
		public PandoraEventManager OnDialogFocus 
		{
			get
			{
				return ControlEventsManager["dialogfocus"];
			}
		}
		public PandoraEventManager OnDragStart 
		{
			get
			{
				return ControlEventsManager["dialogdragstart"];
			}
		}
		public PandoraEventManager OnDrag 
		{
			get
			{
				return ControlEventsManager["dialogdrag"];
			}
		}
		public PandoraEventManager OnDragStop 
		{
			get
			{
				return ControlEventsManager["dialogdragstop"];
			}
		}
		public PandoraEventManager OnResizeStart 
		{
			get
			{
				return ControlEventsManager["dialogresizestart"];
			}
		}
		public PandoraEventManager OnResize 
		{
			get
			{
				return ControlEventsManager["dialogresize"];
			}
		}
		public PandoraEventManager OnResizeStop 
		{
			get
			{
				return ControlEventsManager["dialogresizestop"];
			}
		}
		public PandoraEventManager OnClose
		{
			get
			{
				return ControlEventsManager["dialogclose"];
			}
		}


		#endregion


		#region Public Properties
		public bool AutoOpen { get { return _autoOpen; } set { _autoOpen = value; SetOption("autoOpen", value); } }
		public bool Disabled
		{
			get { return _disabled; }
			set
			{
				_disabled = value;
				SetOption("disabled", value);
				//TODO: Jquery 1.8 will fix this
				if (value)
				{
					jQuery.Select("#" + ControlId).Parent().Find("button,input").Attribute("disabled", "disabled");

				}
				else
				{
					jQuery.Select("#" + ControlId).Parent().Find("button,input").RemoveAttr("disabled");
				}
			}
		}
		public DialogButton[] Buttons { get { return _buttons; } set { _buttons = value; SetOption("buttons", value); } }
		public bool CloseOnEscape { get { return _closeOnEscape; } set { _closeOnEscape = value; SetOption("closeOnEscape", value); } }
		public string CloseText { get { return _closeText; } set { _closeText = value; SetOption("closeText", value); } }
		public string DialogClass { get { return _dialogClass; } set { _dialogClass = value; SetOption("dialogClass", value); } }
		public bool Draggable { get { return _draggable; } set { _draggable = value; SetOption("draggable", value); } }
		public string Hide { get { return _hide; } set { _hide = value; SetOption("hide", value); } }
		public int MaxHeight { get { return _maxHeight; } set { _maxHeight = value; SetOption("maxHeight", value); } }
		public int MaxWidth { get { return _maxWidth; } set { _maxWidth = value; SetOption("maxWidth", value); } }
		public int MinHeight { get { return _minHeight; } set { _minHeight = value; SetOption("minHeight", value); } }
		public int MinWidth { get { return _minWidth; } set { _minWidth = value; SetOption("minWidth", value); } }
		public bool Modal { get { return _modal; } set { _modal = value; SetOption("modal", value); } }
		public string[] DialogPosition { get { return _position; } set { _position = value; SetOption("position", value); } }
		public bool Resizable { get { return _resizable; } set { _resizable = value; SetOption("resizable", value); } }
		public string Show { get { return _show; } set { _show = value; SetOption("show", value); } }
		public bool Stack { get { return _stack; } set { _stack = value; SetOption("stack", value); } }
		public string Title { get { return _title; } set { _title = value; SetOption("title", value); } }
		public int ZIndex { get { return _zIndex; } set { _zIndex = value; SetOption("zIndex", value); } }
		public bool DisableCloseButton
		{
			get { return _disableCloseButton; }
			set
			{
				if (_disableCloseButton && IsInitialised && !value)
				{
					Logging.Log(LoggingType.Error, "The close button has already been disabled, you currently cannot re-enable the button once its been disabled", null);
					return;
				}
				_disableCloseButton = value;
				if (IsInitialised)
					removeCloseButton();
			}
		}

		private void removeCloseButton()
		{
			jQuery.Select("#" + ControlId).Parent().Find(".ui-dialog-titlebar-close").Remove();
		}
		#endregion


		private Dictionary AssembleOptions()
		{

			int? maxHeight = null;
			if (_maxHeight > 0)
				maxHeight = _maxHeight;

			int? maxWidth = null;
			if (_maxWidth > 0)
				maxWidth = _maxWidth;


			return new Dictionary(
				"disabled", _disabled,
				"autoOpen", _autoOpen,
				"buttons", _buttons,
				"closeOnEscape", _closeOnEscape,
				"closeText", _closeText,
				"dialogClass", _dialogClass,
				"draggable", _draggable,
				"height",int.Parse(Height.Replace("px", "")),
				"hide", _hide,
				"maxHeight", maxHeight,
				"maxWidth", maxWidth,
				"minHeight", _minHeight,
				"minWidth", _minWidth,
				"modal", _modal,
				"position", _position,
				"resizable", _resizable,
				"show", _show,
				"stack", _stack,
				"title", _title,
				"width", int.Parse(Width.Replace("px", "")),
				"zIndex", _zIndex
				);
			
		}
		protected override void Control_SetProperties()
		{
			if (IsRendered)
			{
				
				Dictionary options = AssembleOptions();
				Logging.Log(LoggingType.Debug, "Dialog settings", new object[] { options });

				jQueryExtension.Select<jQueryUIObject>("#" + ControlId).Dialog(options);
				
				base.Control_SetProperties();

				if (DisableCloseButton)
				{
					removeCloseButton();
				}
				OnClose.Add(delegate
				{
					Logging.Log(LoggingType.Debug, "Firing Unload from OnClose event", null);
					Unload();
				}, null);
				ControlEventsManager.Rebind();
			}
		}


		protected override void Control_Render()
		{
			jQueryExtension.Select<jQueryUIObject>("#" + Parent.ControlId).Append("<div id='" + ControlId + "'></div>");
			jQueryExtension.Select<jQueryUIObject>("#" + ControlId).Dialog(AssembleOptions());
		}

		protected override void Control_Unload()
		{
			jQueryExtension.Select<jQueryUIObject>("#" + ControlId).Dialog("close");
			base.Control_Unload();
		}

		protected override void SetDisplayType()
		{
			if (IsRendered)
			{
				SetParentDisplayType();
			}
		}
		protected virtual void SetParentDisplayType()
		{
			if (IsRendered)
			{
				string disp = "inherit";
				switch (Display)
				{
					case DisplayType.Inherit:
						disp = "inherit";
						break;
					case DisplayType.Inline:
						disp = "inline";
						break;
					case DisplayType.None:
						disp = "none";
						break;
				}
				jQuery.Select("#" + ControlId).Parent().CSS("display", disp);
			}
		}
		protected void SetOption(string name, object value)
		{
			if (IsRendered)
			{
				jQueryExtension.Select<jQueryUIObject>("#" + ControlId).Dialog("option", name, value);
			}
		}
		protected object GetOption(string name)
		{
			if (IsRendered)
			{
				return jQueryExtension.Select<jQueryUIObject>("#" + ControlId).Dialog("option", name);
			}
			return null;
		}
	}
}
