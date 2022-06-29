using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client2
{
    public partial class Form1 : Form
    {

        bool close = false;
        bool connection = false;
        Socket clientSocket;
        string usern = "";

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }
        
        public void disconnection()
        {
            clientSocket.Close();
            connection = false;
            connect.Enabled = true;
            disconnect.Enabled = false;
            send.Enabled = false;
            removeFriend.Enabled = false;
            addFriend.Enabled = false;
            deletePost.Enabled = false;
            allPosts.Enabled = false;
            friendPosts.Enabled = false;
            ownPosts.Enabled = false;
            friendList.Items.Clear();
        }

        private void connect_Click(object sender, EventArgs e)
        {
            

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string ip_adress = Ip.Text;
            int port_number;

            // Client Side checks.
            if (ip_adress == "") clientLog.AppendText("IP cannot be empty.\n");
            else if (Port.Text == "") clientLog.AppendText("Port cannot be empty. \n");
            else if (username.Text == "") clientLog.AppendText("Username cannot be empty. \n");
            else
            {
                // Taking port number from the text box.
                if (Int32.TryParse(Port.Text, out port_number))
                {
                    try
                    {
                        // Connecting and sending username
                        clientSocket.Connect(ip_adress, port_number);
                        connection = true;

                        Thread receive_from_server = new Thread(Receive);
                        receive_from_server.Start();

                        Byte[] buffer1 = Encoding.Default.GetBytes("clientUsername:" + username.Text);
                        clientSocket.Send(buffer1);


                    }
                    catch
                    {
                        clientLog.AppendText("Couldn't connect to the server.\n");
                    }
                }
                else
                {
                    clientLog.AppendText("Invalid port number.\n");
                }
            }
            

        }
        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            close = true;
            connection = false;
            Environment.Exit(0);
        }

        private void Receive(object obj)
        {
            while (connection)
            {
                try
                {
                    // Receiving responses from server.
                    Byte[] buffer = new byte[350];
                    clientSocket.Receive(buffer);
                    string incoming_message = Encoding.Default.GetString(buffer);
                    incoming_message = incoming_message.Trim('\0');
                    // Checking the response type and act accordingly.
                    if (incoming_message == "Please enter a valid username!(Username does not exist.)\n" || incoming_message == "User already connected to the server.\n")
                    {
                        // When username does not exist or already connected.
                        clientLog.AppendText(incoming_message);
                        clientSocket.Close();
                        connection = false;
                    }
                    else if (incoming_message.Contains("connected"))
                    {
                        // When connection is succeed.
                        clientLog.AppendText(incoming_message);
                        disconnect.Enabled = true;
                        allPosts.Enabled = true;
                        send.Enabled = true;
                        connect.Enabled = false;
                        removeFriend.Enabled = true;
                        addFriend.Enabled = true;
                        deletePost.Enabled = true;
                        allPosts.Enabled = true;
                        friendPosts.Enabled = true;
                        ownPosts.Enabled = true;
                        usern = username.Text;
                    }
                    else if (incoming_message.Contains("|"))
                    {
                        
                        
                        var splittedData = incoming_message.Split('|');
                        for(int x = 0; x+4 < splittedData.Count(); x += 4)
                        {
                            clientLog.AppendText("Username: " + splittedData[x] + "\n" + "PostID: " + splittedData[x+1] + "\n" + "Post: " + splittedData[x+3] + "\n" + "Time: " + splittedData[x+2] + "\n");
                        }

                        

                    }
                    else if (incoming_message.Contains("URFRNDS;"))
                    {
                        List<string> list = incoming_message.Split(';').ToList();
                        list.RemoveAt(0);
                        list.RemoveAt(list.Count()-1);
                        friendList.Items.AddRange(list.ToArray());
                    }
                    else if (incoming_message.Substring(0,22) == "You succesfully added ")
                    {
                        string toBeAdded = incoming_message.Substring(22,incoming_message.IndexOf(" to your")-22);
                        friendList.Items.Add(toBeAdded);
                        clientLog.AppendText(incoming_message);
                    }
                    else if (incoming_message.Contains(" has added you as a friend"))
                    {
                        string toBeAdded = incoming_message.Substring(0, incoming_message.IndexOf(" has added"));
                        friendList.Items.Add(toBeAdded);
                        clientLog.AppendText(incoming_message);
                    }
                    else if (incoming_message.Contains(" has removed you from friend list."))
                    {
                        string toBeRemoved = incoming_message.Substring(0, incoming_message.IndexOf(" has removed you"));
                        friendList.Items.Remove(toBeRemoved);
                        clientLog.AppendText(incoming_message);
                    }
                    else
                    {
                        // Anything else
                        clientLog.AppendText(incoming_message);
                    }
                }
                catch
                {
                    if (!close && connection)
                    {
                        clientLog.AppendText("The server has disconnected. \n");
                    }
                    disconnection();
                }
            }
        }

        private void disconnect_Click(object sender, EventArgs e)
        {
            
            
            clientLog.AppendText("You are disconnected from the server.\n");
            disconnection();
            

        }

        private void send_Click(object sender, EventArgs e)
        {
            clientLog.AppendText("You have succesfully sent a post. \n"+ usern +": " + post.Text + "\n");
            Byte[] buffer1 = Encoding.Default.GetBytes(post.Text);
            clientSocket.Send(buffer1);
        }

        private void allPosts_Click(object sender, EventArgs e)
        {
            clientLog.AppendText("Here is all posts:\n");
            Byte[] buffer = Encoding.Default.GetBytes("request:allposts");
            clientSocket.Send(buffer);
        }

        private void deletePost_Click(object sender, EventArgs e)
        {
            Byte[] buffer = Encoding.Default.GetBytes("request:deletepost:"+dpid.Text);
            clientSocket.Send(buffer);
        }

        private void addFriend_Click(object sender, EventArgs e)
        {
            Byte[] buffer = Encoding.Default.GetBytes("request:addFriend:" + friendUsername.Text);
            clientSocket.Send(buffer);
        }

        private void friendPosts_Click(object sender, EventArgs e)
        {
            clientLog.AppendText("Here is your friends posts:\n");
            Byte[] buffer = Encoding.Default.GetBytes("request:friendsposts");
            clientSocket.Send(buffer);
        }

        private void ownPosts_Click(object sender, EventArgs e)
        {
            clientLog.AppendText("Here is your own posts:\n");
            Byte[] buffer = Encoding.Default.GetBytes("request:ownposts");
            clientSocket.Send(buffer);
            
        }

        private void removeFriend_Click(object sender, EventArgs e)
        {
            Byte[] buffer = Encoding.Default.GetBytes("request:deletefriend:"+friendList.SelectedItem.ToString());
            clientSocket.Send(buffer);
            friendList.Items.RemoveAt(friendList.SelectedIndex);
        }
    }
}
