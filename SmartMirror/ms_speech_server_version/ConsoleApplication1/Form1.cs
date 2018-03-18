using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//using DotNetBrowser;
//using DotNetBrowser.WinForms;

namespace ConsoleApplication1
{
    public partial class Form1 : Form
    {
        //public WebBrowser search = new WebBrowser();
        //public BrowserView web;
        public static Thread mytimeThread = null;
        public static int movie = 1;
        public Form1()
        {
            InitializeComponent();
            this.label_time.Location = new Point(530, 970);
            //Movie_player.Visible = false;
            //runBrowserThread(new Uri("http://google.com/")) ;
            //this.label_time.Visible = false;
            //search.Navigate("http://google.com");\
        }
        public void black()
        {

            pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/black.png");
        }
        public void setMusicList()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/MusicList.jpg");
        }
        public void setMusic(string music)
        {
            if(music != "Stop")
            for (int i = 0; i < 3; i++)
            {
                pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Music/MusicLoading1.jpg");
                System.Threading.Thread.Sleep(200);
                pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Music/MusicLoading2.jpg");
                System.Threading.Thread.Sleep(200);
                pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Music/MusicLoading3.jpg");
                System.Threading.Thread.Sleep(200);
            }
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Music/" + music + ".jpg");
            if(music == "Stop")
            {
                System.Threading.Thread.Sleep(3500);
                pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Black.png");
            }
        }

        public void setDocumentList()
        {
            pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Document/Document_list.JPG");
        }

        public void setDocument(int classification)
        {
            switch(classification)
            {
                case 1:
                    pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Document/News1.JPG");
                    break;
                case 2:
                    pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Document/Novel1.JPG");
                    break;
                case 3:
                    pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Document/Sports1.JPG");
                    break;
                case 4:
                    pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Document/Fun1.JPG");
                    break;
                default:
                    break;
            }
        
        }
        public void setDocumentAudio(int classification)
        {
            switch (classification)
            {
                case 1:
                    pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Document/News1_audio.JPG");
                    break;
                case 2:
                    pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Document/Novel1_audio.JPG");
                    break;
                case 3:
                    pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Document/Sports1_audio.JPG");
                    break;
                case 4:
                    pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Document/Fun1_audio.JPG");
                    break;
                default:
                    break;
            }

        }

        public void setTime()
        {
            if (mytimeThread == null) { 
                mytimeThread = new Thread(setTimeThread);
                mytimeThread.Start();
            }  
            pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Time.png");
          
        }
        public void setTimeThread()
        {
            string time_tmp;
            while (true)
            {
                time_tmp = DateTime.Now.ToString();
                this.label_time.Text = time_tmp.Substring(0, 10) + "\n" + time_tmp.Substring(11, time_tmp.Length - 11);
                System.Threading.Thread.Sleep(1000);
            }
            
        }
        public void unsetTime()
        {
            if(mytimeThread != null)
                mytimeThread.Abort();
            mytimeThread = null;
            this.label_time.Text = "";

        }

        public void setMapList()
        {

        }
        public void setMap()
        {
            
        }
        
        public void setSearch(string givenString)
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.FileName = @"C:\chrome.exe.lnk";
            proc.Arguments = @"-kiosk https://search.naver.com/search.naver?ie=utf8&sm=stp_hty&where=se&query=" + givenString;
            Process.Start(proc);
        }
        public void unsetSearch()
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.UseShellExecute = false;
            proc.FileName = @"taskkill";
            proc.Arguments = @" -im Chrome.exe";
            Process.Start(proc);
            pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/black.png");
        }
        public void setSearchImg()
        {
            pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/search.png");
        }

        public void setImgSearch(string givenString)
        {
            
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.FileName = @"C:\chrome.exe.lnk";
            proc.Arguments = @"-kiosk http://imagesearch.naver.com/search.naver?sm=ext&viewloc=1&where=idetail&rev=31&query="+ givenString + "&section=image&res_fr=0&res_to=0&ie=utf8&face=0&color=0&ccl=0&aq=0&spq=1&nx_search_query=" + givenString;
            Process.Start(proc);
        }
        public void unsetImgSearch()
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.UseShellExecute = false;
            proc.FileName = @"taskkill";
            proc.Arguments = @" -im Chrome.exe";
            Process.Start(proc);
            pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/black.png");
        }
        public void setImgSearchImg()
        {
            pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/search.png");
        }


        public void setMovieList()
        {
            this.Opacity = 0;

            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Movie/Movie1.jpg");
            for (int i = 0; i < 500; i++)
            {
                this.Opacity = this.Opacity + 0.002;
                System.Threading.Thread.Sleep(1);
            }

        }
        public void setMovie(int movie_count)
        {
            for (int i = 0; i < 500; i++)
            {
                this.Opacity = this.Opacity - 0.002;
                System.Threading.Thread.Sleep(1);
            }
            pictureBox1.Image = Image.FromFile(@"C:\Users\Lee\Desktop\speech_recog (1)\ms_speech_server_version\ConsoleApplication1\Resource/Movie/Movie" + movie_count + ".jpg");
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            for (int i = 0; i < 500; i++)
            {
                this.Opacity = this.Opacity + 0.002;
                System.Threading.Thread.Sleep(1);
            }
            Console.Write(this.Opacity);
            
        }
        public void execMovie(int movie_count)
        {
            string url = "";
            switch (movie_count)
            {
                case 1:
                    url = "http://movie.naver.com/movie/bi/mi/mediaView.nhn?code=38899&mid=31999#tab";
                    break;
                case 2:
                    url = "http://movie.naver.com/movie/bi/mi/mediaView.nhn?code=125459&mid=31494#tab";
                    
                    break;
                case 3:
                    url = "http://movie.naver.com/movie/bi/mi/mediaView.nhn?code=140695&mid=31928#tab";
                    break;
                case 4:
                    url = "http://movie.naver.com/movie/bi/mi/mediaView.nhn?code=129461&mid=31186#tab";
                    break;
                case 5:
                    url = "http://movie.naver.com/movie/bi/mi/mediaView.nhn?code=115642&mid=32192#tab";
                    break;
                default:
                    return;
                    break;
            }
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.FileName = @"C:\chrome.exe.lnk";
            proc.Arguments = @"-kiosk " + url;
            Process.Start(proc);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
