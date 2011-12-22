// TAb.cs
//

using System;
using System.Collections.Generic;
using System.Collections;
using jQueryApi;
using Illisian.PandoraJs.Enums;
using Illisian.PandoraJs.Utils;
using Illisian.PandoraJs.Utils.Extension.jQueryUI;
using Illisian.PandoraJs.Events;
using Illisian.PandoraJs.Utils.Extension;
namespace Illisian.PandoraJs.Controls.jQueryUI
{
	public class Tabs : Control
	{

		public Tabs()
		{
			OnInitialised = OnTabsInitialised;
		}

		#region Private Fields
		private Dictionary _ajaxOptions = null;
		private bool _cache = false;
		private bool _collapsible = false;
		private object _cookie = null;
		private bool _deselectable = false;
		private Array _disabledTabs = new Array();
		private string _event = "click";
		private Dictionary _fx = null;
		private string _idPrefix = "ui-tabs-";
		private string _panelTemplate = "<div></div>";
		private int _selected = 0;
		private string _spinner = "<em>Loading&#8230;</em>";
		private string _tabTemplate = "<li id=''><a href='#{href}'><span>#{label}</span></a><span class='ui-icon ui-icon-close'>Remove Tab</span></li>";
		#endregion

		#region Public Events

		public PandoraEventManager OnCreate
		{
			get
			{
				return ControlEventsManager["tabscreate"];
			}
		}
		public PandoraEventManager OnTabSelect 
		{
			get
			{
				return ControlEventsManager["tabsselect"];
			}
		}
		public PandoraEventManager OnLoad 
		{
			get
			{
				return ControlEventsManager["tabsload"];
			}
		}
		public PandoraEventManager OnShow 
		{
			get
			{
				return ControlEventsManager["tabsshow"];
			}
		}
		public PandoraEventManager OnAdd 
		{
			get
			{
				return ControlEventsManager["tabsadd"];
			}
		}
		public PandoraEventManager OnRemove 
		{
			get
			{
				return ControlEventsManager["tabsremove"];
			}
		}
		public PandoraEventManager OnEnable
		{
			get
			{
				return ControlEventsManager["tabsenable"];
			}
		}
		public PandoraEventManager OnDisable
		{
			get
			{
				return ControlEventsManager["tabsdisable"];
			}
		}
		#endregion

		#region Public Properties

		public Dictionary AjaxOptions
		{
			get
			{
				if (!IsRendered)
					return _ajaxOptions;
				return (Dictionary)GetOption("ajaxOptions");
			}
			set { _ajaxOptions = value; SetOption("ajaxOptions", value); }
		}
		public bool Cache
		{
			get
			{
				if (!IsRendered)
					return _cache;
				return (bool)GetOption("cache");
			}
			set { _cache = value; SetOption("cache", value); }
		}
		public bool Collapsible
		{
			get
			{
				if (!IsRendered)
					return _collapsible;
				return (bool)GetOption("collapsible");
			}
			set { _collapsible = value; SetOption("collapsible", value); }
		}
		public object Cookie
		{
			get
			{
				if (!IsRendered)
					return _cookie;
				return GetOption("_cookie");
			}
			set { _cookie = value; SetOption("cookie", value); }
		}
		public bool Deselectable
		{
			get
			{
				if (!IsRendered)
					return _deselectable;
				return (bool)GetOption("_deselectable");
			}
			set { _deselectable = value; SetOption("deselectable", value); }
		}
		public Array DisabledTabs
		{
			get
			{
				if (!IsRendered)
					return _disabledTabs;
				return (int[])GetOption("disabledTabs");
			}
			set { _disabledTabs = value; SetOption("disabledTabs", value); }
		}
		public string Event
		{
			get
			{
				if (!IsRendered)
					return _event;
				return (string)GetOption("event");
			}
			set { _event = value; SetOption("event", value); }
		}
		public Dictionary Fx
		{
			get
			{
				if (!IsRendered)
					return _fx;
				return (Dictionary)GetOption("fx");
			}
			set { _fx = value; SetOption("fx", value); }
		}
		public string IdPrefix
		{
			get
			{
				if (!IsRendered)
					return _idPrefix;
				return (string)GetOption("idPrefix");
			}
			set { _idPrefix = value; SetOption("idPrefix", value); }
		}
		public string PanelTemplate
		{
			get
			{
				if (!IsRendered)
					return _panelTemplate;
				return (string)GetOption("panelTemplate");
			}
			set { _panelTemplate = value; SetOption("panelTemplate", value); }
		}
		public int Selected
		{
			get
			{
				if (!IsRendered)
					return _selected;
				return (int)GetOption("selected");
			}
			set { _selected = value; SetOption("selected", value); }
		}
		public string Spinner
		{
			get
			{
				if (!IsRendered)
					return _spinner;
				return (string)GetOption("spinner");
			}
			set { _spinner = value; SetOption("spinner", value); }
		}
		public string TabTemplate
		{
			get
			{
				if (!IsRendered)
					return _tabTemplate;
				return (string)GetOption("tabTemplate");
			}
			set { _tabTemplate = value; SetOption("tabTemplate", value); }
		}

		#endregion


		#region Protected Functions
		private Dictionary AssembleOptions()
		{
			return new Dictionary(
				"ajaxOptions", _ajaxOptions,
				"cache", _cache,
				"collapsible", _collapsible,
				"cookie", _cookie,
				"deselectable", _deselectable,
				"disabled", _disabledTabs,
				"event", _event,
				"fx", _fx,
				"idPrefix", _idPrefix,
				"panelTemplate", _panelTemplate,
				"selected", _selected,
				"spinner", _spinner,
				"tabTemplate", _tabTemplate);

		}
		protected override void Control_SetProperties()
		{
			if (IsRendered)
			{
				base.Control_SetProperties();
			}
		}


		protected override void Control_Render()
		{

			string toAppend = "<div id='" + ControlId + "'><ul id='" + ControlId + "-tabs'></ul></div>";
			jQueryExtension.Select<jQueryUIObject>("#" + Parent.ControlId).Append(toAppend);
		}
		protected override void Control_Unload()
		{
			base.Control_Unload();

		}
		protected void OnTabsInitialised(Control c)
		{
			Dictionary options = AssembleOptions();
			Logging.Log(LoggingType.Debug, "Tab settings", new object[] { options });
			jQueryExtension.Select<jQueryUIObject>("#" + ControlId).Tabs(options);
		}

		protected void SetOption(string name, object value)
		{
			if (IsRendered)
			{
				jQueryExtension.Select<jQueryUIObject>("#" + ControlId).Tabs("option", name, value);
			}
		}
		protected object GetOption(string name)
		{
			if (IsRendered)
			{
				return jQueryExtension.Select<jQueryUIObject>("#" + ControlId).Tabs("option", name);
			}
			return null;
		}
		public override string[] CssFiles
		{
			get
			{
				return new string[] { "jquery.ui.tabs.extra" };
			}
		}

		#endregion

		public override void AddChild(Control control)
		{
			if (!(control is TabItem))
			{
				Logging.Error("You cannot add anything but a TabItem directly to a Tab control", new object[] { this, control});
				return;
			}
			
			base.AddChild(control);
		}
	}

	public delegate bool jQueryTabActionHandler();
	public class TabItem : Control
	{
		public TabItem()
		{
			Position = PositionType.Relative;
		}
		private const string _removeTab = "<span id='btnRemoveTab' class='ui-icon ui-icon-close'>Remove Tab</span>";
		private string _title;
		private bool _isClosable;
		public string Title
		{
			get
			{
				return _title;
			}
			set { _title = value; }
		}
		public bool IsClosable
		{
			get
			{
				return _isClosable;
			}
			set
			{
				if (value && !_isClosable)
				{

					//TODO: add/remove close icon
				}

				_isClosable = value;
				
			}
		}

		private jQueryTabActionHandler _ontabclose;

		public jQueryTabActionHandler OnTabClose
		{
			get
			{
				return _ontabclose;
			}
			set
			{
				_ontabclose = value;
			}

		}

		protected override void Control_Render()
		{
			string template = "<li id='{0}-title' class='tab-title'><a href='#{1}'>{2}</a>{3}</li>";
			string remTab = _isClosable ? _removeTab : "";
			string preInitTemplate = String.Format(template, ControlId, ControlId, _title, remTab);
			string Template = String.Format(template, ControlId, "{href}", "#{label}", remTab);

			jQueryExtension.Select<jQueryUIObject>("#" + Parent.ControlId).Tabs("option", "tabTemplate", Template);
			jQuery.Select("#" + Parent.ControlId).Append("<div id='" + ControlId + "'></div>");
			if (!Parent.IsInitialised)
			{
				jQuery.Select("#" + Parent.ControlId + "-tabs").Append(preInitTemplate);
			}
			else
			{
				jQueryExtension.Select<jQueryUIObject>("#" + Parent.ControlId).Tabs("add", "#" + ControlId, Title);
			}
			if (!_isClosable)
			{
				jQuery.Select("#" + ControlId + "-title>#btnRemoveTab").Remove();
			}
			else
			{
				jQuery.Select("#" + ControlId + "-title .ui-icon-close").Live("click", delegate(jQueryEvent e)
				{
					Close();

				});
			}
		}
		public void Close()
		{
			bool closeTab = true;

			if (OnTabClose != null)
			{
				closeTab = OnTabClose();
			}

			if (closeTab)
			{
				int index = jQuery.Select("#" + ControlId + "-title").Index();
				jQueryExtension.Select<jQueryUIObject>("#" + Parent.ControlId).Tabs("remove", index.ToString());
			}
		}

	}
}
