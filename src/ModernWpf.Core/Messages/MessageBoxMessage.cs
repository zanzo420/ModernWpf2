using ModernWpf.Controls;
using System;
using System.Windows;

namespace ModernWpf.Messages
{
    /// <summary>
    /// Message for showing typical modal dialog.
    /// </summary>
    public class MessageBoxMessage : MessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBoxMessage" /> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="callback">The callback.</param>
        public MessageBoxMessage(string content, Action<MessageBoxResult> callback = null) : this(null, null, content, callback) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBoxMessage" /> class.
        /// </summary>
        /// <param name="sender">The message's original sender.</param>
        /// <param name="content">The content.</param>
        /// <param name="callback">The callback.</param>
        public MessageBoxMessage(object sender, string content, Action<MessageBoxResult> callback = null) : this(sender, null, content, callback) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBoxMessage" /> class.
        /// </summary>
        /// <param name="sender">The message's original sender.</param>
        /// <param name="target">The message's intended target.</param>
        /// <param name="content">The content.</param>
        /// <param name="callback">The callback.</param>
        public MessageBoxMessage(object sender, object target, string content, Action<MessageBoxResult> callback = null)
            : base(sender, target)
        {
            Content = content;
            _callback = callback;
        }

        /// <summary>
        /// Gets or sets the available buttons.
        /// </summary>
        /// <value>
        /// The button.
        /// </value>
        public MessageBoxButton Button { get; set; }

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        /// <value>
        /// The caption.
        /// </value>
        public string Caption { get; set; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; private set; }

        /// <summary>
        /// Gets or sets the default result.
        /// </summary>
        /// <value>
        /// The default result.
        /// </value>
        public MessageBoxResult DefaultResult { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public MessageBoxImage Icon { get; set; }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public MessageBoxOptions Options { get; set; }

        Action<MessageBoxResult> _callback;

        /// <summary>
        /// Does the callback to notify dialog result.
        /// </summary>
        /// <param name="result">The result.</param>
        public void DoCallback(MessageBoxResult result)
        {
            if (_callback != null)
            {
                _callback(result);
            }
        }
        

        /// <summary>
        /// Handles a basic <see cref="MessageBoxMessage" /> on a window by showing built-in <see cref="MessageBox"/>
        /// and invokes the callback.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public void HandleWithPlatform(Window owner)
        {
            MessageBoxResult res = MessageBoxResult.None;

            var d = owner.FindDispatcher();
            if (d != null && !d.CheckAccess())
            {
                d.BeginInvoke(new Action<Window>(win =>
                {
                    HandleWithPlatform(win);
                }), owner);
                return;
            }

            if (owner == null)
            {
                res = MessageBox.Show(Content, Caption, Button, Icon, DefaultResult, Options);
            }
            else
            {
                res = MessageBox.Show(owner, Content, Caption, Button, Icon, DefaultResult, Options);
            }
            DoCallback(res);
        }

    }
}
