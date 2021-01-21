using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Pipes;

namespace AyugraAPICSharp
{
    public partial class Form1 : Form
    {
        NamedPipeClientStream client;

        private String[] getAyugraId()
        {
            String[] listOfPipes = System.IO.Directory.GetFiles(@"\\.\pipe\");
            List<String> listOfIds_List = new List<String>();
            for (int i = 0; i < listOfPipes.Length; i++)
            {
                
                if (listOfPipes[i].Contains("AyugraPacket"))
                {
                    Console.WriteLine(listOfPipes[i]);
                    String[] split = listOfPipes[i].Split('_');
                    Console.WriteLine(split[1]);
                    listOfIds_List.Add(split[1]);
                }
            }

            String[] listOfIds = listOfIds_List.ToArray();
            return listOfIds;
        }

        private NamedPipeClientStream connection(string id)
        {
            Console.WriteLine("Connexion..");
            NamedPipeClientStream client = new NamedPipeClientStream("AyugraPacketApi_" + id);
            client.Connect();
            return client;
        }

        private void refreshList()
        {
            String[] ayugraIds = getAyugraId();

            for (int i = 0; i < ayugraIds.Length; i++)
            {
                ayugra_list.Items.Add(ayugraIds[i]);
            }

        }

        private void disconnect()
        {
            client.Close();
        }

        public Form1()
        {
            InitializeComponent();

            refreshList();

            //Get data from NamedPipe
            /*
            using (StreamReader sr = new StreamReader(client))
            {
                // Display the read text to the console
                string temp;
                while ((temp = sr.ReadLine()) != null)
                {
                    Console.WriteLine("Received from server: {0}", temp);
                }
            }*/

        }

        private void connect_button_Click(object sender, EventArgs e)
        {
            string id = ayugra_list.SelectedItem.ToString();
            if (id != null)
            {
                client = connection(id);
            }
        }

        private void disconnect_button_Click(object sender, EventArgs e)
        {
            disconnect();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refreshList();
        }
    }
}
