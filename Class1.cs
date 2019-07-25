﻿using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace RandomNames
{
    class SyncTextBox : RichTextBox
    {
        public SyncTextBox()
        {
            this.Multiline = true;
            this.ScrollBars = RichTextBoxScrollBars.Vertical;
        }
        public Control Buddy { get; set; }

        public static bool scrolling;   // In case buddy tries to scroll us
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            // Trap WM_VSCROLL message and pass to buddy
            if (m.Msg == 0x115 && !scrolling && Buddy != null && Buddy.IsHandleCreated)
            {
                scrolling = true;
                SendMessage(Buddy.Handle, m.Msg, m.WParam, m.LParam);
                scrolling = false;
            }
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
    }
}