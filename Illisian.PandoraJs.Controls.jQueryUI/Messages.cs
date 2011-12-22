// Confirm.cs
//

using System;
using System.Collections.Generic;
using jQueryApi;
using Illisian.PandoraJs.Controls;
using System.Collections;
using Illisian.PandoraJs.Utils.Extension;

namespace Illisian.PandoraJs.Controls.jQueryUI
{
	public class Messages
	{
		public static void ShowConfirm(string message, string title, string height, string width, PandoraEventHandler okEvent, PandoraEventHandler cancelEvent)
		{
			Dialog _dialog = new Dialog();
			Literal _litMessage = new Literal();
			_litMessage.Id = "litMessage";
			_litMessage.Html = message;
			_dialog.Id = "dlgConfirmBox";
			_dialog.CloseOnEscape = false;
			_dialog.DisableCloseButton = true;
			_dialog.AddChild(_litMessage);
			_dialog.Modal = true;
			_dialog.Height = height;
			_dialog.Width = width;
			_dialog.Title = title;
			DialogButton okButton = new DialogButton();
			okButton.Click = delegate(object e, object[] args)
			{
				if (okEvent != null)
					okEvent(e);
				_dialog.Unload();
			};
			okButton.Text = "Ok";
			DialogButton cancelButton = new DialogButton();
			cancelButton.Click = delegate(object e, object[] args)
			{
				if (cancelEvent != null)
					cancelEvent(e, args);
				_dialog.Unload();
			};
			cancelButton.Text = "Cancel";
			_dialog.Buttons = new DialogButton[] { okButton, cancelButton };
			Page.Context.AddChild(_dialog);
		}

		public static void ShowAlert(string message,string title,string height, string width, PandoraEventHandler okEvent)
		{
			Dialog _dialog = new Dialog();
			Literal _litMessage = new Literal();
			_litMessage.Id = "litMessage";
			_litMessage.Html = message;
			_dialog.Id = "dlgAlertBox";
			_dialog.CloseOnEscape = false;
			_dialog.DisableCloseButton = true;
			_dialog.Width = width;
			_dialog.Height = height;
			_dialog.Title = title;
			_dialog.Modal = true;
			_dialog.AddChild(_litMessage);
			DialogButton okButton = new DialogButton();
			okButton.Click = delegate(object e, object[] args)
			{
				if (okEvent != null)
					okEvent(e, args);
				_dialog.Unload();
			};
			okButton.Text = "Ok";
			_dialog.Buttons = new DialogButton[] { okButton };
			Page.Context.AddChild(_dialog);
		}
	}
}
