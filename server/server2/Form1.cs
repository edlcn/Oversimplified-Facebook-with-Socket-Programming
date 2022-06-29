using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace server2
{
    public partial class Form1 : Form
    {
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        List<string> activeUsers = new List<string>();
        int lastPID = 0;

        bool terminating = false;
        bool listening = false;


        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        public void add_friend(string un1,string un2, Socket thisClient)
        {
            if (un1 == un2)
            {
                Log.AppendText(un1 + " tried to send a friend request to itself.\n");
                Byte[] buffer2 = Encoding.Default.GetBytes("You cannot send a friend request to yourself.\n");
                thisClient.Send(buffer2);
                return;
            }
            string rootDirectory = "../../friendship/", un1Path = rootDirectory + un1 + ".txt", un2Path = rootDirectory + un2 + ".txt";
            string finalMessage = "You succesfully added "+ un2 +" to your friend list.\n", logMessage = un1 + " and " + un2 + " are now friends.\n";

            var lines = File.ReadLines("../../user-db.txt");
            bool isExists = false;
            foreach (string line in lines)
            {
                if (line == un2)
                {
                    isExists = true;
                    break;
                }
            }
            if (isExists)
            {
                if (!File.Exists(un1Path))
                {
                    File.AppendAllText(un2Path, un1 + "\n");
                    File.AppendAllText(un1Path, un2 + "\n");
                    add_friend_notification(un1, un2);
                }
                else
                {
                    var friends = File.ReadLines(un1Path);
                    bool isFriend = false;
                    foreach (string friend in friends)
                    {
                        if (friend == un2)
                        {
                            isFriend = true;
                            logMessage = un1 + " and " + un2 + " are already friends.\n";
                            finalMessage = un2 + " is already in your friend list.\n";
                            break;
                        }
                    }
                    if (!isFriend)
                    {
                        File.AppendAllText(un2Path, un1 + "\n");
                        File.AppendAllText(un1Path, un2 + "\n");
                        add_friend_notification(un1, un2);
                    }
                }
            } 
            else
            {
                finalMessage = un2 + " is not found in database. \n";
                logMessage = un2 + " is not found in database. \n";
            }
            Log.AppendText(logMessage);
            Byte[] buffer1 = Encoding.Default.GetBytes(finalMessage);
            thisClient.Send(buffer1);
        }

        public void get_friend(string username,Socket thisClient)
        {
            string path = "../../friendship/" + username + ".txt";
            if (File.Exists(path))
            {
                var lines = File.ReadLines(path);
                string allFriends = "URFRNDS;";
                foreach (string friend in lines)
                {
                    allFriends += friend + ";";
                }
                Byte[] buffer1 = Encoding.Default.GetBytes(allFriends);
                thisClient.Send(buffer1);

            }

        }

        public void delete_friend(string un1,string un2)
        {
            string path = "../../friendship/" + un1 + ".txt";
            List<string> toBeReplaced = File.ReadAllLines(path).ToList();
            toBeReplaced.Remove(un2);
            File.WriteAllLines(path, toBeReplaced.ToArray());

        }

        public void add_friend_notification(string un1,string un2)
        {
            int index = activeUsers.IndexOf(un2);
            if (index != -1)
            {
                Byte[] buffer1 = Encoding.Default.GetBytes(un1 + " has added you as a friend.\n");
                clientSockets[index].Send(buffer1);
            }
        }

        public void delete_friend_notification(string un1,string un2)
        {
            int index = activeUsers.IndexOf(un2);
            if (index != -1)
            {
                Byte[] buffer1 = Encoding.Default.GetBytes(un1 + " has removed you from friend list.\n");
                clientSockets[index].Send(buffer1);
            }
        }

        private void listen_Click(object sender, EventArgs e)
        {
            int port;


            // Taking port number. 
            if (Int32.TryParse(Port.Text, out port))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);
                listening = true;
                listen.Enabled = false;

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                Log.AppendText("Started listening on port: " + port + "\n");
            }
            else
            {
                Log.AppendText("Please check port number \n");
            }
            pidFinder();  // finds and assigns last ID.
        }

        // It finds the last assigned ID.
        private void pidFinder()
        {
            var lines = File.ReadLines("../../post-db.txt");
            string lastLine = "";
            foreach (string line in lines)
            {
                lastLine = line;
            }
            if (lastLine != "")
            {
                lastPID = Convert.ToInt16(lastLine.Split('|')[1]);
            }
            
            
        }

        private void Accept(object obj)
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);
                    //Log.AppendText("A client is connected.\n");

                    Thread receiveThread = new Thread(() => Receive(newClient)); // client bağlandı, artık o cliente özel bir
                    receiveThread.Start();                                       // thread açılıyor ve client bağlı olduğu sürece o clientten gelen giden var mı bakacak
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        Log.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            terminating = true;
            listening = false;
            Environment.Exit(0);
        }

        private void Receive(Socket thisClient)
        {
            bool connected = true;
            string usern = "";   // Holds username
            while (connected && !terminating)
            {
                try
                {
                    // Receiving requests from client.
                    Byte[] buffer = new Byte[10000];
                    thisClient.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    
                    // Username validation.
                    if (incomingMessage.Contains("clientUsername:"))
                    {
                        usern = incomingMessage.Split(':')[1];
                        var lines = File.ReadLines("../../user-db.txt");
                        bool isExists = false;

                        foreach (string line in lines)
                        {
                            if (usern == line && !activeUsers.Contains(usern))
                            {
                                isExists = true;
                                Log.AppendText(usern + " has connected.\n");
                                activeUsers.Add(usern);
                                Byte[] buffer1 = Encoding.Default.GetBytes("Hello "+ usern + "! You are connected to the server.\n");
                                thisClient.Send(buffer1);
                                get_friend(usern, thisClient);
                                break;
                            }
                        }
                        if (isExists == false)
                        {
                            if (activeUsers.Contains(usern))
                            {
                                Log.AppendText(usern + " is already connected to the server.\n");
                                Byte[] buffer1 = Encoding.Default.GetBytes("User already connected to the server.\n");
                                thisClient.Send(buffer1);
                            }
                            else
                            {
                                Log.AppendText(usern + " tried to connect to the server, but failed.(Username does not exist.)\n");
                                Byte[] buffer1 = Encoding.Default.GetBytes("Please enter a valid username!(Username does not exist.)\n");
                                thisClient.Send(buffer1);

                            }
                           
                            
                            thisClient.Close();
                            clientSockets.Remove(thisClient);
                            connected = false;
                        }
                    }
                    // When a client asks for all posts.
                    else if (incomingMessage.Contains("request:allposts"))
                    {

                        

                        // Finding all posts which is not belong to this username.
                        Log.AppendText("Showed all posts for " + usern + "\n");
                        var lines = File.ReadLines("../../post-db.txt");
                        foreach (string line in lines)
                        {
                            if (line.Split('|')[0] != usern)
                            {
                                
                                Byte[] buffer1 = Encoding.Default.GetBytes(line);
                                thisClient.Send(buffer1);
                            }
                        }

                    }
                    else if (incomingMessage.Contains("request:deletepost"))
                    {

                        List<string> lineList = File.ReadAllLines("../../post-db.txt").ToList();
                        bool doesExist = false;
                        int lineNum = 1;
                        string resultMessage = "Your post has been deleted.\n";
                        foreach (string line in lineList)
                        {
                            
                            if (line.Split('|')[1] == incomingMessage.Split(':')[2])
                            {
                                doesExist = true;
                                if (line.Split('|')[0] == usern)
                                {
                                    Log.AppendText(usern + " deleted the post with post id: " + incomingMessage.Split(':')[2]+ "\n");
                                    
                                    lineList.RemoveAt(lineNum-1);
                                    File.WriteAllLines("../../post-db.txt", lineList);
                                    break;





                                }
                                else
                                {
                                    Log.AppendText(usern + " cannot delete the post with post id: " + incomingMessage.Split(':')[2] + ", since the post does not belong to it.\n");
                                    resultMessage = "This post does not belong to you.\n";
                                    
                                }
                            }
                            lineNum++;
                        }
                        if (!doesExist)
                        {
                            Log.AppendText(usern + " cannot delete the post with post id: " + incomingMessage.Split(':')[2] + ", since the post does not exist or already deleted.\n");
                            resultMessage = "This post is already deleted or does not exist.\n";
                        }
                        Byte[] buffer1 = Encoding.Default.GetBytes(resultMessage);
                        thisClient.Send(buffer1);
                    }
                    else if (incomingMessage.Contains("request:addFriend:"))
                    {
                        string toBeAdded = incomingMessage.Split(':')[2];
                        add_friend(usern, toBeAdded,thisClient);
                        

                    }
                    else if (incomingMessage.Contains("request:ownposts"))
                    {
                        Log.AppendText("Showed own posts for " + usern + "\n");
                        var lines = File.ReadLines("../../post-db.txt");
                        foreach (string line in lines)
                        {
                            if (line.Split('|')[0] == usern)
                            {

                                Byte[] buffer1 = Encoding.Default.GetBytes(line);
                                thisClient.Send(buffer1);
                            }
                        }
                    }
                    else if (incomingMessage.Contains("request:friendsposts"))
                    {
                        List<string> friendList;
                        string path = "../../friendship/" + usern + ".txt";
                        if (File.Exists(path))
                        {
                            Log.AppendText("Showed friend posts for " + usern + "\n");
                            friendList = File.ReadAllLines(path).ToList();
                            var lines = File.ReadLines("../../post-db.txt");
                            foreach (string line in lines)
                            {
                                if (friendList.Contains(line.Split('|')[0]))
                                {

                                    Byte[] buffer1 = Encoding.Default.GetBytes(line);
                                    thisClient.Send(buffer1);
                                }
                            }
                        }
                    }
                    else if (incomingMessage.Contains("request:deletefriend:"))
                    {
                        
                        string toBeDeleted = incomingMessage.Split(':')[2];
                        delete_friend(usern, toBeDeleted);
                        delete_friend(toBeDeleted, usern);
                        Log.AppendText(usern + " has removed " + toBeDeleted + " from its friend list.\n");
                        delete_friend_notification(usern, toBeDeleted);
                        Byte[] buffer1 = Encoding.Default.GetBytes("You succesfully removed " +toBeDeleted + " from your friend list.\n");
                        thisClient.Send(buffer1);
                    }
                    // When a client sends a post.
                    else
                    {
                        string timestamp = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                        int postId = ++lastPID;
                        string wholeEntry = usern + "|" + postId + "|" + timestamp + "|" + incomingMessage + "|";
                        using (StreamWriter file = new StreamWriter("../../post-db.txt", append: true))
                        {
                            file.WriteLine(wholeEntry);
                        }

                        Log.AppendText(usern + " has sent a post:\n" + incomingMessage + "\n");

                    }

                    


                }
                catch
                {
                    if (!terminating)
                    {
                        Log.AppendText(usern +" has disconnected.\n");
                        
                    }

                    thisClient.Close();
                    activeUsers.Remove(usern);
                    clientSockets.Remove(thisClient);
                    connected = false;

                }
            }
        }
    }
}
