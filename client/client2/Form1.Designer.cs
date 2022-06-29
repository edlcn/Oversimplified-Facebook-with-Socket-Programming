namespace client2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Ip = new System.Windows.Forms.TextBox();
            this.Port = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.post = new System.Windows.Forms.TextBox();
            this.clientLog = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.connect = new System.Windows.Forms.Button();
            this.disconnect = new System.Windows.Forms.Button();
            this.send = new System.Windows.Forms.Button();
            this.allPosts = new System.Windows.Forms.Button();
            this.friendPosts = new System.Windows.Forms.Button();
            this.friendUsername = new System.Windows.Forms.TextBox();
            this.dpid = new System.Windows.Forms.TextBox();
            this.addFriend = new System.Windows.Forms.Button();
            this.deletePost = new System.Windows.Forms.Button();
            this.friendList = new System.Windows.Forms.ListBox();
            this.removeFriend = new System.Windows.Forms.Button();
            this.ownPosts = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Ip
            // 
            this.Ip.Location = new System.Drawing.Point(70, 48);
            this.Ip.Name = "Ip";
            this.Ip.Size = new System.Drawing.Size(143, 20);
            this.Ip.TabIndex = 0;
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(70, 90);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(142, 20);
            this.Port.TabIndex = 1;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(72, 134);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(141, 20);
            this.username.TabIndex = 2;
            // 
            // post
            // 
            this.post.Location = new System.Drawing.Point(38, 367);
            this.post.Multiline = true;
            this.post.Name = "post";
            this.post.Size = new System.Drawing.Size(227, 41);
            this.post.TabIndex = 3;
            // 
            // clientLog
            // 
            this.clientLog.Location = new System.Drawing.Point(329, 28);
            this.clientLog.Name = "clientLog";
            this.clientLog.Size = new System.Drawing.Size(575, 380);
            this.clientLog.TabIndex = 4;
            this.clientLog.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ip:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Username:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 342);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Post";
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(232, 51);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(91, 26);
            this.connect.TabIndex = 9;
            this.connect.Text = "connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // disconnect
            // 
            this.disconnect.Enabled = false;
            this.disconnect.Location = new System.Drawing.Point(232, 117);
            this.disconnect.Name = "disconnect";
            this.disconnect.Size = new System.Drawing.Size(91, 23);
            this.disconnect.TabIndex = 10;
            this.disconnect.Text = "disconnect";
            this.disconnect.UseVisualStyleBackColor = true;
            this.disconnect.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // send
            // 
            this.send.Enabled = false;
            this.send.Location = new System.Drawing.Point(96, 423);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(87, 24);
            this.send.TabIndex = 11;
            this.send.Text = "send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // allPosts
            // 
            this.allPosts.Enabled = false;
            this.allPosts.Location = new System.Drawing.Point(452, 414);
            this.allPosts.Name = "allPosts";
            this.allPosts.Size = new System.Drawing.Size(91, 33);
            this.allPosts.TabIndex = 12;
            this.allPosts.Text = "All Posts";
            this.allPosts.UseVisualStyleBackColor = true;
            this.allPosts.Click += new System.EventHandler(this.allPosts_Click);
            // 
            // friendPosts
            // 
            this.friendPosts.Location = new System.Drawing.Point(602, 414);
            this.friendPosts.Name = "friendPosts";
            this.friendPosts.Size = new System.Drawing.Size(85, 33);
            this.friendPosts.TabIndex = 13;
            this.friendPosts.Text = "Friends\' Posts";
            this.friendPosts.UseVisualStyleBackColor = true;
            this.friendPosts.Click += new System.EventHandler(this.friendPosts_Click);
            // 
            // friendUsername
            // 
            this.friendUsername.Location = new System.Drawing.Point(18, 500);
            this.friendUsername.Name = "friendUsername";
            this.friendUsername.Size = new System.Drawing.Size(143, 20);
            this.friendUsername.TabIndex = 14;
            // 
            // dpid
            // 
            this.dpid.Location = new System.Drawing.Point(17, 526);
            this.dpid.Name = "dpid";
            this.dpid.Size = new System.Drawing.Size(143, 20);
            this.dpid.TabIndex = 15;
            // 
            // addFriend
            // 
            this.addFriend.Location = new System.Drawing.Point(192, 500);
            this.addFriend.Name = "addFriend";
            this.addFriend.Size = new System.Drawing.Size(120, 20);
            this.addFriend.TabIndex = 16;
            this.addFriend.Text = "Add Friend";
            this.addFriend.UseVisualStyleBackColor = true;
            this.addFriend.Click += new System.EventHandler(this.addFriend_Click);
            // 
            // deletePost
            // 
            this.deletePost.Location = new System.Drawing.Point(193, 526);
            this.deletePost.Name = "deletePost";
            this.deletePost.Size = new System.Drawing.Size(119, 20);
            this.deletePost.TabIndex = 17;
            this.deletePost.Text = "Delete Post";
            this.deletePost.UseVisualStyleBackColor = true;
            this.deletePost.Click += new System.EventHandler(this.deletePost_Click);
            // 
            // friendList
            // 
            this.friendList.FormattingEnabled = true;
            this.friendList.Location = new System.Drawing.Point(48, 177);
            this.friendList.Name = "friendList";
            this.friendList.Size = new System.Drawing.Size(206, 108);
            this.friendList.TabIndex = 18;
            // 
            // removeFriend
            // 
            this.removeFriend.Location = new System.Drawing.Point(96, 291);
            this.removeFriend.Name = "removeFriend";
            this.removeFriend.Size = new System.Drawing.Size(108, 25);
            this.removeFriend.TabIndex = 19;
            this.removeFriend.Text = "Remove Friend";
            this.removeFriend.UseVisualStyleBackColor = true;
            this.removeFriend.Click += new System.EventHandler(this.removeFriend_Click);
            // 
            // ownPosts
            // 
            this.ownPosts.Location = new System.Drawing.Point(734, 414);
            this.ownPosts.Name = "ownPosts";
            this.ownPosts.Size = new System.Drawing.Size(85, 32);
            this.ownPosts.TabIndex = 20;
            this.ownPosts.Text = "Own Posts";
            this.ownPosts.UseVisualStyleBackColor = true;
            this.ownPosts.Click += new System.EventHandler(this.ownPosts_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 576);
            this.Controls.Add(this.ownPosts);
            this.Controls.Add(this.removeFriend);
            this.Controls.Add(this.friendList);
            this.Controls.Add(this.deletePost);
            this.Controls.Add(this.addFriend);
            this.Controls.Add(this.dpid);
            this.Controls.Add(this.friendUsername);
            this.Controls.Add(this.friendPosts);
            this.Controls.Add(this.allPosts);
            this.Controls.Add(this.send);
            this.Controls.Add(this.disconnect);
            this.Controls.Add(this.connect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clientLog);
            this.Controls.Add(this.post);
            this.Controls.Add(this.username);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.Ip);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Ip;
        private System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox post;
        private System.Windows.Forms.RichTextBox clientLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.Button disconnect;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.Button allPosts;
        private System.Windows.Forms.Button friendPosts;
        private System.Windows.Forms.TextBox friendUsername;
        private System.Windows.Forms.TextBox dpid;
        private System.Windows.Forms.Button addFriend;
        private System.Windows.Forms.Button deletePost;
        private System.Windows.Forms.ListBox friendList;
        private System.Windows.Forms.Button removeFriend;
        private System.Windows.Forms.Button ownPosts;
    }
}

