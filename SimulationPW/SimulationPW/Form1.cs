using System;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace SimulationPW
{
    public partial class Form1 : Form
    {
        public bool run = false;

        private const String paused = "Paused";
        private const String running = "Running";

        private const int minStep = 2;
        private const int stepToCalculate = 23; 

        public bool isProgressBarTaken1 = false;
        public bool isProgressBarTaken2 = false;
        public bool isProgressBarTaken3 = false;
        public bool isProgressBarTaken4 = false;
        public bool isProgressBarTaken5 = false;

        private Plik currentPlik1 = null;
        private Plik currentPlik2 = null;
        private Plik currentPlik3 = null;
        private Plik currentPlik4 = null;
        private Plik currentPlik5 = null;

        private int dysk1Step = 10;
        private int dysk2Step = 10;
        private int dysk3Step = 10;
        private int dysk4Step = 10;
        private int dysk5Step = 10;

        private const int minimumFiles = 1;
        private const int maxFiles = 10;
        private const string V = "Puste";
        private int fileId = 0;
        private int clientId = 0;

        private volatile List<Klient> allActieClients = new List<Klient>();
        private volatile List<Plik> allPlikList = new List<Plik>();

        public delegate void UpdateProgressBar1();
        public delegate void UpdateCurrentFileLabel();

        public void UiUpdateLabel1()
        {
            if (progressBar1.Value + dysk1Step <= 100)
                progressBar1.Value += dysk1Step;
            else
            {
                progressBar1.Value = 0;
                isProgressBarTaken1 = false;
                progressBar1.Invoke(new UpdateProgressBar1(UiUpdateCurrentFileLabelEMPTY1));
            }
        }


        public void UiUpdateLabel2()
        {
            if (progressBar2.Value + dysk2Step <= 100)
                progressBar2.Value += dysk2Step;
            else
            {
                progressBar2.Value = 0;
                isProgressBarTaken2 = false;
                progressBar2.Invoke(new UpdateProgressBar1(UiUpdateCurrentFileLabelEMPTY2));
            }
        }


        public void UiUpdateLabel3()
        {
            if (progressBar3.Value + dysk3Step <= 100)
                progressBar3.Value += dysk3Step;
            else
            {
                progressBar3.Value = 0;
                isProgressBarTaken3 = false;
                progressBar3.Invoke(new UpdateProgressBar1(UiUpdateCurrentFileLabelEMPTY3));
            }
        }


        public void UiUpdateLabel4()
        {
            if (progressBar4.Value + dysk4Step <= 100)
                progressBar4.Value += dysk4Step;
            else
            {
                progressBar4.Value = 0;
                isProgressBarTaken4 = false;
                progressBar4.Invoke(new UpdateProgressBar1(UiUpdateCurrentFileLabelEMPTY4));
            }
        }


        public void UiUpdateLabel5()
        {
            if (progressBar5.Value + dysk5Step <= 100)
                progressBar5.Value += dysk5Step;
            else
            {
                progressBar5.Value = 0;
                isProgressBarTaken5 = false;
                progressBar5.Invoke(new UpdateProgressBar1(UiUpdateCurrentFileLabelEMPTY5));
            }
        }

        public void UiUpdateCurrentFileLabelText1()
        {
            dysk1Current.Text = currentPlik1.weight + " Mb";
        }

        public void UiUpdateCurrentFileLabelText2()
        {
            dysk2Current.Text = currentPlik2.weight + " Mb";
        }

        public void UiUpdateCurrentFileLabelText3()
        {
            dysk3Current.Text = currentPlik3.weight + " Mb";
        }

        public void UiUpdateCurrentFileLabelText4()
        {
            dysk4Current.Text = currentPlik4.weight + " Mb";
        }

        public void UiUpdateCurrentFileLabelText5()
        {
            dysk5Current.Text = currentPlik5.weight + " Mb";
        }


        public void UiUpdateCurrentFileLabelEMPTY1()
        {
            dysk1Current.Text = V;
        }

        public void UiUpdateCurrentFileLabelEMPTY2()
        {
            dysk2Current.Text = V;
        }

        public void UiUpdateCurrentFileLabelEMPTY3()
        {
            dysk3Current.Text = V;
        }

        public void UiUpdateCurrentFileLabelEMPTY4()
        {
            dysk4Current.Text = V;
        }

        public void UiUpdateCurrentFileLabelEMPTY5()
        {
            dysk5Current.Text = V;
        }

        public Form1()
        {
            InitializeComponent();
            intiClients();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread thr = new Thread(new ThreadStart(IterateProgressBar));
            thr.Start();
        }

        private void intiClients()
        {
            int numberOfClients = generateRandomInt(1, 10);
            for (int i = 0; i < numberOfClients; i++)
            {
                allActieClients.Add(generateRandomClient(true));
                Thread.Sleep(generateRandomInt(1, 10));
            }
            fillKlientList();
        }

        private void button1_Click(object sender, EventArgs e)
        {

             run = !run;
            if (run)
                label6.Text = running;
            else
                label6.Text = paused;
        }

        public void EnableStartButton()
        {
            button1.Enabled = !button1.Enabled;
        }

        public void IterateProgressBar()
        {
            Thread.Sleep(5000);
            progressBar1.Invoke(new UpdateProgressBar1(EnableStartButton));
            while (true) {
                while (run)
                {
                    if(isProgressBarTaken1)
                        progressBar1.Invoke(new UpdateProgressBar1(UiUpdateLabel1));
                    else
                    {
                        calculateFileQueryFactorForClients();
                        currentPlik1 = findBestFile();
                        
                        if (currentPlik1 != null)
                        {
                            dysk1Step = calculateStepByWeight(currentPlik1.weight);
                            cleanupAfterSelectingBestFile(currentPlik1);
                            isProgressBarTaken1 = true;
                            progressBar1.Invoke(new UpdateCurrentFileLabel(UiUpdateCurrentFileLabelText1));
                        }
                    }

                    if (isProgressBarTaken2)
                        progressBar1.Invoke(new UpdateProgressBar1(UiUpdateLabel2));
                    else
                    {
                        calculateFileQueryFactorForClients();
                        currentPlik2 = findBestFile();
                       
                        if (currentPlik2 != null)
                        {
                            dysk2Step = calculateStepByWeight(currentPlik2.weight);
                            cleanupAfterSelectingBestFile(currentPlik2);
                            isProgressBarTaken2 = true;
                            progressBar1.Invoke(new UpdateCurrentFileLabel(UiUpdateCurrentFileLabelText2));
                        }
                    }

                    if (isProgressBarTaken3)
                        progressBar1.Invoke(new UpdateProgressBar1(UiUpdateLabel3));
                    else
                    {
                        calculateFileQueryFactorForClients();
                        currentPlik3 = findBestFile();
                        
                        if (currentPlik3 != null)
                        {
                            dysk3Step = calculateStepByWeight(currentPlik3.weight);
                            cleanupAfterSelectingBestFile(currentPlik3);
                            isProgressBarTaken3 = true;
                            progressBar1.Invoke(new UpdateCurrentFileLabel(UiUpdateCurrentFileLabelText3));
                        }
                    }

                    if (isProgressBarTaken4)
                        progressBar1.Invoke(new UpdateProgressBar1(UiUpdateLabel4));
                    else
                    {
                        calculateFileQueryFactorForClients();
                        currentPlik4 = findBestFile();
                        
                        if (currentPlik4 != null)
                        {
                            dysk4Step = calculateStepByWeight(currentPlik4.weight);
                            cleanupAfterSelectingBestFile(currentPlik4);
                            isProgressBarTaken4 = true;
                            progressBar1.Invoke(new UpdateCurrentFileLabel(UiUpdateCurrentFileLabelText4));
                        }
                    }

                    if (isProgressBarTaken5)
                        progressBar1.Invoke(new UpdateProgressBar1(UiUpdateLabel5));
                    else
                    {
                        calculateFileQueryFactorForClients();
                        currentPlik5 = findBestFile();
                        
                        if (currentPlik5 != null)
                        {
                            dysk5Step = calculateStepByWeight(currentPlik5.weight);
                            cleanupAfterSelectingBestFile(currentPlik5);
                            isProgressBarTaken5 = true;
                            progressBar1.Invoke(new UpdateCurrentFileLabel(UiUpdateCurrentFileLabelText5));
                        }
                    }


                    Thread.Sleep(1000);
                }
            }
        }

        private void cleanupAfterSelectingBestFile(Plik bestFile)
        {
            List<Klient> clientsToDelete = new List<Klient>();
            foreach (Klient client in allActieClients)
            {
                if (client.plikiList.Contains(bestFile))
                    client.plikiList.Remove(bestFile);
                allPlikList.Remove(bestFile);
                if (client.plikiList.Count == 0)
                    clientsToDelete.Add(client);
            }
            clientsToDelete.ForEach(x=>allActieClients.Remove(x));
            listView1.Invoke(new UpdateProgressBar1(fillKlientList));
        }

        private Plik findBestFile()
        {
            Plik bestFile = null;
            foreach(Klient client in allActieClients)
            {
                foreach(Plik file in client.plikiList)
                {
                    if (bestFile == null || bestFile.queryFactor < file.queryFactor)
                        bestFile = file;
                }
            }
            return bestFile;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            addClient();
            calculateFileQueryFactorForClients();
        }

        private void addClient()
        {
            allActieClients.Add(generateRandomClient(true));
            fillKlientList();
        }

        private void fillKlientList()
        {
            listView1.Items.Clear();
            foreach(Klient klient in allActieClients)
            {
                String name = klient.name;
                String pliki = "{ ";
                List<Plik> list = klient.plikiList;
                for (int i = 0; i < list.Count; i++)
                {
                    Plik plik = list[i];
                    pliki += "("
                             + plik.weight
                             + " MB)";
                    if (i != list.Count - 1)
                        pliki += ", ";
                    
                }
                pliki += " }";
                string[] row = { name, pliki };
                ListViewItem listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
            }
        }

        private Klient generateRandomClient(Boolean offsetDate)
        {
            Klient returnKlient = new Klient();
            returnKlient.name = "Klient" + clientId;
            clientId++;
            int numberOfFiles = generateRandomInt(minimumFiles, maxFiles);
            Plik nowyPlik;
            for(int i = 0; i < numberOfFiles; i++)
            {
                DateTime time = DateTime.Now;
                if (offsetDate)
                    time.AddSeconds(generateRandomDouble(10) + 1.00);
                if(generateRandomInt(1, 100) % 2 == 0)
                    nowyPlik = new Plik(generateRandomInt(1, 1000000), "Plik" + fileId, time);
                else
                    nowyPlik = new Plik(generateRandomInt(1, 10000), "Plik" + fileId, time);
                returnKlient.addPlik(nowyPlik);
                allPlikList.Add(nowyPlik);
                fileId++;
                Thread.Sleep(20);
            }
            return returnKlient;
        }
        
        private int generateRandomInt(int start, int stop)
        {
            Random r = new Random();
            int rInt = r.Next(start, stop);
            return rInt;
        }
        private double generateRandomDouble(int range)
        {
            Random r = new Random();
            double rDouble = r.NextDouble() * range;
            return rDouble;
        }

        private void calculateFileQueryFactorForClients()
        {
            foreach(Klient client in allActieClients)
            {
                int numberOfFiles = client.plikiList.Count;
                foreach(Plik file in client.plikiList)
                {
                    file.queryFactor = calculateQueryFactor(file.timeUploadCreate, numberOfFiles, file.weight);
                }
            }
        }

        private Double calculateQueryFactor(DateTime fileCreateDate, int numberOfClientFiles, Double fileSize)
        {
            TimeSpan ts = DateTime.Now - fileCreateDate;
            Double fileSizeCalculatedValue = 0.0;
            Double minutesElapsed = ts.Minutes;
            Double timeElapsedCalculatedValue = Math.Log(minutesElapsed + 1)/numberOfClientFiles;
            Double calculationFileSize = fileSize / 100.0;
            
            if(calculationFileSize != 1.0000)
                fileSizeCalculatedValue = (Math.Log(calculationFileSize) /(calculationFileSize - 1)) * numberOfClientFiles;
            else
                fileSizeCalculatedValue = (Math.Log(calculationFileSize) /(calculationFileSize - 0.99999)) * numberOfClientFiles;
            return timeElapsedCalculatedValue + fileSizeCalculatedValue;
        }

        private int calculateStepByWeight(Double fileSize)
        {
            return (int)((((Math.Abs( fileSize - 1000000)) / 1000000) * stepToCalculate) + minStep);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
